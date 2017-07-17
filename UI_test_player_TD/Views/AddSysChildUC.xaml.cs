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
    /// Interaction logic for AddSysChildW.xaml
    /// </summary>
    public partial class AddSysChildUC : UserControl
    {
        public AddSysChildWindow parentChildWindow { get; set; }
        public MainWindow mainWindow { get; set; }
        public SysView parentWindow { get; set; }

        public AddSysChildUC(AddSysChildWindow parentChildWindow, SysView parentWindow , MainWindow mainWindow)
        {
            InitializeComponent();
            this.parentChildWindow = parentChildWindow;
            this.parentWindow = parentWindow;
            this.mainWindow = mainWindow;
        }

        private void Salvar_Btn_Click(object sender, RoutedEventArgs e)
        {
            //Salvar No Banco, E voltar para a página anterior
            if (String.IsNullOrWhiteSpace(nomeSistemaTxt.Text))
            {
                nomeSistemaTxt.BorderBrush = Brushes.Red;
            }
            else
            {
                nomeSistemaTxt.BorderBrush = Brushes.Gray;
                var sistemaAdicionado = new Sistema(nomeSistemaTxt.Text);
                sistemaAdicionado.homeURL = urlSistemaTxt.Text;
                sistemaAdicionado.Salvar();
                this.parentChildWindow.Close();
                parentWindow.refresh();
                mainWindow.FlyOutFeedBack("Novo Sistema Adicionado!");
            }
        }

        private void Cancelar_Btn_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.refresh();
            this.parentChildWindow.Close();
        }
    }
}
