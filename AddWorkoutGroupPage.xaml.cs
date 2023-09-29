using Gym_App_MVVM_V1.Model;
using Gym_App_MVVM_V1.ViewModel;

namespace Gym_App_MVVM_V1;

public partial class AddWorkoutGroupPage : ContentPage
{
	public AddWorkoutGroupPage()
	{
		InitializeComponent();
        int groupSelectedIndex = MainPage.workoutListViewModel.GroupSelectedIndex;

        // Display the workouts from the selected workout group

        var workoutGroups = WorkoutGroupsCollectionModel.GetWorkoutGroups();
        var workouts = workoutGroups[groupSelectedIndex].GetWorkoutsList();

        for (int i = 0; i < workouts.Count; i++)
        {
            WorkoutGroupsList.RowDefinitions.Add(new RowDefinition());
            Label label = new()
            {
                Text = workouts[i].Exercise
            };
            WorkoutGroupsList.Add(label, 0, i);
        }

    }

    private async void BackBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}