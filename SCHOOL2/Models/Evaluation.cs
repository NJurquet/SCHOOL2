using SCHOOL2.Utils;

namespace SCHOOL2.Models
{
    public class Evaluation
    {
        public static List<Evaluation> AllEvaluations = new List<Evaluation>();
        public Student Student { get; private set; }
        public Activity Activity { get; private set; }
        public int Grade { get; private set; }
        public string Filename { get; private set; }

        public Evaluation(string studentFileName, string activityFileName, int grade)
        {
            Student = Student.Load(studentFileName);
            Activity = Activity.Load(activityFileName);
            Grade = grade;
            Filename = $"{Path.GetRandomFileName()}.evaluation.txt";
        }

        public void SetAppreciation(string appreciation)
        {
            Grade = appreciation switch
            {
                "X" => 20,
                "TB" => 16,
                "B" => 12,
                "C" => 8,
                "N" => 4,
                _ => 0,
            };
        }   

        public void SetGrade(int grade)
        {
            Grade = grade;
        }   

        public static Evaluation Load(string filename)
        {
            var rootFilename = Path.Combine(Config.RootDir, filename);
            string content = File.ReadAllText(rootFilename);
            var tokens = content.Split(Environment.NewLine);
            Evaluation evaluation = new Evaluation(tokens[0], tokens[1], Int32.Parse(tokens[2]));
            evaluation.Filename = filename;

            return evaluation;
        }

        public static void LoadAll()
        {
            AllEvaluations = new List<Evaluation>();
            IEnumerable<Evaluation> evaluations = Directory
                .EnumerateFiles(Config.RootDir, "*.evaluation.txt")
                .Select(filename => Evaluation.Load(Path.GetFileName(filename)))
                .OrderBy(evaluation => evaluation.Student.DisplayName);
            foreach (var evaluation in evaluations)
            {
                AllEvaluations.Add(evaluation);
            }
        }

        public void Save()
        {
            var rootFilename = Path.Combine(Config.RootDir, Filename);
            string content = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, Student.Filename, Activity.Filename, Grade);
            File.WriteAllText(rootFilename, content);
        }

        public string DisplayName
        {
            get
            {
                return $"{Activity.Name} : {Grade}/20";
            }
        }
    }
}
