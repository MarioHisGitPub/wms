using Gtk;
using MyConsoleApp.ui;

class Program
{
    static void Main()
    {
        Application.Init();
        var window = new MainWindow();
        Application.Run();
    }
}
