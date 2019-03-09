using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for TelasView.xaml
    /// </summary>
    public partial class TelasView : UserControl
    {
        MainWindow mainWindow;
        public ObservableCollection<Sistema> sistemas { get; set; }
        public ObservableCollection<Screen> telas { get; set; }

        public Sistema selectedSistema { get; set; }
        public TelasView(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            sistemasBox.DataContext = this;
            telasGrid.DataContext = this;
            this.loadData();


        }

        public Screen selectedTela { get; set; }
        public void refresh()
        {
            loadData();
        }
        public void loadData()
        {
            sistemas = Sistema_DAO.getAllSistemas();
            sistemasBox.ItemsSource = sistemas;

            if (selectedSistema != null)
            {
                telas = Tela_DAO.getAllTelas(selectedSistema);
                telasGrid.ItemsSource = telas;
            }
            
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //Salvar no Banco a Tela do Sistema selecionado (via ID)
        //    //MessageBox.Show(selectedSistema.Nome);
        //    if (selectedSistema == null)
        //    {
        //        MessageBox.Show("Selecione um Sistema para Cadastrar a Tela");
        //    }
        //    else
        //    {
        //        Tela telaAdicionada = new Tela(nomeTelaBlock.Text, selectedSistema);
        //        telaAdicionada.Salvar();
        //        MessageBox.Show("Tela Adicionada: (" + selectedSistema.Nome + ") " + telaAdicionada.Id + ": " + telaAdicionada.Nome);
        //    }
        //    refresh();
        //}

        private void sistemasBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //refresh();//RefreSh Telas Somente
            if (selectedSistema != null)
            {
                telas = Tela_DAO.getAllTelas(selectedSistema);
                telasGrid.ItemsSource = telas;
            }
        }

        private async void EditarRegitroTela(object sender, RoutedEventArgs e)
        {

             MetroDialogSettings st = new MetroDialogSettings();
            st.AffirmativeButtonText = "Ok";
            st.NegativeButtonText = "Cancelar";
            st.DefaultText = selectedTela.Nome;
            string nomeTemp = await this.mainWindow.ShowInputAsync("Alterar Nome Tela", "Insira Novo Nome Da Tela", st);
            if (!String.IsNullOrEmpty(nomeTemp))
            {

                selectedTela.Nome = nomeTemp;
                selectedTela.Alterar();
                await this.mainWindow.ShowMessageAsync("Sucesso!", "Nome Da Tela alterado com sucesso!");

            }
            else
            {
                await this.mainWindow.ShowMessageAsync("Novo Nome Não Informado", "A Tela não foi Alterada");
            }
            
        }

        private void ValidarEdicao(object sender, RoutedEventArgs e)
        {

        }

        private async void DeletarRegistro(object sender, RoutedEventArgs e)
        {
            //Verificar se uma ação está selecionada
            if (selectedTela == null)
            {
                mainWindow.FlyOutFeedBack("Nenhuma Tela selecionada.");
                return;
            }


            MessageDialogResult result = await mainWindow.showQuestion("Confirmar Exclusão", "Tem certeza que deseja excluir a Tela"  + selectedTela.Nome +  "(e todas suas ações)? Essa operação não pode ser desfeita.");
            if (result == MessageDialogResult.Affirmative)
            {
                selectedTela.Deletar();
                this.refresh();
                mainWindow.FlyOutFeedBack("Tela Excluída");
            }
            else
            {
                mainWindow.FlyOutFeedBack("Exclusão Cancelada");
            }
            //Confirmar Operação
            //Feedback
        }

        private void novaTela_Btn(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenTelaAddChildWindow(this);
        }

        

    }
}
