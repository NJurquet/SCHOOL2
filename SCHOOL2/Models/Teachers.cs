namespace SCHOOL2;

public class Teacher
{
    public static List<Teacher> AllTeachers = new List<Teacher>();
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public double Salary { get; set; }
    public string Filename { get; set; }
    public string DisplayName
    {
        get { return $"{Firstname} {Lastname}"; }
    }

    public Teacher(string firstname, string lastname, double salary)
    {
        Firstname = firstname;
        Lastname = lastname;
        Salary = salary;
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

    public void Save()
    {
        var rootFilename = Path.Combine(Config.RootDir, Filename);
        string content = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, Firstname, Lastname, Salary);
        File.WriteAllText(rootFilename, content);
    }
}
