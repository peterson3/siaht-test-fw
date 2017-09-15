using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDown_QA_FrameWork;
using UI_test_player_TD.Controllers;
using UI_test_player_TD.DB;

namespace UI_test_player_TD.Model
{
    public class PassoDoRoteiro : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public ObservableCollection<TestCase> TestCasePossiveis { get; set; }

        private TestCase _SelectedCase { get; set; }
        public TestCase SelectedCase 
        {
            get 
            {
                return _SelectedCase;
            }

            set 
            {
                _SelectedCase = value;
                NotifyPropertyChanged("SelectedCase");
            }
        }

        public bool deveExecutar { get; set; }

        private FileInfo _SelectedArq { get; set; }
        public FileInfo SelectedArq 
        { 
            get
            {
                return _SelectedArq;
            }

            set 
            {
                _SelectedArq = value;
                NotifyPropertyChanged("SelectedArq");
            }
        }

        private string _Obs { get; set; }
        public string Obs
        {
            get
            {
                return _Obs;
            }
            set
            {
                _Obs = value;
                NotifyPropertyChanged("Obs");
            }
        }

        private int _Ordem { get; set; }
        public int Ordem
        {
            get
            {
                return _Ordem;
            }

            set
            {
                _Ordem = value;
                NotifyPropertyChanged("Ordem");
            }
        }

        private int _ie_total { get; set; }
        public int ie_total { get { return _ie_total; } set { _ie_total = value; NotifyPropertyChanged("ie_total"); } }

        private int _ie_apr { get; set; }
        public int ie_apr { get { return _ie_apr; } set { _ie_apr = value; NotifyPropertyChanged("ie_apr"); } }

        private string _ie_stats { get; set; }
        public string ie_stats { get { return _ie_stats; } set { _ie_stats = value; NotifyPropertyChanged("ie_stats"); } }

        private int _ffox_total { get; set; }
        public int ffox_total { get { return _ffox_total; } set { _ffox_total = value; NotifyPropertyChanged("ffox_total"); } }

        private int _ffox_apr { get; set; }
        public int ffox_apr { get { return _ffox_apr; } set { _ffox_apr = value; NotifyPropertyChanged("ffox_apr"); } }

        private string _ffox_stats { get; set; }
        public string ffox_stats { get { return _ffox_stats; } set { _ffox_stats = value; NotifyPropertyChanged("ffox_stats"); } }

        private int _chrome_total { get; set; }
        public int chrome_total { get { return _chrome_total; } set { _chrome_total = value; NotifyPropertyChanged("chrome_total"); } }

        private int _chrome_apr { get; set; }
        public int chrome_apr { get { return _chrome_apr; } set { _chrome_apr = value; NotifyPropertyChanged("chrome_apr"); } }

        private string _chrome_stats { get; set; }
        public string chrome_stats { get { return _chrome_stats; } set { _chrome_stats = value; NotifyPropertyChanged("chrome_stats"); } }

        private int _edge_total { get; set; }
        public int edge_total { get { return _edge_total; } set { _edge_total = value; NotifyPropertyChanged("edge_total"); } }

        private int _edge_apr { get; set; }
        public int edge_apr { get { return _edge_apr; } set { _edge_apr = value; NotifyPropertyChanged("edge_apr"); } }
        
        private string _edge_stats { get; set; }
        public string edge_stats { get { return _edge_stats; } set { _edge_stats = value; NotifyPropertyChanged("edge_stats"); } }


        public PassoDoRoteiro(TestSuite testSuitePai)
        {
            //ObservableCollection<Sistema> sistemas = Sistema_DAO.getAllSistemas();
            this.TestSuitePai = testSuitePai;
            //this.TestCasePossiveis = new ObservableCollection<TestCase>(TestCase_DAO.GetAllFromSistema(this.TestSuitePai.sistemaPai));
            this.TestCasePossiveis = new ObservableCollection<TestCase>(testSuitePai.sistemaPai.testCases);
        }

        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;   

        public void run(int stepDelay, int browser_cod)
        {
            int[] exc;
            exc = this.SelectedCase.execFromFile(this._SelectedArq, stepDelay, browser_cod);
            this.Obs = "";

            foreach (var passo in this.SelectedCase.Passos)
            {
                if (passo.teveSucesso.HasValue)
                {
                    if (passo.teveSucesso.Value == false)
                    {
                        switch (browser_cod)
                        {
                            case Browser.IE_BROWSER:
                                this.Obs += "[IE]Falha no Passo " + passo.OrdemSeq.ToString() + " - " + passo.acaoSelecionada.Nome + ": " + passo.Retorno + Environment.NewLine;
                                break;
                            case Browser.FIREFOX_BROWSER:
                                this.Obs += "[FFOX]Falha no Passo " + passo.OrdemSeq.ToString() + " - " + passo.acaoSelecionada.Nome + ": " + passo.Retorno + Environment.NewLine;
                                break;
                            case Browser.CHROME_BROWSER:
                                this.Obs += "[CHROME]Falha no Passo " + passo.OrdemSeq.ToString() + " - " + passo.acaoSelecionada.Nome + ": " + passo.Retorno + Environment.NewLine;
                                break;
                            case Browser.EDGE_BROWSER:
                                this.Obs += "[EDGE]Falha no Passo " + passo.OrdemSeq.ToString() + " - " + passo.acaoSelecionada.Nome + ": " + passo.Retorno + Environment.NewLine;
                                break;

                        }
                    }
                }

            }

            if (browser_cod == Browser.IE_BROWSER)
            {
                this.ie_apr = exc[0];
                this.ie_total = exc[1];
                this.ie_stats = ie_apr.ToString() + "/" + ie_total.ToString();
            }
            if (browser_cod == Browser.FIREFOX_BROWSER)
            {

                this.ffox_apr = exc[0];
                this.ffox_total = exc[1];
                this.ffox_stats = ffox_apr.ToString() + "/" + ffox_total.ToString();

            }
            if (browser_cod == Browser.CHROME_BROWSER)
            {
                this.chrome_apr = exc[0];
                this.chrome_total = exc[1];
                this.chrome_stats = chrome_apr.ToString() + "/" + chrome_total.ToString();

            }
            if (browser_cod == Browser.EDGE_BROWSER)
            {

                this.edge_apr = exc[0];
                this.edge_total = exc[1];
                this.edge_stats = edge_apr.ToString() + "/" + edge_total.ToString();
            }
        }

        public TestSuite TestSuitePai { get; set; }

        public void zerarStats()
        {
            this.ie_apr = 0;
            this.ie_total = 0;
            this.ie_stats = "NA";

            this.ffox_apr = 0;
            this.ffox_total = 0;
            this.ffox_stats = "NA";

            this.chrome_apr = 0;
            this.chrome_total = 0;
            this.chrome_stats = "NA";

            this.edge_apr = 0;
            this.edge_total = 0;
            this.edge_stats = "NA";
        }
    }

}
