using Gym_App_MVVM_V1.Model;
using Gym_App_MVVM_V1.ViewModel;


namespace Gym_App_MVVM_V1
{
    public partial class MainPage : ContentPage
    {
        #region Vars
        internal static int GroupPickerIndex = 0;
        internal static int WorkoutPickerIndex = 0;
        internal static object WorkoutPickerItem;
        internal static WorkoutGroupsViewModel viewModel = new();
        internal static WorkoutListViewModel workoutListViewModel = new();
        internal static RecordsViewModel recordsViewModel = new();

        #endregion

        public MainPage()
        {
            InitializeComponent();
            // This is where data will be loaded in if it exists...
            #region TempData
            // Example workout groups and workouts

            WorkoutGroupModel push = new("Push Day");
            WorkoutGroupModel pull = new("Pull Day");
            WorkoutGroupModel legs = new("Leg Day");
            WorkoutModel bench = new("Bench", push);
            WorkoutModel tris = new("Tri Ext.", push);
            WorkoutModel incline = new("Incline", push);
            WorkoutModel curls = new("Curls", pull);
            WorkoutModel highRows = new("High Rows", pull);
            WorkoutModel lowRows = new("Low Rows", pull);
            WorkoutModel squats = new("Squats", legs);
            WorkoutModel calfRaises = new("Calf Raises", legs);
            WorkoutModel deadLifts = new("Dead Lifts", legs);

            // Example workout records

            RecordOfWorkoutModel workoutRecord = new(curls, 10, 3, 30);
            workoutRecord = new(curls, 10, 3, 50);
            workoutRecord = new(curls, 10, 3, 40);
            workoutRecord = new(highRows, 10, 3, 50);
            workoutRecord = new(highRows, 10, 3, 60);
            workoutRecord = new(highRows, 10, 3, 55);
            workoutRecord = new(lowRows, 10, 3, 50);
            workoutRecord = new(lowRows, 10, 3, 60);
            workoutRecord = new(lowRows, 10, 3, 55);

            #endregion

            // GroupPicker.IsVisible = true;
        }
        private void GroupPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Need to reassess this and see if I can move it to the viewmodel
            var picker = (Picker)sender;
            workoutListViewModel.GroupSelectedIndex = picker.SelectedIndex;
            var workouts = workoutListViewModel.GetWorkoutList();
            WorkoutPicker.BindingContext = workoutListViewModel;
            WorkoutPicker.ItemsSource = workouts;
            WorkoutPicker.ItemDisplayBinding = new Binding("Exercise");

            // Hides UI until user selects workout group
            if (GroupPickerIndex >= 0)
            {
                WorkoutPicker.IsVisible = true;
                WorkoutGroupButtons.IsVisible = true;
                WorkoutButtons.IsVisible = true;
                picker.BackgroundColor = Color.Parse("DarkBlue");
            }
            else
            {
                WorkoutGrid.IsVisible = false;
                SubmitButton.IsVisible = false;
                picker.BackgroundColor = Color.Parse("Gray");
                WorkoutGroupButtons.IsVisible = false;
            }
        }

        private void WorkoutPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            WorkoutPickerIndex = WorkoutPicker.SelectedIndex;
            WorkoutPickerItem = WorkoutPicker.SelectedItem;

            // Hides UI until user selects workout

            if(WorkoutPickerIndex >= 0)
            {
                recordsViewModel.GetFirstLastWorkout(FirstWeight, FirstReps, FirstSets, LastWeight, LastReps, LastSets);
                recordsViewModel.GetMaxWorkout(MaxWeight, MaxReps, MaxSets);
                WorkoutGrid.IsVisible = true;
                SubmitButton.IsVisible = true;
                WorkoutPicker.BackgroundColor = Color.Parse("DarkBlue");
            }
            else
            {
                WorkoutGrid.IsVisible = false;
                SubmitButton.IsVisible = false;
                WorkoutPicker.BackgroundColor = Color.Parse("Gray");
            }
        }
        private async void AddWorkoutGroupClicked(object sender, EventArgs e)
        {
            // Opens new page with fields to add a workout group
            await Navigation.PushModalAsync(new AddWorkoutGroupPage());
        }

        private void AddWorkoutClicked(object sender, EventArgs e)
        {
            // Opens new page with fields to add a workout
        }

        private async void RemoveWorkoutGroupClicked(object sender, EventArgs e)
        {
            // Confirm and then delete selected workout group doesn't need a new page
            await Navigation.PushModalAsync(new RemoveWorkoutGroupPage());
        }

        private void RemoveWorkoutClicked(object sender, EventArgs e)
        {
            // Confirm and then delete selected workout
        }

        private void SubmitClicked(object sender, EventArgs e)
        {
            // Submit data to record
            WorkoutModel workoutModel = viewModel.WorkoutGroups[GroupPicker.SelectedIndex].WorkoutsList[WorkoutPicker.SelectedIndex];
            RecordOfWorkoutModel workoutToRecord = new(workoutModel, int.Parse(Reps.Text), int.Parse(Sets.Text), int.Parse(Weight.Text));
        }
    }
}