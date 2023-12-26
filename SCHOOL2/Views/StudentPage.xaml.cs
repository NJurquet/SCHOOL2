using SCHOOL2.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SCHOOL2.Views;

public partial class StudentPage : ContentPage, INotifyPropertyChanged
{
    public List<Student> AllStudents => Student.AllStudents;
    public List<Activity> AllActivities => Activity.AllActivities;

    private Student _selectedEvaluationStudent;
    private Activity _selectedEvaluationActivity;

    private Student _selectedStudent;

    public List<Evaluation> SelectedStudentEvaluations => SelectedStudent?.StudentEvaluations;

    public event PropertyChangedEventHandler PropertyChanged;
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
                OnPropertyChanged();
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
                OnPropertyChanged();
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
    }

    public Student SelectedStudent
    {
        get => _selectedStudent;
        set
        {
            if (_selectedStudent != value)
            {
                _selectedStudent = value;
                _selectedStudent.LoadAllEvaluations();
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedStudentEvaluations));
            }
        }
    }

    private void OnStudentSelected(object sender, EventArgs e)
    {
        // This will trigger UI update for SelectedStudentEvaluations
        OnPropertyChanged(nameof(SelectedStudentEvaluations));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}