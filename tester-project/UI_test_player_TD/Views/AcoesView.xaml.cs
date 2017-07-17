using MahApps.Metro.Controls.Dialogs;
using System;
using System.CodeDom.Compiler;
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
    /// Interaction logic for AcoesView.xaml
    /// </summary>
    public partial class AcoesView : UserControl
    {
        public MainWindow mainWindow;
        public ObservableCollection<Sistema> sistemas { get; set; }
        public ObservableCollection<Tela> telas { get; set; }
        public ObservableCollection<AcaoDyn> acoes { get; set; }

        private Sistema _selectedSistema { get; set; }
        public Sistema selectedSistema 
        {
            get { return _selectedSistema; }
            set { _selectedSistema = value;
                telas = _selectedSistema.telas;
                telaCombo.ItemsSource = telas;
                telaCombo.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Nome", System.ComponentModel.ListSortDirection.Ascending));
                }
        }
        public Tela selectedTela { get; set; }

        public AcaoDyn selectedAcao { get; set; }

        public AcoesView(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            acaoGrid.DataContext = this;
            sistemaCombo.DataContext = this;
            telaCombo.DataContext = this;
            refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string richText = new TextRange(CodeToRun.Document.ContentStart, CodeToRun.Document.ContentEnd).Text;
            //Execute(richText);
        }

        public void refresh()
        {
            loadData();
        }

        public void loadData()
        {
            Sistema sistemaSelecionado = null;
            Tela telaSelecionada = null;
            
            if (sistemaCombo.SelectedItem != null)
            {
                sistemaSelecionado = (Sistema)sistemaCombo.SelectedItem;
            }
            
            if (telaCombo.SelectedItem != null)
            {
                telaSelecionada = (Tela)telaCombo.SelectedItem; 
            }

            sistemas = Sistema_DAO.getAllSistemas();
            sistemaCombo.ItemsSource = sistemas;
            sistemaCombo.Items.Refresh();

            if (sistemaSelecionado != null)
            {
                foreach (Sistema item in sistemaCombo.Items)
                {
                    if (item.Id == sistemaSelecionado.Id)
                    {
                        sistemaCombo.SelectedItem = item;
                    }
                }
            }



            if (telaSelecionada != null)
            {
                foreach (Tela item in telaCombo.Items)
                {
                    if (item.Id == telaSelecionada.Id)
                    {
                        telaCombo.SelectedItem = item;
                    }
                }
            }


            telaCombo.Items.Refresh();

            if (selectedTela != null)
            {
                acoes = AcaoDyn_DAO.getAllActionsFromTela(selectedTela);
                acaoGrid.ItemsSource = acoes;
            }

        }

        private void Execute(string code)
        {
            StringBuilder sb = new StringBuilder();

            //-----------------
            // Create the class as usual
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Windows.Forms;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using TopDown_QA_FrameWork;");
            sb.AppendLine("using System.Runtime.InteropServices;");
            sb.AppendLine("using OpenQA.Selenium;");
            sb.AppendLine("using OpenQA.Selenium.Support.PageObjects;");
            sb.AppendLine("using OpenQA.Selenium.Support.UI;");
            sb.AppendLine("using System.ComponentModel;");

            sb.AppendLine();
            sb.AppendLine("namespace TestFrameWork");
            sb.AppendLine("{");

            sb.AppendLine("      public class ActionToRun");
            sb.AppendLine("      {");

            // My pre-defined class named FilterCountries that receive the sourceListBox
            sb.AppendLine("            public void RunAction()");
            sb.AppendLine("            {");
            sb.AppendLine(code);
            sb.AppendLine("            }");
            sb.AppendLine("      }");
            sb.AppendLine("}");

            //-----------------
            // The finished code
            String classCode = sb.ToString();

            //-----------------
            // Dont need any extra assemblies
            Object[] requiredAssemblies = new Object[] { "TopDown_QA_FrameWork.dll" };

            dynamic classRef;
            try
            {
                //txtErrors.Clear();

                //------------
                // Pass the class code, the namespace of the class and the list of extra assemblies needed
                classRef = CodeHelper.HelperFunction(classCode, "TestFrameWork.ActionToRun", requiredAssemblies);

                //-------------------
                // If the compilation process returned an error, then show to the user all errors
                if (classRef is CompilerErrorCollection)
                {
                    StringBuilder sberror = new StringBuilder();

                    foreach (CompilerError error in (CompilerErrorCollection)classRef)
                    {
                        sberror.AppendLine(string.Format("{0}:{1} {2} {3}", error.Line, error.Column, error.ErrorNumber, error.ErrorText));
                    }

                    //txtErrors.Text = sberror.ToString();
                    MessageBox.Show(sberror.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                // If something very bad happened then throw it
                MessageBox.Show(ex.Message);
                throw;
            }

            //-------------
            // Finally call the class to filter the countries with the specific routine provided
            classRef.RunAction();
            //List<string> targetValues = classRef.FilterCountries(lstSource);
            //List<string> targetValues = new List<String>();
            ////-------------
            //// Move the result to the target listbox
            //lstTarget.Items.Clear();
            //lstTarget.Items.AddRange(targetValues.ToArray());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                //MessageBox.Show("[" + selectedSistema.Nome + "] " + selectedTela.Nome);
                //AcaoDyn acaoAdicionada = new AcaoDyn(acaoNomeBlock.Text, selectedTela);
                //acaoAdicionada.requerParametro = true;
                //string richText = new TextRange(CodeToRun.Document.ContentStart, CodeToRun.Document.ContentEnd).Text;
                //acaoAdicionada.CodeScript = richText;
                //acaoAdicionada.Salvar();
                //MessageBox.Show("Nova Ação Adicionada-> " + "[" + acaoAdicionada.TelaPai.SistemaPai.Nome + "] " + acaoAdicionada.TelaPai.Nome + ": " + acaoAdicionada.Id + " - " + acaoAdicionada.Nome);
        
        }

        private void telaCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedTela != null)
            {
                acoes = AcaoDyn_DAO.getAllActionsFromTela(selectedTela);
                acaoGrid.ItemsSource = acoes;
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (selectedAcao == null)
            {
                mainWindow.FlyOutFeedBack("Nenhuma ação selecionada.");
                return;
            }
            mainWindow.OpenAcaoEditChildWindow(this, selectedAcao);

//            mainWindow.FlyOutFeedBack(selectedAcao.Nome + " selecionada.");
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            if ((this.selectedTela != null) && (this.selectedSistema != null))
            {
                mainWindow.OpenAddAcaoChildWindow(this, selectedTela, selectedSistema);
            }
            else
            {
                mainWindow.OpenAddAcaoChildWindow(this);
            }

        }

        private async void ExcluirItem(object sender, RoutedEventArgs e)
        {
            //Verificar se uma ação está selecionada
            if (selectedAcao == null)
            {
                mainWindow.FlyOutFeedBack("Nenhuma ação selecionada.");
                return;
            }


            MessageDialogResult result = await mainWindow.showQuestion("Confirmar Exclusão", "Tem certeza que deseja excluir a ação " + selectedAcao.Nome+ "?");
            if (result == MessageDialogResult.Affirmative)
            {
                selectedAcao.Deletar();
                this.refresh();
                mainWindow.FlyOutFeedBack("Ação Excluída");
            }
            else
            {
                mainWindow.FlyOutFeedBack("Exclusão Cancelada");
            }
            //Confirmar Operação
            //Feedback
        }
    }
}
