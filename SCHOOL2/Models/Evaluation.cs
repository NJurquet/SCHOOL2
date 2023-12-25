namespace SCHOOL2.Models
{
    public class Evaluation
    {
        public static List<Evaluation> AllEvaluations = new List<Evaluation>();
        public Student Student { get; set; }
        public Activity Activity { get; set; }
        public int Grade { get; set; }
        public string Filename { get; set; }
        public string StudentFilename { get; set; }
        public string ActivityFilename { get; set; }

        public Evaluation(string studentFileName, string activityFileName, int grade)
        {
            Student = Student.Load(studentFileName);
            Activity = Activity.Load(activityFileName);
            Grade = grade;
            Filename = $"{Path.GetRandomFileName()}.evaluation.txt";
        }

        public void setAppreciation(string appreciation)
        {
            switch(appreciation)
            {
                case "X":
                    Grade = 20;
                    break;
                case "TB":
                    Grade = 16;
                    break;
                case "B":
                    Grade = 12;
                    break;
                case "C":
                    Grade = 8;
                    break;
                case "N":
                    Grade = 4;
                    break;
                default:
                    Grade = 0;
                    break;
            }
        }   

        public void setGrade(int grade)
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

        public static void Delete(string filename)
        {
            var rootFilename = Path.Combine(Config.RootDir, filename);
            File.Delete(rootFilename);
        }

        public override string ToString()
        {
            return $"{Activity.Name} : {Grade}/20";
        }
    }
}
