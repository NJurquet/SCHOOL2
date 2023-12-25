using SCHOOL2.Views;

namespace SCHOOL2.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        Teacher.LoadAll();
        BindingContext = this;
    }

    public List<Teacher> AllActivities
    {
        get;
    }

    public List<Teacher> AllTeachers
    {
        get
        {
            return Teacher.AllTeachers;
        }
    }

    public List<Teacher> AllStudents
    {
        get;
    }

    private async void OnAddTeacherClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TeacherPage());
    }
}

