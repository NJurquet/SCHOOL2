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

    public static List<Activity> AllActivities
    {
        get
        {
            return Activity.AllActivities;
        }
    }

    public static List<Teacher> AllTeachers
    {
        get
        {
            return Teacher.AllTeachers;
        }
    }

    public static  List<Student> AllStudents
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

