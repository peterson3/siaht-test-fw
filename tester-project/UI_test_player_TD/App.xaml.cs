using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace UI_test_player_TD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length==0)
            {
               MetroWindow wnd = new MainWindow();
               wnd.Show();
            }
            else
            {
                MessageBox.Show("Parametros Não Suportados");
                Application.Current.Shutdown();
            }
        }
    }
}
