
using System.Globalization;

namespace SCHOOL2.Models
{
    public class Activity
    {
        public static List<Activity> AllActivities = new List<Activity>();
        public List<Evaluation> ActivityEvaluations { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public string Filename { get; set; }
        public int ECTS { get; set; }
        public string TeacherFileName { get; set;} 

        public Activity(string name, int ects, string teacherFileName)
        {
            Name = name;
            TeacherFileName = teacherFileName;
            ActivityEvaluations = new List<Evaluation>();
            Teacher = Teacher.Load(teacherFileName);
            ECTS = ects;
            Filename = $"{Path.GetRandomFileName()}.activity.txt";
        }

        public static Activity Load(string filename)
        {
            var rootFilename = Path.Combine(Config.RootDir, filename);
            string content = File.ReadAllText(rootFilename);
            var tokens = content.Split(Environment.NewLine);
            Activity activity = new Activity(tokens[0], Int32.Parse(tokens[1]), tokens[2]); 
            activity.Filename = filename;

            return activity;
        }

        public static void LoadAll()
        {
            AllActivities = new List<Activity>();
            IEnumerable<Activity> activities = Directory
                .EnumerateFiles(Config.RootDir, "*.activity.txt")
                .Select(filename => Activity.Load(Path.GetFileName(filename)))
                .OrderBy(activity => activity.Name);
            foreach (var activity in activities)
            {
                AllActivities.Add(activity);
            }
        }

        public void LoadAllEvaluations()
        {
            ActivityEvaluations.Clear();
            IEnumerable<Evaluation> evaluations = Directory
                .EnumerateFiles(Config.RootDir, "*.evaluation.txt")
                .Select(filename => Evaluation.Load(Path.GetFileName(filename)))
                .OrderBy(evaluation => evaluation.DisplayName);
            foreach (var evaluation in evaluations)
            {
                if (evaluation.Activity.Filename == Filename)
                {
                    ActivityEvaluations.Add(evaluation);
                }
            }
        }

        public void Save()
        {
            var rootFilename = Path.Combine(Config.RootDir, Filename);
            string content = string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, Name, ECTS, TeacherFileName);
            File.WriteAllText(rootFilename, content);
        }

        public static void Delete(string filename)
        {
            var rootFilename = Path.Combine(Config.RootDir, filename);
            File.Delete(rootFilename);
        }

        public string DisplayName
        {
            get{ return String.Format("{0} - {2}", Name, ECTS, Teacher.DisplayName); }
        }

        public string DisplayActivity
        {
            get { return String.Format("{0} [{1}]", Name, ECTS); }
        }   
    }
}
