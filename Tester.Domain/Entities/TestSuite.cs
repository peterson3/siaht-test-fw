using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using UI_test_player_TD.Controllers;
//using UI_test_player_TD.DB;

namespace UI_test_player_TD.Model
{
    public class TestSuite : INotifyPropertyChanged
    {
        private int _Id { get; set; }
        public int Id 
        {
            get 
            {
                return _Id;
            }

            set 
            {
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }
        private string _Codigo { get; set; }
        public string Codigo 
        {
            get
            {
                return _Codigo;
            }

            set
            {
                _Codigo = value;
                NotifyPropertyChanged("Codigo");
            }
        }
        public ObservableCollection<PassoDoRoteiro> PassosDoRoteiro { get; set; }
        public Sistema sistemaPai { get; set; }
        private string _Descricao { get; set; }

        public TimeSpan tempoExecucao { get; set; }
        public DateTime horaExecucao { get; set; }

        public string Descricao 
        {
            get { return _Descricao; }
            set { _Descricao = value; NotifyPropertyChanged("Descricao"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private string nomeRtf;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //public TestSuite()
        //{
        //    PassosDoRoteiro = new ObservableCollection<PassoDoRoteiro>();
        //}

        public TestSuite(string nomeRtf, Sistema sistemaPai)
        {
            // TODO: Complete member initialization
            this.Codigo = nomeRtf;
            PassosDoRoteiro = new ObservableCollection<PassoDoRoteiro>();
            this.sistemaPai = sistemaPai;

        }
        ////Função para Testes
        //public static List<TestSuite> getTestSuitesExample()
        //{
        //    List<TestSuite> testSuites = new List<TestSuite>();

        //    testSuites.Add(new TestSuite()
        //    {
        //        Codigo = "TS_codigo",
        //        Id = 1,
        //    });

        //    var testSuite = new TestSuite() { Codigo = "TS_codigo2", Id = 2 };

        //    testSuites.Add(testSuite);

        //    return testSuites;
        //}

        public void Salvar()
        {
            TestSuite_DAO.Salvar(this);
            //TODO
        }

        public void DeletarDoBD()
        {
            TestSuite_DAO.Deletar(this);
            //TODO
        }


        public void GetPassosFromDB()
        {
            TestSuite_DAO.GetPassos(this);
        }

        public void ordenarPassos()
        {
            //Ordenando
            int i = 1;
            foreach (PassoDoRoteiro passo in PassosDoRoteiro)
            {
                passo.Ordem = i;
                i++;
            }

            this.PassosDoRoteiro.OrderBy(passo => passo.Ordem);
        }
    }
}
