using SCHOOL2.Models;

namespace SCHOOL2.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        Teacher.LoadAll();
        BindingContext = this;
    }

    public List<Activity> AllActivities
    {
        get
        {
            return Activity.AllActivities;
        }
    }

    public List<Teacher> AllTeachers
    {
        get
        {
            return Teacher.AllTeachers;
        }
    }

    public List<Student> AllStudents
    {
        get
        {
            return Student.AllStudents;
        }
    }

    private async void OnAddTeacherClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeacherPage());
    }
}

