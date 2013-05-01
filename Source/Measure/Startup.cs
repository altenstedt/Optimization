using System;
using System.Windows;

namespace Measure
{
    public class Startup
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new Application();
            var mainWindow = new MainWindow();
            app.Run(mainWindow);
        }
    }
}
