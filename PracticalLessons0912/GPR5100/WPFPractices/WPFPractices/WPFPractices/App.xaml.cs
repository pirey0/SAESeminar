using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFPractices
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {

            var mainWindow = new MainWindow();

            mainWindow.ParseCommandLineArguments(e.Args);

            MainWindow = mainWindow;
            MainWindow.Show();
        }

    }
}
