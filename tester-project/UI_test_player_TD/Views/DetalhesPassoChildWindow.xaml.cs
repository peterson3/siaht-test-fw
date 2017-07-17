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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI_test_player_TD.Model;

namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for DetalhesPassoChildWindow.xaml
    /// </summary>
    public partial class DetalhesPassoChildWindow : ChildWindow
    {
        private MainWindow mainWindow;
        private TestCaseView parentWindow;
        private PassoDoTeste passo;
        private DetalhesPassoUC detalhesPassoUC;
        public DetalhesPassoChildWindow(MainWindow mainWindow, TestCaseView parentWindow, Model.PassoDoTeste passo)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.parentWindow = parentWindow;
            this.passo = passo;
            detalhesPassoUC = new DetalhesPassoUC(this, parentWindow, mainWindow, passo);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(detalhesPassoUC);
            this.parentWindow = parentWindow;
        }
    }
}
