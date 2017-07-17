using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI_test_player_TD.Controllers;
using UI_test_player_TD.Model;

namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        MainWindow mainWindow;

        public SettingsView(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.loadInfo();
        }

        public void loadInfo()
        {
            this.ctfPathBlock.Text = Settings.ctfsPath;

            if (Settings.IgnorarFalha == true)
            {
                ignoraErroY.IsChecked = true;
            }
            else
            {
                ignoraErroN.IsChecked = true;
            }

            if (Settings.CloseBrowserAfterTestCase == true)
            {
                fecharBrowserY.IsChecked = true;
            }
            else
            {
                fecharBrowserN.IsChecked = true;
            }
        }

        public void saveInfo()
        {
            Settings.ctfsPath = this.ctfPathBlock.Text;

            if (ignoraErroY.IsChecked == true)
            {
                Settings.IgnorarFalha = true;
            }
            else
            {
                Settings.IgnorarFalha = false;
            }

            if (fecharBrowserY.IsChecked == true)
            {
                Settings.CloseBrowserAfterTestCase = true;
            }
            else
            {
                Settings.CloseBrowserAfterTestCase = false;
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            saveInfo();
            MessageBox.Show("Salvo com Sucesso");
        }

    }
}
