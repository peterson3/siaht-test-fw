using MahApps.Metro.SimpleChildWindow;
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
using System.Windows.Shapes;

namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for AddTelaChildWindow.xaml
    /// </summary>
    public partial class AddTelaChildWindow : ChildWindow
    {

        public AddTelaChildUC addSysChildW { get; set; }
        public MainWindow mainWindow { get; set; }
        public TelasView parentWindow { get; set; }


        public AddTelaChildWindow(MainWindow mainWindow, TelasView parentWindow)
        {
            this.InitializeComponent();
            this.mainWindow = mainWindow;
            CloseOnOverlay = false;
            addSysChildW = new AddTelaChildUC(this, parentWindow, mainWindow);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(addSysChildW);
            this.parentWindow = parentWindow;
        }

       
        private void CloseSec_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
