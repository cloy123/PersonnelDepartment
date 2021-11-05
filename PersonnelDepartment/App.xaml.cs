using System.Windows;

namespace PersonnelDepartment
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (e.Args.Length >= 1)
            {
                MainWindow window = new MainWindow(e.Args[0]);
                window.Show();
            }

            else
            {
                MainWindow window = new MainWindow();
                window.Show();
            }
        }
    }
}
