namespace SCHOOL2;

public class Config
{
    public static string RootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".SCHOOL");

    private Config() { }
}
