using SCHOOL2.Models;

namespace SCHOOL2.Views;

public partial class TeacherPage : ContentPage
{
    public static List<Teacher> AllTeachers
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
        // Récupérer les données du formulaire
        string firstName = entryFirstName.Text;
        string lastName = entryLastName.Text;
        double salary;

        if (double.TryParse(entrySalary.Text, out salary))
        {
            // Ajouter le professeur à la liste ou effectuer d'autres opérations nécessaires
            var newTeacher = new Teacher(firstName, lastName, salary);
            newTeacher.Save();
            // Ajouter la logique pour ajouter le nouveau professeur à votre liste de professeurs
            // par exemple, MainPageViewModel.Instance.AddTeacher(newTeacher);
        }
        else
        {
            // Gérer l'erreur de conversion du salaire
        }

        // Retourner à la page principale après l'ajout
        Navigation.PopAsync();
    }
}