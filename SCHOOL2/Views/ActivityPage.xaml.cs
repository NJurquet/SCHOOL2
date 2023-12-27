using SCHOOL2.Models;
using System.ComponentModel;
using SCHOOL2.Utils;

namespace SCHOOL2.Views;
public partial class ActivityPage : ContentPage, INotifyPropertyChanged
{
    private Teacher _selectedTeacher;
    private Activity _selectedActivity;
    public List<Teacher> AllTeachers => Teacher.AllTeachers;
    public List<Activity> AllActivities => Activity.AllActivities;
    public List<Evaluation> SelectedActivityEvaluations => SelectedActivity?.ActivityEvaluations;
    public ActivityPage()
	{
		InitializeComponent();
        Activity.LoadAll();
        BindingContext = this;
        DataChangedNotifier.OnDataChanged += LoadData;
    }
    private void LoadData()
    {
        Teacher.LoadAll();
        Activity.LoadAll();
        OnPropertyChanged(nameof(AllActivities));
        OnPropertyChanged(nameof(AllTeachers));
        OnPropertyChanged(nameof(SelectedActivityEvaluations));
    }
    public Teacher SelectedTeacher
    {
        get => _selectedTeacher;
        set
        {
            if (_selectedTeacher != value)
            {
                _selectedTeacher = value;
            }
        }
    }

    public Activity SelectedActivity
    {
        get => _selectedActivity;
        set
        {
            if (_selectedActivity != value)
            {
                _selectedActivity = value;
                _selectedActivity?.LoadAllEvaluations();
            }
        }
    }

    private void OnActivitySelected(object sender, EventArgs e)
    {
        OnPropertyChanged(nameof(SelectedActivityEvaluations));
    }

    private void OnAddActivityClicked(object sender, EventArgs e)
	{
        string name = entryName.Text;
        string ECTS = entryECTS.Text;
        int ECTSint;
        int.TryParse(ECTS, out ECTSint);

        string teacherFileName = SelectedTeacher.Filename;

        var newActivity = new Activity(name, ECTSint, teacherFileName);
        newActivity.Save();

        entryName.Text = string.Empty;
        entryECTS.Text = string.Empty;

        Activity.LoadAll();
        OnPropertyChanged(nameof(AllActivities));

        DataChangedNotifier.NotifyDataChanged();
    }
}