using SCHOOL2.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SCHOOL2.Views;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    public List<Student> AllStudents => Student.AllStudents;
    public List<Activity> AllActivities => Activity.AllActivities;
    public List<Teacher> AllTeachers => Teacher.AllTeachers;

    public MainPage()
	{
		InitializeComponent();
        Teacher.LoadAll();
        Student.LoadAll();
        Activity.LoadAll();
        BindingContext = this;
    }
}

