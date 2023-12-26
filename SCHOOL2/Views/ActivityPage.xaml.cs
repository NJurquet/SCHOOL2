namespace SCHOOL2.Views;
using SCHOOL2.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public partial class ActivityPage : ContentPage, INotifyPropertyChanged
{
    private Teacher _selectedTeacher;
    public List<Teacher> AllTeachers => Teacher.AllTeachers;
    public event PropertyChangedEventHandler PropertyChanged;

    public ActivityPage()
	{
		InitializeComponent();
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
                OnPropertyChanged();
            }
        }
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
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}