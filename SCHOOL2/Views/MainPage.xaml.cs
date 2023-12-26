using SCHOOL2.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SCHOOL2.Views;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public MainPage()
	{
		InitializeComponent();
        Teacher.LoadAll();
        BindingContext = this;
        Activity.LoadAll();
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

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

