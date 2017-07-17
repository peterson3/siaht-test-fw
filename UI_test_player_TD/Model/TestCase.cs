using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDown_QA_FrameWork;
using TopDown_QA_FrameWork.Geradores;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Windows;
using System.ComponentModel;
using UI_test_player_TD.DB;

namespace UI_test_player_TD.Model 
{
    public class TestCase : INotifyPropertyChanged
    {
        public static readonly string IMG_ERRO = "../Resources/erro.png";
        public static readonly string IMG_APPR = "../Resources/ok.png";
        public static readonly string IMG_QUEST = "../Resources/question.png";

        public int Id { get; set; }

        private string _Nome { get; set; }

        public string Nome 
        { 
            get
            {
                return _Nome;
            }
            set 
            {
                _Nome = value;
                NotifyPropertyChanged("Nome");
            }
        }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public ObservableCollection<PassoDoTeste> Passos { get; set; }
        public Sistema SistemaPai { get; set; }
        public DateTime UltimaVezExecutado { get; set; }
        public string CaminhoArquivoCTF { get; set; }
        public string Funcao { get; set; }
        public string Modulo { get; set; }
        private int _TotalExecutado { get; set; }
        public int TotalExecutado 
        {
            get 
            {
                return _TotalExecutado;
            }
            set 
            {
                _TotalExecutado = value;
                NotifyPropertyChanged("TotalExecutado");
            }
        }
        private bool? _UltimaSituacao { get; set; }
        public bool? UltimaSituacao 
        { 
            get
            {
                return _UltimaSituacao;
            }
            
            set
            {
                _UltimaSituacao = value;
                if (UltimaSituacao.HasValue)
                {
                    if (UltimaSituacao == true)
                    {
                        UltimaSituacaoString = "Aprovado";
                    }
                    else
                    {
                        UltimaSituacaoString = "Reprovado";
                    }
                }
                else
                {
                    UltimaSituacaoString = "Nunca Executado";
                }
                NotifyPropertyChanged("UltimaSituacao");
                NotifyPropertyChanged("UltimaSituacaoString");

            }
        } //Aprovado, Erro, Nulo=nao executado
        private string _UltimaSituacaoString { get; set; }
        public string UltimaSituacaoString 
        {
            get 
            {
                return _UltimaSituacaoString;
            }
            set 
            {
                _UltimaSituacaoString = value;
                NotifyPropertyChanged("UltimaSituacaoString");
            }
        }

        public string _testSituationImg { get; set; }
        public string testSituationImg 
        {
            get 
            {
                return _testSituationImg;
            }
            set 
            {
                _testSituationImg = value;
                NotifyPropertyChanged("testSituationImg");
            }
        }
        
        public int _TotalApr { get; set; }
        public int TotalApr
        {
            get 
            {
                return _TotalApr;
            }

            set 
            {
                _TotalApr = value;
                NotifyPropertyChanged("TotalApr");
            }
        }

        private int _TotalErr { get; set; }
        public int TotalErr 
        {
            get 
            {
                return _TotalErr;
            }
            set 
            {
                _TotalErr = value;
                NotifyPropertyChanged("TotalErr");
            }
        }
        public string PreCondicao { get; set; }
        public string PosCondicao { get; set; }
        public string SAC { get; set; }

        private TimeSpan _tempoEstimado { get; set; }
        public TimeSpan tempoEstimado 
        {
            get { return _tempoEstimado; } 
            set
            {
                _tempoEstimado = value;
                NotifyPropertyChanged("tempoEstimado");
            } 
        }
        public PassoDoTeste PassoSelecionado { get; set; }

        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TestCase(string Codigo)
        {
            Passos = new ObservableCollection<PassoDoTeste>();
            TotalExecutado = 0;
            TotalErr = 0;
            TotalApr = 0;
            this.Codigo = Codigo;
            UltimaSituacao = null;
            this.CaminhoArquivoCTF = Settings.ctfsPath + "\\" + this.Codigo + ".xls";
            testSituationImg = TestCase.IMG_QUEST;
        }

        public void ordenarPassos()
        {
            //Ordenando
            int i = 1;
            foreach (PassoDoTeste passo in Passos)
            {
                passo.OrdemSeq = i;
                i++;
            }

            this.Passos.OrderBy(passo => passo.OrdemSeq);
        }

        public void exec()
        {
            #region Cabeçalho CTF
            CTF.Iniciar(this.Codigo);
            CTF.InformacoesIniciais(
                this.Modulo,
                this.Funcao,
                this.PreCondicao,
                this.PosCondicao,
                "Browser:" + Settings.BrowserDesc + "\tWeb:" + Settings.ServerUrl + "\tBD:" + Settings.DataBase,
                Settings.ProductVersion,
                this.SAC,
                Settings.Tester,
                DateTime.Today.ToString(@"DD/MM/YYYY"));
            //
            #endregion


            foreach (PassoDoTeste passo in Passos)
            {
                if (passo.deveExecutar)
                {
                    passo.executar();
                }
            }

            CTF.Finalizar();
        }

