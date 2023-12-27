using SCHOOL2.Models;
using System.ComponentModel;

namespace SCHOOL2.Views;

public partial class StudentPage : ContentPage, INotifyPropertyChanged
{
    public List<Student> AllStudents => Student.AllStudents;
    public List<Activity> AllActivities => Activity.AllActivities;
    private Student _selectedEvaluationStudent;
    private Activity _selectedEvaluationActivity;

    private Student _selectedStudent;
    private string _selectedStudentAverage;

    public List<Evaluation> SelectedStudentEvaluations => SelectedStudent?.StudentEvaluations;

    public StudentPage()
    {
        InitializeComponent();
        Student.LoadAll();
        BindingContext = this;
    }
    private void OnAddStudentClicked(object sender, EventArgs e)
    {
        // Get student form data
        string firstName = entryFirstName.Text;
        string lastName = entryLastName.Text;

        var newStudent = new Student(firstName, lastName);
        newStudent.Save();
        Student.LoadAll();

        OnPropertyChanged(nameof(AllStudents));
        entryFirstName.Text = string.Empty;
        entryLastName.Text = string.Empty;
    }

    public Student SelectedEvaluationStudent
    {
        get => _selectedEvaluationStudent;
        set
        {
            if (_selectedEvaluationStudent != value)
            {
                _selectedEvaluationStudent = value;
            }
        }
    }
    public Activity SelectedEvaluationActivity
    {
        get => _selectedEvaluationActivity;
        set
        {
            if (_selectedEvaluationActivity != value)
            {
                _selectedEvaluationActivity = value;
            }
        }
    }
    private void OnAddEvaluationClicked(object sender, EventArgs e)
    {
        string studentFileName = SelectedEvaluationStudent.Filename;
        string activityFileName = SelectedEvaluationActivity.Filename;
        string grade = entryGrade.Text;

        Evaluation newEvaluation = new Evaluation(studentFileName, activityFileName, 0);
        if (int.TryParse(grade, out int numericGrade))
        {
            newEvaluation.SetGrade(numericGrade);
        }
        else
        {
            newEvaluation.SetAppreciation(grade);
        }
        newEvaluation.Save();
        Evaluation.LoadAll();
        SelectedEvaluationStudent.LoadAllEvaluations();

        entryGrade.Text = string.Empty;
    }

    public Student SelectedStudent
    {
        get => _selectedStudent;
        set
        {
            if (_selectedStudent != value)
            {
                _selectedStudent = value;
                _selectedStudent?.LoadAllEvaluations();
                OnPropertyChanged(nameof(SelectedStudentEvaluations));

                SelectedStudentAverage = _selectedStudent != null && _selectedStudent.StudentEvaluations.Any()
                ? $"Average for student {_selectedStudent.DisplayName} is : {_selectedStudent.Average():N2}/20"
                : "Select a student or add an evaluation";
            }
        }
    }

    private void OnStudentSelected(object sender, EventArgs e)
    {
        // This will trigger UI update for SelectedStudentEvaluations
        OnPropertyChanged(nameof(SelectedStudentEvaluations));
    }

    public string SelectedStudentAverage
    {
        get => _selectedStudentAverage;
        set
        {
            _selectedStudentAverage = value;
            OnPropertyChanged(nameof(SelectedStudentAverage));
        }
    }
}