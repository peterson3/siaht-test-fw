using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopDown_QA_FrameWork.Geradores;
using UI_test_player_TD.Controllers;
using UI_test_player_TD.DB;

namespace UI_test_player_TD.Model
{
    public class Sistema
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string homeURL { get; set; }
        public ObservableCollection<TestSuite> testSuites { get; set; }

        private ObservableCollection<Tela> _telas { get; set; }
        public ObservableCollection<Tela> telas {
            get 
            {
                if (_telas == null)
                {
                    _telas = new ObservableCollection<Tela>(Tela_DAO.getAllTelas(this));
                }
                return _telas;
            }
            set 
            {
                _telas = value;
            }
        }

        private ObservableCollection<TestCase> _testCases { get; set; }
        public ObservableCollection<TestCase> testCases { 
            get 
            {
                if (_testCases == null)
                {
                    //recupera do banco
                    _testCases = new ObservableCollection<TestCase>(TestCase_DAO.GetAllFromSistema(this));
                }
                return _testCases;
            }
            set 
            {
                _testCases = value;
            }
        }

        public Sistema(string Nome)
        {
            this.Nome = Nome;
            //Salvar No Banco E Receber Novo Id
        }

        public void Salvar()
        {
            Sistema_DAO.Salvar(this);
        }

        //public ObservableCollection<Tela> getTelas()
        //{
        //   return Tela_DAO.getAllTelas(this);
        //}

        public ObservableCollection<TestCase> getTestCasesFromDb()
        {
            var testCases = new ObservableCollection<TestCase>(TestCase_DAO.GetAllFromSistema(this));
            return testCases;
            //ObservableCollection<TestCase> testCases = new ObservableCollection<TestCase>();
            //string conexao = "Data Source=db/testplayerdb.db";
            //string nomebanco = "db/testplayerdb.db";

            //if (!File.Exists(nomebanco))
            //{
            //    Logger.escrever("Não Encontrado o Arquivo de Banco. Criando..");

            //}
            //else
            //{
            //    //Db existe

            //    //Abre conexão
            //    SQLiteConnection conn = new SQLiteConnection(conexao);
            //    if (conn.State == ConnectionState.Closed)
            //        conn.Open();

            //    //Carrega Registros

            //    SQLiteCommand sql_cmd = new SQLiteCommand("SELECT * FROM TestCase WHERE IdSistema=@IdSistema", conn);
            //    sql_cmd.Parameters.AddWithValue("@IdSistema", this.Id);

            //    SQLiteDataReader sql_dataReader = sql_cmd.ExecuteReader();


            //    while (sql_dataReader.Read())
            //    {
            //        TestCase testCase = new TestCase(sql_dataReader["Codigo"].ToString());
            //        testCase.Id = Convert.ToInt32(sql_dataReader["Id"]);
            //        testCase.Nome = sql_dataReader["Nome"].ToString();
            //        testCase.Descricao = sql_dataReader["Descricao"].ToString();
            //        if (!sql_dataReader.IsDBNull(4))
            //        {
            //            testCase.UltimaVezExecutado = Convert.ToDateTime(sql_dataReader["UltimaExec"]);
            //        }
            //        testCase.Funcao = sql_dataReader["FuncNome"].ToString();
            //        testCase.Modulo = sql_dataReader["ModuloNome"].ToString();
            //        if (!sql_dataReader.IsDBNull(8))
            //            testCase.TotalExecutado = Convert.ToInt32(sql_dataReader["TotalExec"]);
            //        if (!sql_dataReader.IsDBNull(10))
            //        {
            //            testCase.TotalApr = Convert.ToInt32(sql_dataReader["TotalAppr"]);
            //            testCase.TotalErr = testCase.TotalExecutado - testCase.TotalApr;
            //        }

            //        if (!sql_dataReader.IsDBNull(9))
            //        {
            //            testCase.UltimaSituacao = Convert.ToBoolean(sql_dataReader["UltimaSituacao"]);
            //            if (testCase.UltimaSituacao == true)
            //            {
            //                testCase.testSituationImg = TestCase.IMG_APPR;
            //            }
            //            else
            //            {
            //                testCase.testSituationImg = TestCase.IMG_ERRO;
            //            }
            //        }

            //        testCase.PreCondicao = sql_dataReader["PreCond"].ToString();
            //        testCase.PosCondicao = sql_dataReader["PosCond"].ToString();
            //        testCase.SistemaPai = this;

            //        if (!sql_dataReader.IsDBNull(13))
            //            testCase.tempoEstimado = TimeSpan.Parse(DateTime.Parse(sql_dataReader["TempoEstimado"].ToString()).ToLongTimeString());


            //        testCases.Add(testCase);
            //    }

            //}

            //return testCases;
        }

        public void Deletar()
        {
            Sistema_DAO.Deletar(this);
        }

        public void Update()
        {
            Sistema_DAO.Update(this);
        }

        public  ObservableCollection<TestSuite> getTestSuitesFromDB(Sistema sistema)
        {
            //TODO
            var testSuites = TestSuite_DAO.getTestSuitesFromSistema(sistema);


            return testSuites;
        }
    }
}
