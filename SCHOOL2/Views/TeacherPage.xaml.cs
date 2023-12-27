using SCHOOL2.Models;
using System.ComponentModel;
using SCHOOL2.Utils;

namespace SCHOOL2.Views;
public partial class TeacherPage : ContentPage, INotifyPropertyChanged
{
    private Teacher _selectedTeacher;
    public List<Teacher> AllTeachers => Teacher.AllTeachers;
    public List<Activity> SelectedTeacherActivities => SelectedTeacher?.TeacherActivities;
    public TeacherPage()
    {
        InitializeComponent();
        Teacher.LoadAll();
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += LoadData;
    }

    private void LoadData()
    {
        Teacher.LoadAll();
        OnPropertyChanged(nameof(AllTeachers));
    }

    public Teacher SelectedTeacher
    {
        get => _selectedTeacher;
        set
        {
            if (_selectedTeacher != value)
            {
                _selectedTeacher = value;
                _selectedTeacher?.LoadAllActivities();
            }
        }
    }

    private void OnTeacherSelected(object sender, EventArgs e)
    {
        OnPropertyChanged(nameof(SelectedTeacherActivities));
    }

    private void OnAddTeacherClicked(object sender, EventArgs e)
    {
        string firstName = entryFirstName.Text;
        string lastName = entryLastName.Text;
        double salary;
        double.TryParse(entrySalary.Text, out salary);

        var newTeacher = new Teacher(firstName, lastName, salary);
        newTeacher.Save();

        Teacher.LoadAll();
        OnPropertyChanged(nameof(AllTeachers));

        DataChangedNotifier.NotifyDataChanged();

        entryFirstName.Text = string.Empty;
        entryLastName.Text = string.Empty;
        entrySalary.Text = string.Empty;
    }
}