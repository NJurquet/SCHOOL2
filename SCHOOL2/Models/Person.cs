namespace SCHOOL2.Models
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Filename { get; set; }


        public Person(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public string DisplayName
        {
            get { return $"{Firstname} {Lastname}"; }
        }

    }
}