        public void exec(int sleepTime, int browserCod)
        {
            #region Cabeçalho CTF
            CTF.Iniciar(this.Codigo);
            switch (browserCod)
            {
                case Browser.IE_BROWSER:
                    Settings.BrowserDesc = "Internet Explorer";
                    break;
                case Browser.CHROME_BROWSER:
                    Settings.BrowserDesc = "Google Chrome";
                    break;
                case Browser.FIREFOX_BROWSER:
                    Settings.BrowserDesc = "Mozilla Firefox";
                    break;
                case Browser.SAFARI_BROWSER:
                    Settings.BrowserDesc = "Safari";
                    break;
                case Browser.EDGE_BROWSER:
                    Settings.BrowserDesc = "Microsoft Edge";
                    break;
                case Browser.OPERA_BROWSER:
                    Settings.BrowserDesc = "Opera";
                    break;
            }
            CTF.InformacoesIniciais(
                this.Modulo,
                this.Funcao,
                this.PreCondicao,
                this.PosCondicao,
                "Browser:" + Settings.BrowserDesc + "\tWeb:" + Settings.ServerUrl + "\tBD:" + Settings.DataBase,
                Settings.ProductVersion,
                this.SAC,
                Settings.Tester,
                DateTime.Today.ToString(@"DD/MM/YYYY"));
            //
            #endregion
            Stopwatch cronometro = new Stopwatch();
            cronometro.Start();
            foreach (PassoDoTeste passo in Passos)
            {
                passo.SituationImg = PassoDoTeste.IMG_QUEST;
                passo.teveSucesso = null;
                passo.Obs = "";
                passo.Retorno = "";
            }
            foreach (PassoDoTeste passo in Passos)
            {
                if (passo.deveExecutar)
                {
                    try
                    {
                        passo.executar();
                        if (passo.acaoSelecionada.Nome.ToUpper().Contains("IR PARA"))
                        {
                            CTF.irParaFuncionalidade(passo.telaSelecionada.Nome);
                        }
                        else
                        {
                            if (passo.ParametroEhFuncao())
                            {
                                CTF.inserirComando("", passo.acaoSelecionada.Nome, passo.parametroComputado, "", "", passo.Retorno);
                            }
                            else
                            {
                                CTF.inserirComando("", passo.acaoSelecionada.Nome, passo.Parametro, "", "", passo.Retorno);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        passo.Obs = ex.Source +": " +ex.Message + Environment.NewLine + ex.StackTrace;
                        passo.teveSucesso = false;
                        passo.SituationImg = PassoDoTeste.IMG_ERRO;
                        Logger.escrever(ex.Message + ex.Source);
                        if (passo.acaoSelecionada.Nome.ToUpper().Contains("IR PARA"))
                        {
                            CTF.irParaFuncionalidade(passo.telaSelecionada.Nome);
                        }
                        else
                        {
                            if (passo.ParametroEhFuncao())
                            {
                                CTF.inserirComando("", passo.acaoSelecionada.Nome, passo.parametroComputado, "", "", "ERRO / " + passo.Retorno);
                            }
                            else
                            {
                                CTF.inserirComando("", passo.acaoSelecionada.Nome, passo.Parametro, "", "", "ERRO / " + passo.Retorno);
                            }
                        } 
                        CTF.inserirImagemErro(PrintUtils.takeSS());

                        if (Settings.IgnorarFalha == false)
                        {
                            cronometro.Stop();
                            this.Salvar();
                            throw ex;
                        }
                        else
                        {
                            //do nth
                        }
                     
                    }
                    finally
                    {
                        cronometro.Stop();
                        Browser.Sleep(sleepTime);
                        cronometro.Start();
                        this.Salvar();
                    }
                }
            }
            cronometro.Stop();

            if (TotalExecutado==0)
            {   
                tempoEstimado = cronometro.Elapsed;
            }
            else
            {
                tempoEstimado = TimeSpan.FromTicks((tempoEstimado.Ticks + cronometro.Elapsed.Ticks)/ 2);
            }

            CTF.Finalizar();
        }

        public void postExec()
        {
            TotalExecutado++;
            this.UltimaVezExecutado = DateTime.Now;
        } 

        public void aprovar()
        {
            TotalApr++;
            this.UltimaSituacao = true;
            this.testSituationImg = IMG_APPR;
            Tools.ToolTipSystemTray.showAprBalloon("Caso de Teste", "Finalizado com Sucesso");
            CTF.registrarSucesso();
            this.Salvar();
        }

        public void reprovar()
        {
            TotalErr++;
            this.UltimaSituacao = false;
            this.testSituationImg = IMG_ERRO;
            Tools.ToolTipSystemTray.showErrorBalloon("Caso de Teste", "Abortado - Apresentou Falha");
            CTF.registrarErro();
            this.Salvar();
        }

        public void Salvar()
        {
            TestCase_DAO.Salvar(this);
        }

        public int[] execFromFile(FileInfo file, int stepDelay, int browser_cod)
        {
            int qtdPassos = this.Passos.Count;
            int qtdParametros = 0;
            int qtdTestes = 0;
            StreamReader reader = new StreamReader(file.FullName, System.Text.Encoding.Default);

            int qtdAprov = this.TotalApr;
            int qtdErr = this.TotalErr;

            while (!reader.EndOfStream)
            {
                string linha = reader.ReadLine();
                string[] parametros;



                parametros = linha.Split(';');
                qtdParametros = parametros.Length;

                //a ultima string pode ser jogada fora
                //string test = "";
                //for (int j=0; j<qtdParametros; j++)
                //{
                //    if (String.IsNullOrWhiteSpace(parametros[j]))
                //    {
                //        test += "-vazio-" + Environment.NewLine;
                //    }
                //    else
                //    {
                //        test += parametros[j] + Environment.NewLine;

                //    }
                //}
                //MessageBox.Show(test);

                if (qtdParametros != qtdPassos)
                {
                    MessageBox.Show("Linha no Arquivo com Quantidade de Parâmetros diferente do Necessário. (Necessário: " + qtdPassos.ToString() + ", quantidade na linha: " + qtdParametros + ")");
                    reader.Close();
                    return null;
                }


                this.Passos = new ObservableCollection<PassoDoTeste>(this.Passos.OrderBy(passo => passo.OrdemSeq).ToList());

                int i=0;

                foreach (PassoDoTeste passo in Passos)
                {
                    passo.Parametro = parametros[i];
                    i++;
                }

                //for (int i = 0; i < qtdPassos; i++)
                //{
                //    this.Passos[i].Parametro = parametros[i];
                //}


                this.run(stepDelay, browser_cod);
                qtdTestes++;
                //Copiar o CTF gerado para uma pasta qualquer renomeando para número da linha
                File.Copy(this.CaminhoArquivoCTF, this.CaminhoArquivoCTF.Replace(".xls", "_" + qtdTestes) + ".xls", true);
            }

            qtdAprov = this.TotalApr - qtdAprov;
            qtdErr = this.TotalErr - qtdErr;

            int[] retorno = new int[2];

            reader.Close();
            retorno [0] = qtdAprov;
            //MessageBox.Show("Quantidade de Testes Executados: " + qtdTestes + " (" + qtdAprov + " aprovado(s), " + qtdErr + " reprovado(s) ).");
            retorno[1] = qtdAprov + qtdErr;

            return retorno;
            //Selecionar arquivo .csv
            //Para cada linha do arquivo
            //fazer a verificação se a quantidade de parâmetros bate com a quantidade de passos
            //Executar o teste com os parâmetros do arquivo
        }

        public void run(int stepDelay, int browser_cod)
        {
            try
            {
                //TODO: SUBSTITUIR POR URL DO SISTEMA SELECIONADO
                string baseDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string IE_DRIVER_PATH = Path.Combine(baseDir, "WebDriver\\IEDriverServer_Win32_2.53.1");
                string CHROME_DRIVER_PATH = Path.Combine(baseDir, "WebDriver\\chromedriver_win32");
                string FIREFOX_DRIVER_PATH = Path.Combine(baseDir, "WebDriver\\geckodriver");
                string SAFARI_DRIVER_PATH = Path.Combine(baseDir, "WebDriver\\chromedriver_win32");
                string EDGE_DRIVER_PATH = Path.Combine(baseDir, "WebDriver\\edgeWebDriver");
                string OPERA_DRIVER_PATH = Path.Combine(baseDir, "WebDriver\\operadriver_win32");

                //teste = @"D:\Peterson\Projetos\QADynamicFramework\TopDown_QA_FrameWork\WebDriver\IEDriverServer_Win32_2.53.1";
                //if (File.Exists(teste))
                //{
                //    MessageBox.Show("Existe");
                //}
                //else
                //{
                //    MessageBox.Show("Nao existe");
                //}
                //MessageBox.Show(teste);

                //Passar o Browser Desejado
                TopDown_QA_FrameWork.Browser.Initialize(this.SistemaPai.homeURL, browser_cod, IE_DRIVER_PATH, CHROME_DRIVER_PATH, FIREFOX_DRIVER_PATH, SAFARI_DRIVER_PATH, EDGE_DRIVER_PATH, OPERA_DRIVER_PATH);
                CTF.Iniciar(this.Codigo);
                this.exec(stepDelay, browser_cod);
                this.aprovar();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.Source + ex.StackTrace);
                this.reprovar();
            }
            finally
            {
                this.postExec();
                //updateWindow();
                if (Settings.CloseBrowserAfterTestCase)
                    TopDown_QA_FrameWork.Browser.Close();
            }

        }

        public PassoDoTeste getPassoDoTestAtIndex(int index)
        {
            int count = 1;
            for (int i = 0; i < Passos.Count; i++)
            {
                if (count == index)
                {
                    return Passos[i];
                }
                count++;
            }

            return null;
        }

        public void GetPassosFromDB()
        {
            TestCase_DAO.GetPassos(this);
            ordenarPassos();
        }

        public void DeletarDoBD()
        {
            TestCase_DAO.Deletar(this);
        }
    }
}
