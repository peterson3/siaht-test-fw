using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TopDown_QA_FrameWork;
using TopDown_QA_FrameWork.Geradores;
using UI_test_player_TD.Controllers;
using UI_test_player_TD.DB;
using UI_test_player_TD.Model;

namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for TestCaseView.xaml
    /// </summary>
    public partial class TestCaseView : UserControl
    {

        public ObservableCollection<TestCase> testCases { get; set; }
        public TestCase selectedCase { get; set; }
        public ObservableCollection<Tela> Telas { get; set; }
        public bool telasComboIsBeingEditedByUser { get; set; }
        public MainWindow mainWindow;
        public bool Loading { get; set; }
        public ObservableCollection<Sistema> sistemas { get; set; }
        private Sistema _selectedSistema { get; set; }
        public Sistema selectedSistema
        {
            get
            {
                return _selectedSistema;
            } 
            set 
            {
                _selectedSistema = value;
                //testCases = new ObservableCollection<TestCase>(TestCaseServices.getTestCasesInDB(_selectedSistema));

            }
        }
        public ObservableCollection<Navegador> Navegadores { get; set; }
        public Navegador SelectedBrowser { get; set; }


        public TestCaseView(MainWindow mainWindow)
        {
            InitializeComponent();
            testCases = new ObservableCollection<TestCase>();
            this.mainWindow = mainWindow;
            //this.mainWindow.Title = "Caso de Teste";
            this.mainWindow.changeTitle("Caso de Teste");

            Navegadores = new ObservableCollection<Navegador>();
            Navegador navegador = new Navegador();
            navegador.cod = 1;
            navegador.nome = "Internet Explorer";
            //navegador.icon = new Bitmap("Resources/internet-explorer_9-11_64x64.png");
            Navegadores.Add(navegador);

            Navegadores.Add(new Navegador()
            {
                cod = 2,
                nome = "Google Chrome"
            });


            Navegadores.Add(new Navegador()
            {
                cod=3,
                nome = "Mozilla Firefox"
            });

            //Navegadores.Add(new Navegador()
            //{
            //    cod = 4,
            //    nome = "Safari"
            //});

            Navegadores.Add(new Navegador()
            {
                cod = 5,
                nome = "Microsoft Edge"
            });


            //Navegadores.Add(new Navegador()
            //{
            //    cod = 6,
            //    nome = "Opera"
            //});


            navegadorSelectBtn.ItemsSource = Navegadores;

            ctfBox.DataContext = this;
            gridBox.DataContext = this;
            DataContext = this;
            sistemaComboBox.DataContext = this;
            refresh();

            RoutedCommand executeCmd = new RoutedCommand();
            executeCmd.InputGestures.Add(new KeyGesture(Key.Space, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(executeCmd, Run_Test));

            RoutedCommand saveCmd = new RoutedCommand();
            saveCmd.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(saveCmd, salvarCasodeTeste));

            RoutedCommand addPassoCmd = new RoutedCommand();
            addPassoCmd.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(addPassoCmd, adicionarPasso));
        }

        public string getSelectedCTF()
        {
            string selectedCTF = "";
            Dispatcher.Invoke(() =>
            {
                if (this.ctfBox.SelectedItem == null)
                {
                    selectedCTF = "";
                }
                else
                {
                    selectedCTF = ((TestCase)this.ctfBox.SelectedItem).Codigo;
                }
            });
            return selectedCTF;
        }

        public PassoDoTeste getSelectedPasso()
        {
            PassoDoTeste selectedPasso = null;
            Dispatcher.Invoke(() =>
            {
                if (this.gridBox.SelectedItem == null)
                {
                    selectedPasso = null;
                }
                else
                {
                    selectedPasso = ((PassoDoTeste)this.gridBox.SelectedItem);
                }
            });
            return selectedPasso;
        }

        public void setTelaPasso()
        {

        }

        public void updateWindow()
        {
            Dispatcher.Invoke(() =>
            {
                this.ctfBox.Items.Refresh();
                ctfBoxUpdate();
            });
        }

        //deprecated
        private void Run_Test(object sender, RoutedEventArgs e)
        {
            //Verifique se há um sistema selecionado
            if (selectedSistema == null)
            {
                mainWindow.FlyOutFeedBack("Selecione um Sistema");
                return;
            }
            if (String.IsNullOrWhiteSpace(getSelectedCTF()))
            {
                mainWindow.FlyOutFeedBack("Selecione um CTF");
                return;
            }
            //await Task.Run(() =>
            //    test_to_run(this));

            test_to_run(this.mainWindow);
        }

        private async void Run_All_Test(object sender, RoutedEventArgs e)
        {
            //Verifique se há um sistema selecionado
            if (selectedSistema == null)
            {
                mainWindow.FlyOutFeedBack("Selecione um Sistema");
                return;
            }



            for (int i = 0; i < ctfBox.Items.Count; i++)
            {
                ctfBox.SelectedIndex = i;
                ctfBoxUpdate();
                ctfBox.Items.Refresh();

                //Task<int> testReturn =  test_to_run(mainWindow);

                await test_to_run(mainWindow);

            }

            ////Executar todos os testes da lista
            //foreach (TestCase testCase in ctfBox.Items)
            //{
            //    ctfBox.SelectedItem = null;
            //    //ctfBox.SelectedItem = testCase;
            //    //test_to_run(this.mainWindow);
            //}

        }

        private async Task<int> test_to_run(MainWindow gui)
        {
  

            for (int i = 0; i < timesToExecute.Value; i++)
            {
                    if (selectedCase == null)
                    {
                    }
                    else
                    {
                        var controller = await this.mainWindow.ShowProgressAsync("Aguardar Teste...", "Teste em Progresso");
                        controller.SetIndeterminate();
                        await Task.Delay(2000);

                        ////mainGrid.IsEnabled = false;
                        ////progressRing.IsActive = true;
                        //selectedCase.run(Convert.ToInt32(velocSlider.Value));
                        //await controller.CloseAsync();
                       Navegador nav_selecionado = navegadorSelectBtn.SelectedItem as Navegador;
                       selectedCase.run(Convert.ToInt32(velocSlider.Value), nav_selecionado.cod);
                       await controller.CloseAsync();


                    }
            }
            this.updateWindow();
            return 1;

        }

        private async void ctfBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Stopwatch cronometer = new Stopwatch();
            //cronometer.Start();
            //Function to Know Load Time

            ctfBoxUpdate();
            //MessageBox.Show(selectedCase.Codigo + " selecionado");
            gridBox.ItemsSource = selectedCase.Passos;
            selectedCase.GetPassosFromDB();


            //End OF Functions
            //cronometer.Stop();
            //MessageBox.Show("LOAD TIME: " + cronometer.Elapsed.ToString());
        }

        private void ctfBoxUpdate()
        {
            string selectedCtfCod = getSelectedCTF();

           foreach (TestCase tc in testCases)
           {
               if (tc.Codigo == getSelectedCTF())
               {
                   this.selectedCase = tc;
               }
           }
            formBox.DataContext = this.selectedCase;    
            gridBox.ItemsSource = selectedCase.Passos;
            gridBox.Items.Refresh();
        }

        private void abrirCTF(object sender, RoutedEventArgs e)
        {
            if (selectedCase == null)
            {
                this.showMessage("Erro", "Nenhum Teste Selecionado");
            }
            else
            {
                if (File.Exists(selectedCase.CaminhoArquivoCTF))
                {
                    ProcessStartInfo processInfo = new ProcessStartInfo();
                    processInfo.UseShellExecute = true;
                    processInfo.FileName = selectedCase.CaminhoArquivoCTF;
                    Process.Start(processInfo).WaitForExit();
                }
                else
                {
                    this.showMessage("Erro", selectedCase.CaminhoArquivoCTF + " não encontrado");
                }
            }

        }

        public void showMessage(string title, string message)
        {
            this.mainWindow.showMessage(title, message);
        }

        private void adicionarPasso(object sender, RoutedEventArgs e)
        {
            if (selectedCase == null)
            {
                MessageBox.Show("selectedCase == null");
                return;
            }
            if (selectedSistema == null)
            {
                MessageBox.Show("selectedSistema == null");
                return;
            }

           // MessageBox.Show("SISTEMA: " + selectedSistema.Id + " - " + selectedSistema.Nome);
            Tela telaPassoAnterior = null;
            if (selectedCase.Passos.Count != 0)
            {
                
                telaPassoAnterior = selectedCase.Passos.Last().telaSelecionada;
                if (telaPassoAnterior != null)
                {
                    PassoDoTeste passoToBeAdded = new PassoDoTeste(selectedCase)
                    {
                        deveExecutar = true,
                        OrdemSeq = selectedCase.Passos.Count + 1,
                    };

                    selectedCase.Passos.Add(passoToBeAdded);

                    foreach (var item in passoToBeAdded.telasPossiveis)
                    {
                        if (item.Id == telaPassoAnterior.Id)
                        {
                            passoToBeAdded.telaSelecionada = item;
                            break;
                        }
                    }
                }
                else
                {
                    PassoDoTeste passoToBeAdded = new PassoDoTeste(selectedCase)
                    {
                        deveExecutar = true,
                        OrdemSeq = selectedCase.Passos.Count + 1,
                    };

                    selectedCase.Passos.Add(passoToBeAdded);
                }

            }
            else
            {
                PassoDoTeste passoToBeAdded = new PassoDoTeste(selectedCase)
                {
                    deveExecutar = true,
                    OrdemSeq = selectedCase.Passos.Count + 1,
                };

                selectedCase.Passos.Add(passoToBeAdded);
            }
            //selectedCase.Salvar();
        }

        private async void adicionarTestCase(object sender, RoutedEventArgs e)
        {
            if (selectedSistema == null)
            {
                mainWindow.FlyOutFeedBack("Selecione o Sistema para adicionar o Caso de Teste.");
            }
            string nomeCtf;

            MetroDialogSettings st = new MetroDialogSettings();
            st.AffirmativeButtonText = "Ok";
            st.NegativeButtonText = "Cancelar";
            nomeCtf = await this.mainWindow.ShowInputAsync("Adição ", "Insira o CÓDIGO do Caso de Teste", st);

            if (!String.IsNullOrEmpty(nomeCtf))
            {
                bool testExists = false;
                //verificar se o código não existe 
                //
                foreach (TestCase test in testCases)
                {
                    if (test.Codigo.ToUpper() == nomeCtf.ToUpper())
                    {
                        testExists = true;
                    }
                }

                if (testExists == false)
                {
                    var testCaseToAdd = new TestCase(nomeCtf);
                    testCaseToAdd.SistemaPai = selectedSistema;
                    testCaseToAdd.Salvar();
                    this.refresh();
                    await this.mainWindow.ShowMessageAsync("Sucesso!", "Caso de Teste " + nomeCtf.ToUpper() + " adicionado com sucesso!");

                }

                else
                {
                    await this.mainWindow.ShowMessageAsync("Erro", "Caso de Teste não adicionado (Código já existe)");
                }
            }
            else
            {
                await this.mainWindow.ShowMessageAsync("Código Não Informado", "O novoa Caso de Teste não foi adicionado");
            }
        }

        private void salvarCasodeTeste(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrWhiteSpace(getSelectedCTF()))
            {
                mainWindow.FlyOutFeedBack("Selecione um CTF");
                return;
            }
            this.selectedCase.Salvar();
            mainWindow.FlyOutFeedBack("Salvo");
        }

        private void removerPasso(object sender, RoutedEventArgs e)
        {
            this.selectedCase.Passos.Remove(getSelectedPasso());
            selectedCase.ordenarPassos();

        }

        private void abrirLog(object sender, RoutedEventArgs e)
        {
            Logger.abrir();
        }

        private async  void removerTestCase(object sender, RoutedEventArgs e)
        {
           
            MessageDialogResult result = await this.mainWindow.showQuestion("Confirmação", "Tem certeza que deseja remover o CTF " + selectedCase.Codigo +"?");

            if (result == MessageDialogResult.Affirmative)
            {
                selectedCase.DeletarDoBD();
                mainWindow.FlyOutFeedBack(selectedCase.Codigo + " deletado com sucesso.");
                this.refresh();
            }
            else
            {
                mainWindow.FlyOutFeedBack("Operação Cancelada");
            }

            //this.testCases.Remove(selectedCase);
            
        }

        private void lerLoteDeTestes(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Arquivos CSV (.csv) | *.csv";
            dialog.FilterIndex = 1;
            dialog.Multiselect = false;
            dialog.ShowDialog();
            string fileName = "";

            if (dialog.FileNames.Length <= 0)
            {
                return;
            }
            if (dialog.FileNames.Length > 0)
            {
                fileName = dialog.FileNames[0];
            }

            FileInfo file = new FileInfo(fileName);
            if (selectedCase == null)
            {
                MessageBox.Show("Sem Caso de Teste Selecionado");
                return;
            }


            selectedCase.execFromFile(file, Convert.ToInt32(velocSlider.Value), 1);

        }

        private void salvarEmArqLote(object sender, RoutedEventArgs e)
        {
            //Usuário seleciona Arquivo
            //Verificar a integridade do arquivo (bate com as condições do teste) (?)
            //Salvar Ao Final do Arquivo
            //Informar ao Usuário


            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Arquivos CSV (.csv) | *.csv";
            dialog.FilterIndex = 1;
            dialog.FileName = this.selectedCase.Codigo+"-data.csv";
            dialog.ShowDialog();
            dialog.CreatePrompt = true;
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            string fileName = dialog.FileName;
            
            
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
            {
                using (file.Create())
                {
                    //Cria arquivo
                }
                     
                        //Adicionar Nova Linha
                        int qtdPassos = selectedCase.Passos.Count;
                        int qtdParametros = 0;
                        int qtdTestes = 0;

                   
                        StreamReader reader = new StreamReader(file.FullName, System.Text.Encoding.Default);
                        string result = reader.ReadToEnd();
                        reader.Close();


                        StreamWriter writer = new StreamWriter(file.FullName, true, System.Text.Encoding.Default);
                        if (String.IsNullOrWhiteSpace(result))
                        {
                            //Não Pula Linha
                        }
                        else
                        {
                            //Pula Linha
                            writer.WriteLine();
                        }
                        for (int i = 0; i < selectedCase.Passos.Count; i++)
                        {
                            writer.Write(selectedCase.Passos[i].Parametro);
                            if (i + 1 >= selectedCase.Passos.Count)
                            {
                                //É o último índice
                            }
                            else
                            {
                                writer.Write(";");
                            }
                        }

                        writer.Close();
                        MessageBox.Show("Arquivo Criado e Parâmetros Gravados!");
                

            }

            else
            {
                MessageBoxResult dialogResult = MessageBox.Show("Adicionar Nova Linha = Sim; Sobreescrever Arquivo = Não", "Adicionar Nova Linha de Parâmetros?", MessageBoxButton.YesNoCancel);
                if (dialogResult == MessageBoxResult.Cancel)
                {
                    //do Nth
                    MessageBox.Show("Operação Cancelada!");
                    return;
                }
                else
                {
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        //Adicionar Nova Linha
                        int qtdPassos = selectedCase.Passos.Count;
                        int qtdParametros = 0;
                        int qtdTestes = 0;


                        StreamReader reader = new StreamReader(file.FullName, System.Text.Encoding.Default);
                        string result = reader.ReadToEnd();
                        reader.Close();


                        StreamWriter writer = new StreamWriter(file.FullName, true, System.Text.Encoding.Default);
                        if (String.IsNullOrWhiteSpace(result))
                        {
                            //Não Pula Linha
                        }
                        else
                        {
                            //Pula Linha
                            writer.WriteLine();
                        }
                        for (int i = 0; i < selectedCase.Passos.Count; i++)
                        {
                            writer.Write(selectedCase.Passos[i].Parametro);
                            if (i + 1 >= selectedCase.Passos.Count)
                            {
                                //É o último índice
                            }
                            else
                            {
                                writer.Write(";");
                            }
                        }

                        writer.Close();
                        MessageBox.Show("Nova Linha da Parâmetros adicionada");
                    }
                    else
                    {
                        if (dialogResult == MessageBoxResult.No)
                        {
                            //Sobreescrever Arquivo
                            MessageBoxResult dialogResult2 = MessageBox.Show("Tem certeza que deseja sobreescrever o arquivo?", "Confirmação de Operação", MessageBoxButton.YesNoCancel);
                            if (dialogResult2 == MessageBoxResult.Cancel)
                            {
                                MessageBox.Show("Operação Cancelada!");
                            }
                            else{

                                if (dialogResult2 == MessageBoxResult.No)
                                {
                                    MessageBox.Show("Operação Cancelada!");

                                }
                                else
                                {
                                    if (dialogResult2 == MessageBoxResult.Yes)
                                    {
   
                                        //Adicionar Nova Linha
                                        int qtdPassos = selectedCase.Passos.Count;
                                        int qtdParametros = 0;
                                        int qtdTestes = 0;


                                        StreamReader reader = new StreamReader(file.FullName, System.Text.Encoding.Default);
                                        string result = reader.ReadToEnd();
                                        reader.Close();


                                        StreamWriter writer = new StreamWriter(file.FullName, false, System.Text.Encoding.Default);

                                        for (int i = 0; i < selectedCase.Passos.Count; i++)
                                        {
                                            writer.Write(selectedCase.Passos[i].Parametro);
                                            if (i + 1 >= selectedCase.Passos.Count)
                                            {
                                                //É o último índice
                                            }
                                            else
                                            {
                                                writer.Write(";");
                                            }
                                        }

                                        writer.Close();
                                        // Sobreescrever Arquivo
                                        MessageBox.Show("Arquivo Sobreescrito!");
                                    }
                                }
                            }
                        }
                    }
                }
            }
           

        }

        private void selectedSistema_Changed(object sender, SelectionChangedEventArgs e)
        {
            this.selectedCase = new TestCase("-");

            this.ctfBox.ItemsSource = null;

            if (selectedSistema != null)
            {
                //mainWindow.FlyOutFeedBack("Sistema = " + selectedSistema.Id.ToString() + "-" + selectedSistema.Nome + " " + selectedSistema.homeURL);

                 this.testCases = selectedSistema.getTestCasesFromDb();
                 this.ctfBox.ItemsSource = testCases;
            }
            else
            {
                //mainWindow.FlyOutFeedBack("selectedSistema == null");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s="";

            if (selectedCase == null)
            {
                MessageBox.Show("Selected Case == null");
                return;
            }

            foreach (PassoDoTeste passo in selectedCase.Passos)
            {
                s += passo.OrdemSeq + " - " + passo.telaSelecionada.Nome + " - " + passo.acaoSelecionada.Nome + " - " + passo.acaoSelecionada.CodeScript;
                s += Environment.NewLine;
            }
            MessageBox.Show(s);
        }

        public void refresh()
        {
            //this.cleanData();
            this.loadData();
        }

        public void loadData()
        {
            string oldSelectedTestCase = getSelectedCTF();
            Sistema oldSelectedSistema = selectedSistema;

            this.cleanData();

            sistemas = Sistema_DAO.getAllSistemas();
            sistemaComboBox.ItemsSource = sistemas;

            if (oldSelectedSistema != null)
            {
                foreach (Sistema item in sistemaComboBox.Items)
                {
                    if (item.Id == oldSelectedSistema.Id)
                    {
                        sistemaComboBox.SelectedItem = item;
                    }
                }
            }

            if (oldSelectedTestCase != null)
            {
                foreach (TestCase item in ctfBox.Items)
                {
                    if (item.Codigo == oldSelectedTestCase)
                    {
                        ctfBox.SelectedValue = item;
                    }
                }
            }

            //sistemaComboBox.SelectedItem = selectedSistema;
            //selectedSistema = sistemas[0];
        }

        public void cleanData()
        {
            this.selectedCase = new TestCase("-");

            this.ctfBox.ItemsSource = null;
        }

        private void subirPosPasso(object sender, RoutedEventArgs e)
        {
            if (selectedCase == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum caso de teste selecionado");
                return;
            }
            if (getSelectedPasso() == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum Passo do teste selecionado");
                return;
            }

            //Ordenando
            int i = 1;
            foreach (PassoDoTeste passo in selectedCase.Passos)
            {
                passo.OrdemSeq = i;
                i++;
            }

            //ìndice do Passo desejado
            int passoIndex = selectedCase.Passos.IndexOf(getSelectedPasso());

            //Guardando o Antigo dono da posição
            try
            {
                PassoDoTeste passoAntigo = selectedCase.Passos.ElementAt(passoIndex - 1);
                PassoDoTeste passoInteresse = selectedCase.Passos.ElementAt(passoIndex);

                //Movendo
                selectedCase.Passos.Remove(passoAntigo);


                //Inserindo o Novo Na Nova Posição
                selectedCase.Passos.Insert(selectedCase.Passos.IndexOf(passoInteresse) + 1, passoAntigo);



                //Ordenando
                i = 1;
                foreach (PassoDoTeste passo in selectedCase.Passos)
                {
                    passo.OrdemSeq = i;
                    if (getSelectedPasso() == passo)
                    {
                        getSelectedPasso().OrdemSeq = i;
                    }
                    i++;

                }
            }
            catch (Exception)
            {

            }
            


        }

        private void descerPosPasso(object sender, RoutedEventArgs e)
        {
            if (selectedCase == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum caso de teste selecionado");
                return;
            }
            if (getSelectedPasso() == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum Passo do teste selecionado");
                return;
            }

            //Ordenando
            int i = 1;
            foreach (PassoDoTeste passo in selectedCase.Passos)
            {
                passo.OrdemSeq = i;
                i++;
            }

            //ìndice do Passo desejado
            int passoIndex = selectedCase.Passos.IndexOf(getSelectedPasso());

            //Guardando o Antigo dono da posição
            try
            {
                PassoDoTeste passoAntigo = selectedCase.Passos.ElementAt(passoIndex +1);
                PassoDoTeste passoInteresse = selectedCase.Passos.ElementAt(passoIndex);

                //Movendo
                selectedCase.Passos.Remove(passoAntigo);


                //Inserindo o Novo Na Nova Posição
                selectedCase.Passos.Insert(selectedCase.Passos.IndexOf(passoInteresse), passoAntigo);


                //Ordenando
                i = 1;
                foreach (PassoDoTeste passo in selectedCase.Passos)
                {
                    passo.OrdemSeq = i;
                    if (getSelectedPasso() == passo)
                    {
                        getSelectedPasso().OrdemSeq = i;
                    }
                    i++;

                }
            }
            catch (Exception)
            {

            }



        }

        private async void copiarTeste(object sender, RoutedEventArgs e)
        {
            //Mesmos passos de adicionar um novo teste
            if (selectedCase == null)
            {
                mainWindow.FlyOutFeedBack("Selecione um caso de teste para ser copiado.");
                return;
            }

            string nomeCtf;

            MetroDialogSettings st = new MetroDialogSettings();
            st.AffirmativeButtonText = "Ok";
            st.NegativeButtonText = "Cancelar";
            nomeCtf = await this.mainWindow.ShowInputAsync("Copiar ", "Insira o CÓDIGO do NOVO Caso de Teste", st);

            if (!String.IsNullOrEmpty(nomeCtf))
            {
                bool testExists = false;
                //verificar se o código não existe 
                //
                foreach (TestCase test in testCases)
                {
                    if (test.Codigo.ToUpper() == nomeCtf.ToUpper())
                    {
                        testExists = true;
                    }
                }

                if (testExists == false)
                {
                    TestCase_DAO.CopyTestCase(nomeCtf, selectedCase);
                    //testCaseToAddPassos.Salvar();
                    Sistema sisTemp = this.selectedSistema;
                    await this.mainWindow.ShowMessageAsync("Sucesso!", "Caso de Teste " + nomeCtf.ToUpper() + " adicionado com sucesso!");
                    this.refresh();
                    foreach (Sistema item in this.sistemaComboBox.Items)
                    {
                        if (item.Id == sisTemp.Id)
                        {
                            this.selectedSistema = item;
                            this.sistemaComboBox.SelectedItem = this.selectedSistema;
                            break;
                        }
                    }


                }

                else
                {
                    await this.mainWindow.ShowMessageAsync("Erro", "Caso de Teste não adicionado (Código já existe)");
                }
            }
            else
            {
                await this.mainWindow.ShowMessageAsync("Código Não Informado", "O novoa Caso de Teste não foi adicionado");
            }
        }

        private void editarAcao(object sender, RoutedEventArgs e)
        {
            if (getSelectedPasso() == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum Passo Selecionado.");
                return;
            }
            mainWindow.OpenAcaoEditChildWindow(this, getSelectedPasso().acaoSelecionada);
        }

        private void adicionarPassoAbaixo(object sender, RoutedEventArgs e)
        {
            if (selectedCase == null)
            {
                MessageBox.Show("selectedCase == null");
                return;
            }
            if (selectedSistema == null)
            {
                MessageBox.Show("selectedSistema == null");
                return;
            }


            // MessageBox.Show("SISTEMA: " + selectedSistema.Id + " - " + selectedSistema.Nome);
            int posPasso = selectedCase.Passos.IndexOf(getSelectedPasso())  + 1;

            selectedCase.Passos.Insert(posPasso, new PassoDoTeste(selectedCase)
            {
                deveExecutar = true,
                OrdemSeq = selectedCase.Passos.Count + 1,
            });

            selectedCase.ordenarPassos();
            
        }

        private void abrirDetalhesPasso(object sender, RoutedEventArgs e)
        {
            PassoDoTeste passoSelecionado = getSelectedPasso();

            if (passoSelecionado == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum Passo Selecionado.");
                return;
            }
            mainWindow.OpenDetalhesPasso(this, passoSelecionado);
        }

        private void ordernarCTFporNome(object sender, RoutedEventArgs e)
        {
            this.ctfBox.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Codigo", System.ComponentModel.ListSortDirection.Ascending));
        }

        private void testBaloon(object sender, RoutedEventArgs e)
        {
            Tools.ToolTipSystemTray.showAprBalloon("a", "b");
        }

        private void novaAcao(object sender, RoutedEventArgs e)
        {
            if (getSelectedPasso() == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum Passo Selecionado.");
                return;
            }

            if (getSelectedPasso().telaSelecionada == null)
            {
                mainWindow.FlyOutFeedBack("Nenhuma Tela Selecionado.");
                return;
            }

            mainWindow.OpenAddAcaoChildWindow(this, getSelectedPasso().telaSelecionada, selectedSistema);


            ////Recarregar as acoes Possiveis
            //foreach (var item in this.selectedCase.Passos){

            //}
            this.selectedCase.Salvar();
            this.gridBox.UpdateLayout();
            this.refresh();
            this.mainWindow.FlyOutFeedBack("re");
            ////Selecionar a Nova Acao Adicionada
            //getSelectedPasso().acaoSelecionada = novaAcaoAdicionada;
        }

        private void abrirPastaCasosTeste(object sender, RoutedEventArgs e)
        {
               ProcessStartInfo processInfo = new ProcessStartInfo();
               processInfo.UseShellExecute = true;
               processInfo.FileName = Settings.ctfsPath;
               Process.Start(processInfo);
        }

        private async void enviarTestCaseViaEmail(object sender, RoutedEventArgs e)
        {
            // A Principio setado para enviar o caso de teste em anexo
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("peterson@topdown.com.br", "humanbeing123");
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("peterson@topdown.com.br");

            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            //smtpClient.Port = 465;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = basicCredential;
            smtpClient.Timeout = 500000;

            message.From = fromAddress;
            message.Subject = "Relatório de Execução de Caso de Teste";
            //Set IsBodyHtml to true means you can send HTML email.
            message.IsBodyHtml = true;

            message.Body = @"<h1>Relatório do Caso de Teste</h1></br> 
                             
                            <h2>Sistema: " + selectedSistema.Nome + @"</h2></br>" +
                          "<h2>Caso: " + this.selectedCase.Codigo.ToUpper() + @"</h2></br>";

            message.Body += @"<h5> Hora da Execução: " + this.selectedCase.UltimaVezExecutado.ToString() + "</h5></br>";
            //message.Body += @"<h5> Tempo de Execução: " + this.selectedCase.t.ToString() + "</h5></br>";
            message.Body += @"<h5> Quantidade de Passos: " + this.selectedCase.Passos.Count.ToString() + "</h5></br>";

//              message.Body += @"<table style=' font-family: arial, sans-serif; border-collapse: collapse; width: 100%;'> 
//                                <tr> 
//                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>Código</th> 
//                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>Nome</th> 
//                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>APR/TOTAL IE</th> 
//                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>APR/TOTAL FFOX</th>
//                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>APR/TOTAL CHROME</th>
//                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>APR/TOTAL EDGE</th>
//                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>INFO</th>
//                                </tr>";


//            foreach (PassoDoRoteiro passo in  this.SelectedSuite.PassosDoRoteiro)
//            {
//                message.Body += "<tr>";
//                message.Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.SelectedCase.Codigo + "</td>";
//                message.Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.SelectedCase.Nome + "</td>";
//                message.Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.ie_stats + "</td>";
//                message.Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.ffox_stats + "</td>";
//                message.Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.chrome_stats + "</td>";
//                message.Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.edge_stats + "</td>";
//                message.Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.Obs + "</td>";
//                message.Body += "</tr>";
//                //Attachment anexo;
//                //anexo = new Attachment(passo.SelectedCase.CaminhoArquivoCTF);
//                //message.Attachments.Add(anexo);

//            }

            //Attachment anexo;
            var anexo = new Attachment(this.selectedCase.CaminhoArquivoCTF);
            message.Attachments.Add(anexo);

            //message.Body += "</table>";
            MetroDialogSettings st = new MetroDialogSettings();
            st.AffirmativeButtonText = "Ok";
            st.NegativeButtonText = "Cancelar";
            string emailDest = await this.mainWindow.ShowInputAsync("Email", "Insira o EMAIL para envio do Caso de Teste", st);

            if (!String.IsNullOrEmpty(emailDest))
            {
                message.To.Add(emailDest);
                try
                {
                    smtpClient.Send(message);
                    await this.mainWindow.ShowMessageAsync("Sucesso", "Mensagem Enviada para "+ emailDest + ".");
                    //MessageBox.Show("Mensagem Enviada");
                }
                catch (Exception ex)
                {
                    //Error, could not send the message
                    this.mainWindow.ShowMessageAsync("Erro", "Detalhes: " + ex.Message + ex.Source + ex.StackTrace);
                }
            }
            else
            {
                await this.mainWindow.ShowMessageAsync("Erro", "Insira o email.");
            }


        }

        private async void editarCodigoTestCase(object sender, RoutedEventArgs e)
        {
            MetroDialogSettings st = new MetroDialogSettings();
            st.AffirmativeButtonText = "Ok";
            st.NegativeButtonText = "Cancelar";
            st.DefaultText = selectedCase.Codigo;
            string nomeTemp = await this.mainWindow.ShowInputAsync("Alterar Código do Caso de Teste", "Insira Novo Código do Caso de Teste", st);
            if (!String.IsNullOrEmpty(nomeTemp))
            {

                selectedCase.Codigo = nomeTemp;
                selectedCase.Salvar();
                await this.mainWindow.ShowMessageAsync("Sucesso!", "Código Do Caso de Teste alterado com sucesso!");

            }
            else
            {
                await this.mainWindow.ShowMessageAsync("Novo Código Não Informado", "O Código do Caso não foi alterado");
            }
        }



    }
}
