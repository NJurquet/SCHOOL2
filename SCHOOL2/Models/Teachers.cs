using System.Collections.ObjectModel;

namespace SCHOOL2.Models;

public class Teacher : Person
{
    public static List<Teacher> AllTeachers = new List<Teacher>();
    public List<Activity> TeacherActivities { get ; set; }

    public double Salary { get; set; }


    public Teacher(string firstname, string lastname, double salary) : base(firstname, lastname)
    {
        Salary = salary;
        TeacherActivities = new List<Activity>();
        Filename = $"{Path.GetRandomFileName()}.teacher.txt";
    }

    public static Teacher Load(string filename)
    {
        var rootFilename = Path.Combine(Config.RootDir, filename);

        string content = File.ReadAllText(rootFilename);
        var tokens = content.Split(Environment.NewLine);
        Teacher teacher = new Teacher(tokens[0], tokens[1], Convert.ToDouble(tokens[2]));
        teacher.Filename = filename;

        return teacher;
    }

    public static void LoadAll()
    {
        AllTeachers = new List<Teacher>();
        IEnumerable<Teacher> teachers = Directory
            .EnumerateFiles(Config.RootDir, "*.teacher.txt")
            .Select(filename => Teacher.Load(Path.GetFileName(filename)))
            .OrderBy(teacher => teacher.DisplayName);
        foreach (var teacher in teachers)
        {
            AllTeachers.Add(teacher);
        }
    }

    public void LoadAllActivities()
    {
        TeacherActivities.Clear();
        var teacherActivities = new List<Activity>();
        IEnumerable<Activity> activities = Directory
            .EnumerateFiles(Config.RootDir, "*.activity.txt")
            .Select(filename => Activity.Load(Path.GetFileName(filename)))
            .OrderBy(activity => activity.Name);
        foreach (var activity in activities)
        {
            if (activity.TeacherFileName == Filename)
            {
                TeacherActivities.Add(activity);
            }
        }
    }

    public void Save()
    {
        var rootFilename = Path.Combine(Config.RootDir, Filename);
        string content = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, Firstname, Lastname, Salary);
        File.WriteAllText(rootFilename, content);
    }

    public static void Delete(string filename)
    {
        var rootFilename = Path.Combine(Config.RootDir, filename);
        File.Delete(rootFilename);
    }

    public override string ToString()
    {
        return $"{Firstname} {Lastname} {Salary}";
    }
}
