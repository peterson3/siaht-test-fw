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
    /// Interaction logic for AddAcaoChildWindow.xaml
    /// </summary>
    public partial class AddAcaoChildWindow : ChildWindow
    {
        private MainWindow mainWindow;
        private AcoesView parentWindow;
        private Tela selectedTela;
        private Sistema selectedSistema;
        private TestCaseView parentWindow1;
        public AddAcaoChildUC addAcaoChildW { get; set; }


        public AddAcaoChildWindow(MainWindow mainWindow, AcoesView parentWindow)
        {
            // TODO: Complete member initialization
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.parentWindow = parentWindow;

            CloseOnOverlay = false;
            addAcaoChildW = new AddAcaoChildUC(this, parentWindow, mainWindow);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(addAcaoChildW);
        }

        public AddAcaoChildWindow(MainWindow mainWindow, AcoesView parentWindow, Tela selectedTela, Sistema selectedSistema)
        {
            // TODO: Complete member initialization
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.parentWindow = parentWindow;

            CloseOnOverlay = false;
            addAcaoChildW = new AddAcaoChildUC(this, parentWindow, mainWindow, selectedTela, selectedSistema);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(addAcaoChildW);
        }

        public AddAcaoChildWindow(MainWindow mainWindow, TestCaseView parentWindow1, Tela selectedTela, Sistema selectedSistema)
        {
            // TODO: Complete member initialization
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.parentWindow1 = parentWindow1;

            CloseOnOverlay = false;
            addAcaoChildW = new AddAcaoChildUC(this, parentWindow1, mainWindow, selectedTela, selectedSistema);
            mainGrid.Children.Clear();
            mainGrid.Children.Add(addAcaoChildW);
        }


    }
}
