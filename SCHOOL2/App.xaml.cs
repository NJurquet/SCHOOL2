namespace SCHOOL2;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
		Directory.CreateDirectory(Config.RootDir);
	}
}
