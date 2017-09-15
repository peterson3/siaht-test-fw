using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using TopDown_QA_FrameWork;
using UI_test_player_TD.Controllers;
using UI_test_player_TD.DB;
using UI_test_player_TD.Model;
using System.Net.Mail;
using System.Net;
using SharpCompress.Writer;
using SharpCompress.Common;


namespace UI_test_player_TD.Views
{
    /// <summary>
    /// Interaction logic for TestSuiteView.xaml
    /// </summary>
    public partial class TestSuiteView : UserControl
    {
        public MainWindow mainWindow;
        public ObservableCollection<TestSuite> TestSuites { get; set; }
        public TestSuite SelectedSuite { get; set; }
        public ObservableCollection<Sistema> sistemas { get; set; }
        public Sistema selectedSistema { get; set; }
        public TestSuiteView(MainWindow mainWindow )
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            testSuiteBox.DataContext = this;

            //TestSuites = new ObservableCollection<TestSuite>(TestSuite.getTestSuitesExample());
            sistemas = Sistema_DAO.getAllSistemas();
            sistemaComboBox.ItemsSource = sistemas;
            DataContext = this;
        }

        private void testSuiteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Teste
            //MessageBox.Show(this.SelectedSuite.Codigo);
            //foreach (TestCase testcase in SelectedSuite.TestCases)
            //{
            //    MessageBox.Show(testcase.Codigo);
            //}

            this.testSuiteGrid.DataContext = this;

