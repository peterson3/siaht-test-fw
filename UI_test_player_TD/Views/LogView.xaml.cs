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

namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for LogView.xaml
    /// </summary>
    public partial class LogView : UserControl
    {
        private MainWindow mainWindow;


        public LogView(MainWindow mainWindow)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        public void AppendText(string text)
        {
            this.textBox.AppendText(text);
        }
    }
}
