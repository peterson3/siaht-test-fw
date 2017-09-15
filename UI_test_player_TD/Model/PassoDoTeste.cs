    using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TopDown_QA_FrameWork;
using TopDown_QA_FrameWork.Geradores;
using UI_test_player_TD.Controllers;
using UI_test_player_TD.DB;

namespace UI_test_player_TD.Model
{
   public class PassoDoTeste : INotifyPropertyChanged
    {
        public static readonly string IMG_ERRO = "../Resources/erro.png";
        public static readonly string IMG_APPR = "../Resources/ok.png";
        public static readonly string IMG_QUEST = "../Resources/question.png";


        public Tela TelaParent { get; set; }
        public TestCase TestCaseParent { get; set; }

        private string _Parametro { get; set; }
        public string Parametro 
        {
            get
            {
                return _Parametro;
            }
            set
            {
                _Parametro = value;
                NotifyPropertyChanged("Parametro");
            }
        }
        private string _Retorno { get; set; }
        public string Retorno
        {
            get { return _Retorno; }
            set { _Retorno = value; NotifyPropertyChanged("Retorno"); }
        }

        private string _Obs { get; set; }
        public string Obs
        {
            get { return _Obs; }
            set { _Obs = value; NotifyPropertyChanged("Obs"); }
        }
        public bool deveFotografar { get; set; }
        public bool deveExecutar { get; set; }
        private bool? _teveSucesso { get; set; }
        private string _SituationImg { get; set; }
        public string parametroComputado { get; set; } 
        public string SituationImg 
        {
            get 
            {
                return _SituationImg;
            }
            set
            {
                _SituationImg = value;
                NotifyPropertyChanged("SituationImg");
            }
        }
        public bool? teveSucesso 
        {
            get 
            {
                return _teveSucesso;
            }
            set
            {
                _teveSucesso = value;
                NotifyPropertyChanged("teveSucesso");
            }
        }

        AcaoDynResult resultado { get; set; }

        private int _OrdemSeq { get; set; }
        public int OrdemSeq 
        {
            get { return _OrdemSeq; }
            set { _OrdemSeq = value; NotifyPropertyChanged("OrdemSeq"); }
        }
        public ObservableCollection<Tela> telasPossiveis { get; set; }

        public ObservableCollection<AcaoDyn> acoesPossiveis
        {
            get
            {
                //ToDo to change
                return AcaoDyn_DAO.getAllActionsFromTela(telaSelecionada);
               //return  (new ObservableCollection<AcaoDyn>(telaSelecionada.acoesPossiveis));
            }
        }
           
        private AcaoDyn _acaoSelecionada;
        public AcaoDyn acaoSelecionada
        { 
            get
            {
                return _acaoSelecionada;
            }
            set
            {
                _acaoSelecionada = value;
                NotifyPropertyChanged("acaoSelecionada");
            }
           }    

        private Tela _telaSelecionada;
        public Tela telaSelecionada
        {
            get
            {
                return _telaSelecionada;
            }

            set
            {
                _telaSelecionada = value;
                NotifyPropertyChanged("telaSelecionada");
            }
        }

        public int telaDropdownIndex { get; set; }
        public int acaoDropdownIndex { get; set; }

        public PassoDoTeste(TestCase parent)
        {
            //if (parent.SistemaPai == null)
            //{
            //    MessageBox.Show("Passo do Teste: parent.SistemaPai == null");
            //}
            //else
            //{
            //    MessageBox.Show("sistema " + parent.SistemaPai.Nome);
            //}
            telasPossiveis = Tela_DAO.getAllTelas(parent.SistemaPai);

            //telasPossiveis = new ObservableCollection<Tela>(parent.SistemaPai.telas);
            resultado = new AcaoDynResult();
            this.TestCaseParent = parent;
            List<Tela> telas = telasPossiveis.ToList();
            telas.OrderBy(tela => tela.Nome);
            telasPossiveis = new ObservableCollection<Tela>(telas);
        }


        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool ParametroEhFuncao()
        {
            if (Parametro == null)
            {
                return false;
            }
            if (Parametro.Trim().StartsWith("=", StringComparison.InvariantCulture))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void executar()
        {
            //Achar a Acao que está no passo

            Logger.escrever("Acao: " + acaoSelecionada.Nome + " " + acaoSelecionada.TelaPai.Nome + " " + acaoSelecionada.Id.ToString());

            if (!deveExecutar)
            {
                throw new Exception("A função não deve ser executada");
            }

            bool retorno;

            //Tratando Programação dentro da Grid
             string ParametroKeeper = Parametro;

             if (Parametro == null)
             {
                 Parametro = "";
             }
             if (ParametroEhFuncao())
             {
                 //é uma programação
                 string funcao = Parametro.Substring(1, Parametro.IndexOf("(") - 1);
                 string parametroFunc = Parametro.Substring(Parametro.IndexOf("(") + 1, Parametro.Length - funcao.Length - 3);

                 funcao = funcao.ToUpper();

                 switch (funcao)
                 {
                     case "PARAMETRO":
                         this.Parametro = this.TestCaseParent.getPassoDoTestAtIndex(Convert.ToInt32(parametroFunc)).Parametro;
                         break;
                     case "RETORNO":
                         //Recuperar Passo do Teste Específico do Caso de Teste do Qual esse Passo Pertence
                         this.Parametro = this.TestCaseParent.getPassoDoTestAtIndex(Convert.ToInt32(parametroFunc)).Retorno;
                         break;

                     case "GERARCPF":
                         this.Parametro = Utils.GerarCpf();
                         break;

                     case "DATAHOJE":
                         this.Parametro = Utils.dataHoje();
                         break;

                     case "GERARCNS":
                         this.Parametro = Utils.geraCNS();
                         break;
                 }

             }

             resultado = acaoSelecionada.Executar(Parametro);

             this.Retorno = resultado.retornoInformacao;

             //MessageBox.Show(resultado.passou.ToString() + resultado.retornoInformacao);
             //if (acaoSelecionada.acaoRetorno != null)
             //    this.Retorno = acaoSelecionada.acaoRetorno(Parametro);

             parametroComputado = Parametro;
             Parametro = ParametroKeeper;
          

            if (deveFotografar)
            {
                CTF.inserirImagem(PrintUtils.takeSS());
            }
            if (resultado.passou == true)
            {
                //ok
                teveSucesso = true;
                SituationImg = PassoDoTeste.IMG_APPR;
            }
            else
            {
                teveSucesso = false;
                SituationImg = PassoDoTeste.IMG_ERRO;
                throw new Exception("Ação retornou FALSE (resultado esperado falhou).");
            }
        }
       //End of Func
    }
}   
