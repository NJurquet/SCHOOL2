namespace SCHOOL2.Models;

public class Student : Person
{
    public static List<Student> AllStudents = new List<Student>();

    public Student(string firstname, string lastname) : base(firstname, lastname)
    {
        Firstname = firstname;
        Lastname = lastname;
        Filename = $"{Path.GetRandomFileName()}.student.txt";
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

    public void Save()
    {
        var rootFilename = Path.Combine(Config.RootDir, Filename);
        string content = string.Format("{1}{0}{2}", Environment.NewLine, Firstname, Lastname);
        File.WriteAllText(rootFilename, content);
    }

    public static void Delete(string filename)
    {
        var rootFilename = Path.Combine(Config.RootDir, filename);
        File.Delete(rootFilename);
    }

    public override string ToString()
    {
        return DisplayName;
    }   
}
