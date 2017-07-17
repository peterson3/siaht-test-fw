using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UI_test_player_TD.DB;
using UI_test_player_TD.Model;

namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for AddTelaChildUC.xaml
    /// </summary>
    public partial class AddTelaChildUC : UserControl
    {
        private AddTelaChildWindow parentChildWindow;
        private TelasView parentWindow;
        private MainWindow mainWindow;



        public Sistema selectedSistema { get; set; }
        public ObservableCollection<Sistema> sistemas { get; set; }




        public AddTelaChildUC(AddTelaChildWindow addTelaChildWindow, TelasView parentWindow, MainWindow mainWindow)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.parentChildWindow = addTelaChildWindow;
            this.parentWindow = parentWindow;
            this.mainWindow = mainWindow;
            this.refresh();
            //Popular a ComboBox
        }

        private void salvarNovaTelaBtn(object sender, RoutedEventArgs e)
        {
            //Salvar Nova Tela
            if (selectedSistema == null)
            {

            }
            else
            {
                if (String.IsNullOrWhiteSpace(nomeTelaTxt.Text))
                {

                }
                else
                {
                    //Adiciona no Bd
                    Tela telaAdicionada = new Tela(nomeTelaTxt.Text, selectedSistema);
                    telaAdicionada.Salvar();
                    //sai da tela
                    parentWindow.refresh();
                    parentChildWindow.Close();
                    //feedback
                    mainWindow.FlyOutFeedBack("Tela Adicionada");
                }
            }
        }

        private void cancelarBtn(object sender, RoutedEventArgs e)
        {
            //Cancelar
            parentWindow.refresh();
            parentChildWindow.Close();
        }

        private void refresh()
        {
            sistemas = Sistema_DAO.getAllSistemas();
            sistemasComboBox.DataContext = this;
            sistemasComboBox.ItemsSource = sistemas;
        }
    }
}