            if (SelectedSuite == null)
            {
                this.testSuiteGrid.ItemsSource = null;
            }
            else
            {
                this.testSuiteGrid.ItemsSource = SelectedSuite.PassosDoRoteiro;
                //MessageBox.Show(selectedCase.Codigo + " selecionado");

                SelectedSuite.GetPassosFromDB();

                this.test_desc_txt.DataContext = SelectedSuite;
            }
        }

        private void selectedSistema_Changed(object sender, SelectionChangedEventArgs e)
        {

            if (selectedSistema != null)
            {
                //mainWindow.FlyOutFeedBack("Sistema = " + selectedSistema.Id.ToString() + "-" + selectedSistema.Nome + " " + selectedSistema.homeURL);

                this.TestSuites = selectedSistema.getTestSuitesFromDB(selectedSistema);
                this.testSuiteBox.ItemsSource = TestSuites ;
            }
            else
            {
                //mainWindow.FlyOutFeedBack("selectedSistema == null");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
            if (SelectedSuite == null)
            {
                MessageBox.Show("Sem Roteiro de Teste Selecionado");
                return;
            }




            ////Descobrir qual é a linha referente ao Botão 

            PassoDoRoteiro fonte = ((FrameworkElement)sender).DataContext as PassoDoRoteiro;
            //if (fonte == null)
            //{
            //    MessageBox.Show("null");
            //}
            //else
            //{
            //    //foreach (TestCase possibilit in fonte.TestCasePossiveis)
            //    //{
            //    //    MessageBox.Show(possibilit.Codigo);
            //    //}
            //    if (fonte.SelectedCase != null)
            //      MessageBox.Show(fonte.SelectedCase.Codigo);
            //    else
            //    {
            //        MessageBox.Show("Sem caso de Teste Selecionado");

            //    }

            //}

            fonte.SelectedArq = new FileInfo(fileName);



        }

        private void Run_Test(object sender, RoutedEventArgs e)
        {
            
            if (SelectedSuite == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum Roteiro Selecionado");
                return;
            }

            if (!ie_check.IsChecked.Value && !ffox_check.IsChecked.Value && !chrome_check.IsChecked.Value && !edge_check.IsChecked.Value)
            {
                mainWindow.FlyOutFeedBack("Nenhum Browser Selecionado");
                return;
            }
            this.SelectedSuite.horaExecucao = DateTime.Now;
            Stopwatch cronometro = new Stopwatch();
            cronometro.Start();
       
            bool ok = false;
            int numPassosSelecionadosParaExecutar = 0;
            int numLinhas;
            //Verificação
            // -- Todos os dados preenchidos
            foreach (PassoDoRoteiro passoDoRoteiro in SelectedSuite.PassosDoRoteiro)
            {
                if (passoDoRoteiro.deveExecutar)
                {
                    if ((passoDoRoteiro.SelectedArq != null) && (passoDoRoteiro.SelectedCase != null))
                    {
                        ok = true;
                    }

                    else
                    {
                        ok = false;
                    }

                    numPassosSelecionadosParaExecutar++;
                    passoDoRoteiro.zerarStats();
                }
                
            }

            // -- Todos Arquivos tem a mesma quantidade de linhas 

            if (numPassosSelecionadosParaExecutar == 0)
            {
                MessageBox.Show("Selecione Pelo Menos um Passo do Roteiro para ser Executado");
                return;
            }
            if (ok == false)
            {
                MessageBox.Show("Preencher os Dados da Grid dos que serão executados");
                return;
            }


            //**DESCOMENTAR SE QUISER ARQUIVOS COM MESMA QUANTIDADE DE LINHA***
            //numLinhas = File.ReadAllLines(SelectedSuite.PassosDoRoteiro[0].SelectedArq.FullName, Encoding.Default).Length;

            //bool numLinhasDif = false;
            //foreach (PassoDoRoteiro passoDoRoteiro in SelectedSuite.PassosDoRoteiro)
            //{
            //    if (numLinhas != File.ReadAllLines(passoDoRoteiro.SelectedArq.FullName, Encoding.Default).Length)
            //    {
            //        numLinhasDif = true;
            //    }
            //}

            ////P/ cada arquivo 


            //if (numLinhasDif == true)
            //{
            //    MessageBox.Show("Arquivos com quantidades de linhas diferentes");
            //}


            //Verificar se as colunas batem c/ as quantidades de passos de cada ctf
            bool numColErr = false;

            //Verificação do Numero de Linhas dos Arquivos de Parametro
            foreach (PassoDoRoteiro passoDoRoteiro in SelectedSuite.PassosDoRoteiro)
            {
                if (passoDoRoteiro.deveExecutar)
                {
                    int qtdPasso = passoDoRoteiro.SelectedCase.Passos.Count;
                    //Ler quantidade de Colunas em cada linha de cada arquivo

                    StreamReader reader = new StreamReader(passoDoRoteiro.SelectedArq.FullName, System.Text.Encoding.Default);
                    while (!reader.EndOfStream)
                    {
                        string linha = reader.ReadLine();
                        string[] parametros;
                        int qtdParametros;

                        passoDoRoteiro.SelectedCase.GetPassosFromDB();
                        int qtdPassos = passoDoRoteiro.SelectedCase.Passos.Count;

                        parametros = linha.Split(';');
                        qtdParametros = parametros.Length;

                        if (qtdParametros != qtdPassos)
                        {
                            passoDoRoteiro.Obs = "Linha no Arquivo com Quantidade de Parâmetros diferente do Necessário. (Necessário: " + qtdPassos.ToString() + ", quantidade na linha: " + qtdParametros + ")";
                            reader.Close();
                            return;
                        }
                        else
                        {
                            numColErr = false;
                        }
                    }

                }
                
            }

            foreach (PassoDoRoteiro passoDoRoteiro in SelectedSuite.PassosDoRoteiro)
            {
                if (passoDoRoteiro.deveExecutar)
                {


                    int qtdPasso = passoDoRoteiro.SelectedCase.Passos.Count;
                    //Ler quantidade de Colunas em cada linha de cada arquivo

                    StreamReader reader = new StreamReader(passoDoRoteiro.SelectedArq.FullName, System.Text.Encoding.Default);
                    while (!reader.EndOfStream)
                    {
                        string linha = reader.ReadLine();
                        string[] parametros;
                        int qtdParametros;

                        passoDoRoteiro.SelectedCase.GetPassosFromDB();
                        int qtdPassos = passoDoRoteiro.SelectedCase.Passos.Count;

                        parametros = linha.Split(';');
                        qtdParametros = parametros.Length;
                        //a ultima string pode ser jogada fora

                        if (qtdParametros != qtdPassos)
                        {
                            passoDoRoteiro.Obs = "Linha no Arquivo com Quantidade de Parâmetros diferente do Necessário. (Necessário: " + qtdPassos.ToString() + ", quantidade na linha: " + qtdParametros + ")";
                            reader.Close();
                            return;
                        }
                        else
                        {
                            // MessageBox.Show("Qtd Parametros " + qtdParametros.ToString() + Environment.NewLine + "Quantidade de Passos " + qtdPassos.ToString() );
                            numColErr = false;
                        }

                        //    for (int i = 0; i < qtdPassos; i++)
                        //   {
                        //        selectedCase.Passos[i].Parametro = parametros[i];
                        //    }
                        //    test_to_run(this.mainWindow);
                        //    qtdTestes++;
                        //    //Copiar o CTF gerado para uma pasta qualquer renomeando para número da linha
                        //    File.Copy(selectedCase.CaminhoArquivoCTF, selectedCase.CaminhoArquivoCTF.Replace(".xls", "_" + qtdTestes) + ".xls", true);
                    }

                    if (numColErr == false)
                    {
                        //TODO: 
                        //Todas as verificações feitas sem erros
                        //Executar o TestSuite
                        //foreach (PassoDoRoteiro ctfDoRoteiro in SelectedSuite.PassosDoRoteiro)
                        //{
                        //    ctfDoRoteiro.SelectedCase.GetPassosFromDB();
                        //    ctfDoRoteiro.run(1);
                        //}
                        //Recuperar o CTF e executar os passos com o parâmetros recuperados do arquivo setado.
                        //Atualizar o Status de cada ctf executado
                        //Atualizar o Status do roteiro.

                        //Como gerar um ctf apenas? -.-.-.-.-.-.-

                        if (ie_check.IsChecked.Value)
                        {
                            passoDoRoteiro.run(0, Browser.IE_BROWSER);
                        }
                        if (ffox_check.IsChecked.Value)
                        {
                            passoDoRoteiro.run(0, Browser.FIREFOX_BROWSER);
                        }
                        if (chrome_check.IsChecked.Value)
                        {
                            passoDoRoteiro.run(0, Browser.CHROME_BROWSER);
                        }
                        if (edge_check.IsChecked.Value)
                        {
                            passoDoRoteiro.run(0, Browser.EDGE_BROWSER);
                        }


                    }
                    reader.Close();
                }
                else
                {
                    passoDoRoteiro.Obs = "NÃO EXECUTADO";
                }

            }
            cronometro.Stop();
            this.SelectedSuite.tempoExecucao = cronometro.Elapsed;
        }

        private void adicionarPasso(object sender, RoutedEventArgs e)
        {
            if (SelectedSuite == null)
            {
                MessageBox.Show("selectedSuite == null");
                return;
            }
            if (selectedSistema == null)
            {
                MessageBox.Show("selectedSistema == null");
                return;
            }

            // MessageBox.Show("SISTEMA: " + selectedSistema.Id + " - " + selectedSistema.Nome);

            SelectedSuite.PassosDoRoteiro.Add(new PassoDoRoteiro(SelectedSuite)
            {
                Ordem = SelectedSuite.PassosDoRoteiro.Count + 1,

            });
            //selectedCase.Salvar();
        }

        private void removerPasso(object sender, RoutedEventArgs e)
        {
            this.SelectedSuite.PassosDoRoteiro.Remove(getSelectedPasso());
            this.ordenarPassos();

        }

        public PassoDoRoteiro getSelectedPasso()
        {
            PassoDoRoteiro selectedPasso = null;
            Dispatcher.Invoke(() =>
            {
                if (this.testSuiteGrid.SelectedItem == null)
                {
                    selectedPasso = null;
                }
                else
                {
                    selectedPasso = ((PassoDoRoteiro)this.testSuiteGrid.SelectedItem);
                }
            });
            return selectedPasso;
        }

        private async void adicionarTestSuite(object sender, RoutedEventArgs e)
        {
            if (selectedSistema == null)
            {
                mainWindow.FlyOutFeedBack("Selecione o Sistema para adicionar o Caso de Teste.");
                return;
            }
            string nomeRtf;

            MetroDialogSettings st = new MetroDialogSettings();
            st.AffirmativeButtonText = "Ok";
            st.NegativeButtonText = "Cancelar";
            nomeRtf = await this.mainWindow.ShowInputAsync("Adição ", "Insira o CÓDIGO do Caso de Teste", st);

            if (!String.IsNullOrEmpty(nomeRtf))
            {
                bool testExists = false;
                //verificar se o código não existe 
                //
                foreach (TestSuite testSuite in TestSuites)
                {
                    if (testSuite.Codigo.ToUpper() == nomeRtf.ToUpper())
                    {
                        testExists = true;
                    }
                }

                if (testExists == false)
                {
                    var testSuiteToAdd = new TestSuite(nomeRtf, selectedSistema);
                    testSuiteToAdd.Salvar();
                    this.refresh();
                    await this.mainWindow.ShowMessageAsync("Sucesso!", "Caso de Teste " + nomeRtf.ToUpper() + " adicionado com sucesso!");

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

        public void refresh()
        {
            //TODO
        }

        private async void removerTestSuite(object sender, RoutedEventArgs e)
        {

            MessageDialogResult result = await this.mainWindow.showQuestion("Confirmação", "Tem certeza que deseja remover o RTF " + SelectedSuite.Codigo + "? " + SelectedSuite.Id.ToString());

            if (result == MessageDialogResult.Affirmative)
            {
                SelectedSuite.DeletarDoBD();
                mainWindow.FlyOutFeedBack(SelectedSuite.Codigo + " deletado com sucesso.");
                this.refresh();
            }
            else
            {
                mainWindow.FlyOutFeedBack("Operação Cancelada");
            }

            //this.testCases.Remove(selectedCase);

        }

        private void salvarRoteirodeTeste(object sender, RoutedEventArgs e)
        {

            this.SelectedSuite.Salvar();
            mainWindow.FlyOutFeedBack("Salvo");
        }

        private void abrirRTF(object sender, RoutedEventArgs e)
        {

        }

        private void subirPosPasso(object sender, RoutedEventArgs e)
        {
            if (SelectedSuite == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum roteiro de teste selecionado");
                return;
            }
            if (getSelectedPasso() == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum Passo do roteiro selecionado");
                return;
            }

            //Ordenando
            int i = 1;
            foreach (PassoDoRoteiro passo in SelectedSuite.PassosDoRoteiro)
            {
                passo.Ordem = i;
                i++;
            }

            //ìndice do Passo desejado
            int passoIndex = SelectedSuite.PassosDoRoteiro.IndexOf(getSelectedPasso());

            //Guardando o Antigo dono da posição
            try
            {
                PassoDoRoteiro passoAntigo = SelectedSuite.PassosDoRoteiro.ElementAt(passoIndex - 1);
                PassoDoRoteiro passoInteresse = SelectedSuite.PassosDoRoteiro.ElementAt(passoIndex);

                //Movendo
                SelectedSuite.PassosDoRoteiro.Remove(passoAntigo);


                //Inserindo o Novo Na Nova Posição
                SelectedSuite.PassosDoRoteiro.Insert(SelectedSuite.PassosDoRoteiro.IndexOf(passoInteresse) + 1, passoAntigo);



                //Ordenando
                i = 1;
                foreach (PassoDoRoteiro passo in SelectedSuite.PassosDoRoteiro)
                {
                    passo.Ordem = i;
                    if (getSelectedPasso() == passo)
                    {
                        getSelectedPasso().Ordem = i;
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
            if (SelectedSuite == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum roteiro de teste selecionado");
                return;
            }
            if (getSelectedPasso() == null)
            {
                mainWindow.FlyOutFeedBack("Nenhum Passo do roteiro selecionado");
                return;
            }

            //Ordenando
            int i = 1;
            foreach (PassoDoRoteiro passo in SelectedSuite.PassosDoRoteiro)
            {
                passo.Ordem = i;
                i++;
            }

            //ìndice do Passo desejado
            int passoIndex = SelectedSuite.PassosDoRoteiro.IndexOf(getSelectedPasso());

            //Guardando o Antigo dono da posição
            try
            {
                PassoDoRoteiro passoAntigo = SelectedSuite.PassosDoRoteiro.ElementAt(passoIndex + 1);
                PassoDoRoteiro passoInteresse = SelectedSuite.PassosDoRoteiro.ElementAt(passoIndex);

                //Movendo
                SelectedSuite.PassosDoRoteiro.Remove(passoAntigo);


                //Inserindo o Novo Na Nova Posição
                SelectedSuite.PassosDoRoteiro.Insert(SelectedSuite.PassosDoRoteiro.IndexOf(passoInteresse), passoAntigo);


                //Ordenando
                i = 1;
                foreach (var passo in SelectedSuite.PassosDoRoteiro)
                {
                    passo.Ordem = i;
                    if (getSelectedPasso() == passo)
                    {
                        getSelectedPasso().Ordem = i;
                    }
                    i++;

                }
            }
            catch (Exception)
            {

            }



        }

        public void ordenarPassos()
        {
            //Ordenando
            int i = 1;
            foreach (var passo in SelectedSuite.PassosDoRoteiro)
            {
                passo.Ordem = i;
                i++;
            }

            this.SelectedSuite.PassosDoRoteiro.OrderBy(passo => passo.Ordem);
        }

        private async void enviarEmail(object sender, RoutedEventArgs e)
        {
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
            message.Subject = "Relatório de Execução de Roteiro";
            //Set IsBodyHtml to true means you can send HTML email.
            message.IsBodyHtml = true;

            message.Body = this.gerarMensagemRelatorioHTML();
//            message.Body = @"<h1>Relatório</h1></br> 
//                             
//                            <h2>Sistema: " + selectedSistema.Nome + @"</h2></br>" +
//                          "<h2>Roteiro: " + this.SelectedSuite.Codigo.ToUpper() + @"</h2></br>";

//            message.Body += @"<h5> Hora da Execução: " + this.SelectedSuite.horaExecucao.ToString() + "</h5></br>";
//            message.Body += @"<h5> Tempo de Execução: " + this.SelectedSuite.tempoExecucao.ToString() + "</h5></br>";
//            message.Body += @"<h5> Quantidade de Testes: " + this.SelectedSuite.PassosDoRoteiro.Count.ToString() + "</h5></br>";

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

//            //Create Zip File

//              //using (var rar = File.OpenWrite(this.SelectedSuite.Codigo+".zip"))
//              //{
//              //    using (var rarWriter = WriterFactory.Open(rar, ArchiveType.Zip, new CompressionInfo() { DeflateCompressionLevel = SharpCompress.Compressor.Deflate.CompressionLevel.BestCompression })) 
//              //    {
//              //        foreach (var passo in this.SelectedSuite.PassosDoRoteiro)
//              //        {
//              //            rarWriter.Write(new FileInfo(passo.SelectedCase.CaminhoArquivoCTF).Name, new FileInfo(passo.SelectedCase.CaminhoArquivoCTF)) ;
//              //        }
//              //    }
//              //}


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

//            //Attachment anexo;
//            //anexo = new Attachment(this.SelectedSuite.Codigo + ".zip");
//            //message.Attachments.Add(anexo);
//            //message.Attachments.Add(anexo);

//            message.Body += "</table>";

            MetroDialogSettings st = new MetroDialogSettings();
            st.AffirmativeButtonText = "Ok";
            st.NegativeButtonText = "Cancelar";
            string emailDest = await this.mainWindow.ShowInputAsync("Email", "Insira o EMAIL para envio do relatório", st);

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

        private string gerarMensagemRelatorioHTML()
        {
            string Body;

            Body = @"<h1>Relatório</h1></br> 
                             
                            <h2>Sistema: " + selectedSistema.Nome + @"</h2></br>" +
                         "<h2>Roteiro: " + this.SelectedSuite.Codigo.ToUpper() + @"</h2></br>";

            Body += @"<h5> Hora da Execução: " + this.SelectedSuite.horaExecucao.ToString() + "</h5></br>";
            Body += @"<h5> Tempo de Execução: " + this.SelectedSuite.tempoExecucao.ToString() + "</h5></br>";
            Body += @"<h5> Quantidade de Testes: " + this.SelectedSuite.PassosDoRoteiro.Count.ToString() + "</h5></br>";

            Body += @"<table style=' font-family: arial, sans-serif; border-collapse: collapse; width: 100%;'> 
                                <tr> 
                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>Código</th> 
                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>Nome</th> 
                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>APR/TOTAL IE</th> 
                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>APR/TOTAL FFOX</th>
                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>APR/TOTAL CHROME</th>
                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>APR/TOTAL EDGE</th>
                                    <th style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>INFO</th>
                                </tr>";


            foreach (PassoDoRoteiro passo in this.SelectedSuite.PassosDoRoteiro)
            {
                Body += "<tr>";
                Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.SelectedCase.Codigo + "</td>";
                Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.SelectedCase.Nome + "</td>";
                Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.ie_stats + "</td>";
                Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.ffox_stats + "</td>";
                Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.chrome_stats + "</td>";
                Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.edge_stats + "</td>";
                Body += "<td style='border: 1px solid #dddddd;text-align: left;padding: 8px;'>" + passo.Obs + "</td>";
                Body += "</tr>";
                
            }

            Body += "</table>";

            return Body;
        }

        private void abrirDetalhesCasoDeTeste(object sender, RoutedEventArgs e)
        {
            this.mainWindow.OpenTestCaseViewWithSelectedCase(getSelectedPasso().SelectedCase);
        }

        private void excluirPasso(object sender, RoutedEventArgs e)
        {
            this.SelectedSuite.PassosDoRoteiro.Remove(getSelectedPasso());
            SelectedSuite.ordenarPassos();
        }

        private void adicionarPassoAbaixo(object sender, RoutedEventArgs e)
        {
            if (SelectedSuite == null)
            {
                MessageBox.Show("selectedSuite == null");
                return;
            }
            if (selectedSistema == null)
            {
                MessageBox.Show("selectedSistema == null");
                return;
            }


            // MessageBox.Show("SISTEMA: " + selectedSistema.Id + " - " + selectedSistema.Nome);
            int posPasso = SelectedSuite.PassosDoRoteiro.IndexOf(getSelectedPasso()) + 1;

            SelectedSuite.PassosDoRoteiro.Insert(posPasso, new PassoDoRoteiro(SelectedSuite)
            {
                deveExecutar = true,
                Ordem = SelectedSuite.PassosDoRoteiro.Count + 1,
            });

            SelectedSuite.ordenarPassos();
        }

        private void abrirCTFGerado(object sender, RoutedEventArgs e)
        {
            var selectedPasso = getSelectedPasso();

            if (selectedPasso == null)
            {

            }
            else
            {
                var selectedCase = selectedPasso.SelectedCase;

                if (File.Exists(selectedCase.CaminhoArquivoCTF))
                {
                    ProcessStartInfo processInfo = new ProcessStartInfo();
                    processInfo.UseShellExecute = true;
                    processInfo.FileName = selectedCase.CaminhoArquivoCTF;
                    Process.Start(processInfo).WaitForExit();
                }
                else
                {
                    this.mainWindow.showMessage("Erro", selectedCase.CaminhoArquivoCTF + " não encontrado");
                }
            }
        }

        private void gerarZipRoteiro(object sender, RoutedEventArgs e)
        {

            //Create Zip File

            var dir = Directory.CreateDirectory("Roteiros\\" + this.SelectedSuite.sistemaPai.Nome + "\\" + this.SelectedSuite.Codigo);


            using (var rar = File.OpenWrite(dir.FullName + "\\" + this.SelectedSuite.Codigo + ".zip"))
            {
                using (var rarWriter = WriterFactory.Open(rar, ArchiveType.Zip, new CompressionInfo() { DeflateCompressionLevel = SharpCompress.Compressor.Deflate.CompressionLevel.BestCompression }))
                {
                    foreach (var passo in this.SelectedSuite.PassosDoRoteiro)
                    {
                        if (File.Exists(passo.SelectedCase.CaminhoArquivoCTF))
                        rarWriter.Write(new FileInfo(passo.SelectedCase.CaminhoArquivoCTF).Name, new FileInfo(passo.SelectedCase.CaminhoArquivoCTF));
                    }
                }
            }

            string filePath = dir.FullName + "\\" + this.SelectedSuite.Codigo + ".zip";
            
            if (!File.Exists(filePath))
            {
                return;
            }

            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + filePath + "\"";

            System.Diagnostics.Process.Start("explorer.exe", argument);

        }

        private void gerarRelatorioHTML(object sender, RoutedEventArgs e)
        {
            var dir = Directory.CreateDirectory("Roteiros\\" + this.SelectedSuite.sistemaPai.Nome + "\\" + this.SelectedSuite.Codigo);

            var writer = File.CreateText(dir.FullName+"\\"+ this.SelectedSuite.Codigo + ".html");
            writer.Write(this.gerarMensagemRelatorioHTML());
            writer.Close();

            string filePath = dir.FullName + "\\" + this.SelectedSuite.Codigo +".html";

            if (!File.Exists(filePath))
            {
                return;
            }

            // combine the arguments together
            // it doesn't matter if there is a space after ','
            string argument = "/select, \"" + filePath + "\"";

            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

        private async void EditarCodigoRoteiro(object sender, RoutedEventArgs e)
        {
            MetroDialogSettings st = new MetroDialogSettings();
            st.AffirmativeButtonText = "Ok";
            st.NegativeButtonText = "Cancelar";
            st.DefaultText = SelectedSuite.Codigo;
            string nomeTemp = await this.mainWindow.ShowInputAsync("Alterar Código do Roteiro de Teste", "Insira Novo Código do Roteiro de Teste", st);
            if (!String.IsNullOrEmpty(nomeTemp))
            {

                SelectedSuite.Codigo = nomeTemp;
                SelectedSuite.Salvar();
                await this.mainWindow.ShowMessageAsync("Sucesso!", "Código Do Roteiro de Teste alterado com sucesso!");

            }
            else
            {
                await this.mainWindow.ShowMessageAsync("Novo Código Não Informado", "O Código do Roteiro não foi alterado");
            }
        }

        private void abrirArqParam(object sender, RoutedEventArgs e)
        {
            var selectedPasso = getSelectedPasso();

            if (selectedPasso == null)
            {

            }
            else
            {


                if (File.Exists(selectedPasso.SelectedArq.FullName))
                {
                    ProcessStartInfo processInfo = new ProcessStartInfo();
                    processInfo.UseShellExecute = true;
                    processInfo.FileName = selectedPasso.SelectedArq.FullName;
                    Process.Start(processInfo).WaitForExit();
                }
                else
                {
                    this.mainWindow.showMessage("Erro", selectedPasso.SelectedArq.FullName + " não encontrado");
                }
            }
        }

    }
}
