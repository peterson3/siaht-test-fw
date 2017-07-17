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
    /// Interaction logic for SysView.xaml
    /// </summary>
    public partial class SysView : UserControl
    {
        MainWindow mainWindow;
        public ObservableCollection<Sistema> sistemas { get; set; }
        public Sistema sistemaSelecionado { get; set; }
        public SysView(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            loadData();
        }

        public void refresh()
        {
            this.loadData();
            this.idTextBlock.Text = "";
            this.nomeSistemaTxt.Text = "";
            this.urlSistemaTxt.Text = "";
            DisableEdit();
        }

        public void loadData()
        {
            sistemas = Sistema_DAO.getAllSistemas();
            sistemasGrid.DataContext = this;
            sistemasGrid.ItemsSource = sistemas;
            sistemasGrid.AutoGenerateColumns = true;
        }

        private void novoSistema_Btn(object sender, RoutedEventArgs e)
        {
            //Abrir A página de adição de novo sistema
           // mainWindow.OpenSysAddView();
            mainWindow.OpenSystemAddChildWindow(this);
            //Salvar Novo Sistema No Banco
            //Sistema sistemaAdicionado = new Sistema(nomeSistemaTxt.Text);
            //sistemaAdicionado.homeURL = urlSistemaTxt.Text;
            //sistemaAdicionado.Salvar();
            //MessageBox.Show("Sistema Adicionado: " + sistemaAdicionado.Id + ": " + sistemaAdicionado.Nome);
            //this.refresh();
        }

        private void sistemasGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (sistemasGrid.SelectedItems.Count == 1)
            {
                sistemaSelecionado = ((Sistema)sistemasGrid.SelectedItems[0]);
                this.idTextBlock.Text = sistemaSelecionado.Id.ToString();
                this.nomeSistemaTxt.Text = sistemaSelecionado.Nome;
                this.urlSistemaTxt.Text = sistemaSelecionado.homeURL;

            }
            this.DisableEdit();

        }

        private void EditarRegistroSistema(object sender, RoutedEventArgs e)
        {
            //Abrir Campos para Edição, Abrir Botão De Salvar
            EnableEdit();
            //mainWindow.FlyOutFeedBack("Edit Mode On");
        }

        private void toogleEdit()
        {
            if (this.nomeSistemaTxt.IsEnabled == true)
            {
                this.nomeSistemaTxt.IsEnabled = false;
                this.urlSistemaTxt.IsEnabled = false;
            }
            else
            {
                this.nomeSistemaTxt.IsEnabled = true;
                this.urlSistemaTxt.IsEnabled = true;
            }

        }

        private void EnableEdit()
        {
            this.nomeSistemaTxt.IsEnabled = true;
            this.urlSistemaTxt.IsEnabled = true;
            saveSysChangesBtn.Visibility = Visibility.Visible;
        }

        private void DisableEdit()
        {
            this.nomeSistemaTxt.IsEnabled = false;
            this.urlSistemaTxt.IsEnabled = false;
            saveSysChangesBtn.Visibility = Visibility.Hidden;
        }

        private async void DeletarRegistroSistema(object sender, RoutedEventArgs e)
        {
            //Confirmar delete (avisando que irá deletar todas as telas
            //e ações correspondentes daquele sistema, ação que não pode ser desfeita)
            if (String.IsNullOrWhiteSpace(idTextBlock.Text))
            {
                mainWindow.FlyOutFeedBack("Nenhum Registro Selecionado");
                return;
            }
            MessageDialogResult result = await mainWindow.showQuestion("Confirmar Exclusão", "Tem certeza que deseja excluir sistema " + nomeSistemaTxt.Text + "?");
            if (result == MessageDialogResult.Affirmative)
            {
                sistemaSelecionado.Deletar();
                this.refresh();
                mainWindow.FlyOutFeedBack("Sistema Excluído");
            }
            else
            {
                mainWindow.FlyOutFeedBack("Exclusão Cancelada");
            }
        }

        private void ValidarEdicao(object sender, RoutedEventArgs e)
        {
            sistemaSelecionado.Nome = nomeSistemaTxt.Text;
            sistemaSelecionado.homeURL = urlSistemaTxt.Text;
            sistemaSelecionado.Update();
            mainWindow.FlyOutFeedBack("Sistema Atualizado");
            DisableEdit();
            refresh();
        }


    }
}
