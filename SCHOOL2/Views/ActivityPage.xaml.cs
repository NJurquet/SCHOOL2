namespace SCHOOL2.Views;
using SCHOOL2.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        // This will trigger UI update for SelectedActivityEvaluations
        OnPropertyChanged(nameof(SelectedActivityEvaluations));
    }

    private void OnAddActivityClicked(object sender, EventArgs e)
	{
        // R�cup�rer les donn�es du formulaire
        string name = entryName.Text;
        string ECTS = entryECTS.Text;
        int ECTSint;
        int.TryParse(ECTS, out ECTSint);

        //Récupérer le fichier du professeur sélectionné
        string teacherFileName = SelectedTeacher.Filename;

        MessagingCenter.Send<ActivityPage>(this, "AddActivity");

        // Ajouter l'activit� � la liste ou effectuer d'autres op�rations n�cessaires
        var newActivity = new Activity(name, ECTSint, teacherFileName);
        newActivity.Save();

        Activity.LoadAll();
        OnPropertyChanged(nameof(AllActivities));
    }
}