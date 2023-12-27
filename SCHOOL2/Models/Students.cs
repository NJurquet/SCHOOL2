using SCHOOL2.Utils;

namespace SCHOOL2.Models;
public class Student : Person
{
    public static List<Student> AllStudents = new List<Student>();
    public List<Evaluation> StudentEvaluations { get; private set; }

    public Student(string firstname, string lastname) : base(firstname, lastname)
    {
        Filename = $"{Path.GetRandomFileName()}.student.txt";
        StudentEvaluations = new List<Evaluation>();
    }

    public static Student Load(string filename)
    {
        var rootFilename = Path.Combine(Config.RootDir, filename);
        string content = File.ReadAllText(rootFilename);
        var tokens = content.Split(Environment.NewLine);
        Student student = new Student(tokens[0], tokens[1]);
        student.Filename = filename;

        return student;
    }

    public static void LoadAll()
    {
        AllStudents = new List<Student>();
        IEnumerable<Student> students = Directory
            .EnumerateFiles(Config.RootDir, "*.student.txt")
            .Select(filename => Student.Load(Path.GetFileName(filename)))
            .OrderBy(student => student.DisplayName);
        foreach (var teacher in students)
        {
            AllStudents.Add(teacher);
        }
    }

    public void LoadAllEvaluations()
    {
        StudentEvaluations.Clear();
        IEnumerable<Evaluation> evaluations = Directory
            .EnumerateFiles(Config.RootDir, "*.evaluation.txt")
            .Select(filename => Evaluation.Load(Path.GetFileName(filename)))
            .OrderBy(evaluation => evaluation.ToString());
        foreach (var evaluation in evaluations)
        {
            if (evaluation.Student.Filename == Filename)
            {
                StudentEvaluations.Add(evaluation);
            }
        }
    }

    public double Average()
    {
        int total = 0;
        int ects = 0;
        foreach (var evaluation in StudentEvaluations)
        {
            total += evaluation.Grade * evaluation.Activity.ECTS;
            ects += evaluation.Activity.ECTS;
        }
        return ects != 0 ? (double)total / ects : 0.0;
    }

    public void Save()
    {
        var rootFilename = Path.Combine(Config.RootDir, Filename);
        string content = string.Format("{1}{0}{2}", Environment.NewLine, Firstname, Lastname);
        File.WriteAllText(rootFilename, content);
    }
}
