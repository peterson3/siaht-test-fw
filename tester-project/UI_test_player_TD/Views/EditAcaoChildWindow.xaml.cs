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
using UI_test_player_TD.Model;

namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for EditAcaoChildWindow.xaml
    /// </summary>
    public partial class EditAcaoChildWindow : ChildWindow
    {
        private MainWindow mainWindow;
        private AcoesView parentWindow;
        private TestCaseView testCaseParentWindow;
        public EditAcaoChildUC editAcaoChildW { get; set; }
        public EditAcaoChildWindow(MainWindow mainWindow, AcoesView parentWindow, AcaoDyn acao)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.parentWindow = parentWindow;
            CloseOnOverlay = false;
            editAcaoChildW = new EditAcaoChildUC(this, parentWindow, mainWindow, acao);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(editAcaoChildW);
            this.parentWindow = parentWindow;
        }

        public EditAcaoChildWindow(MainWindow mainWindow, TestCaseView testCaseParentWindow, AcaoDyn acao)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.testCaseParentWindow = testCaseParentWindow;
            CloseOnOverlay = false;
            editAcaoChildW = new EditAcaoChildUC(this, testCaseParentWindow, mainWindow, acao);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(editAcaoChildW);
        }

        private void CloseSec_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
