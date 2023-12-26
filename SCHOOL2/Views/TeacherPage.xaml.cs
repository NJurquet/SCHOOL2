using SCHOOL2.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SCHOOL2.Views;

public partial class TeacherPage : ContentPage, INotifyPropertyChanged
{
    private Teacher _selectedTeacher;
    public List<Teacher> AllTeachers => Teacher.AllTeachers;
    public event PropertyChangedEventHandler PropertyChanged;

    public List<Activity> SelectedTeacherActivities => SelectedTeacher?.TeacherActivities;
    public TeacherPage()
    {
        InitializeComponent();
        Teacher.LoadAll();
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
                OnPropertyChanged(nameof(SelectedTeacherActivities));
                _selectedTeacher.LoadAllActivities();
            }
        }
    }

    private void OnTeacherSelected(object sender, EventArgs e)
    {
        // This will trigger UI update for SelectedTeacherActivities
        OnPropertyChanged(nameof(SelectedTeacherActivities));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnAddTeacherClicked(object sender, EventArgs e)
    {
        // R�cup�rer les donn�es du formulaire
        string firstName = entryFirstName.Text;
        string lastName = entryLastName.Text;
        double salary;

        if (double.TryParse(entrySalary.Text, out salary))
        {
            // Ajouter le professeur � la liste ou effectuer d'autres op�rations n�cessaires
            var newTeacher = new Teacher(firstName, lastName, salary);
            newTeacher.Save();
            // Ajouter la logique pour ajouter le nouveau professeur � votre liste de professeurs
            // par exemple, MainPageViewModel.Instance.AddTeacher(newTeacher);
        }
        else
        {
            // G�rer l'erreur de conversion du salaire
        }

        // Retourner � la page principale apr�s l'ajout
        Navigation.PopAsync();
    }
}