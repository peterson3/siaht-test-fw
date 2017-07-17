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
    /// Interaction logic for DetalhesPassoUC.xaml
    /// </summary>
    public partial class DetalhesPassoUC : UserControl
    {
        private DetalhesPassoChildWindow detalhesPassoChildWindow;
        private TestCaseView parentWindow;
        private MainWindow mainWindow;
        private PassoDoTeste passo;

        public DetalhesPassoUC(DetalhesPassoChildWindow detalhesPassoChildWindow, TestCaseView parentWindow, MainWindow mainWindow, Model.PassoDoTeste passo)
        {
            InitializeComponent();
            this.detalhesPassoChildWindow = detalhesPassoChildWindow;
            this.parentWindow = parentWindow;
            this.mainWindow = mainWindow;
            this.passo = passo;
            this.obsTextBlock.Text = passo.Obs;
        }
    }
}
