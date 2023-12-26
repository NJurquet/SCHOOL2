using SCHOOL2.Models;

namespace SCHOOL2.Views;

public partial class TeacherPage : ContentPage
{
    public List<Teacher> AllTeachers
    {
        get
        {
            return Teacher.AllTeachers;
        }
    }

    public TeacherPage()
    {
        InitializeComponent();
        Teacher.LoadAll();
        BindingContext = this;
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