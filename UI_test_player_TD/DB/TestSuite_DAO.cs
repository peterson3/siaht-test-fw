using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI_test_player_TD.Model;

namespace UI_test_player_TD.DB
{
    public class TestSuite_DAO
    {

        public static void Salvar(TestSuite testSuite)
        {
            try
            {


                DBConnection.Connect();

                //Carrega Registros
                OracleCommand sql_cmd = new OracleCommand(@"
                SELECT * FROM TESTSUITE WHERE COD_TESTSUITE= :COD_TESTSUITE",
                DBConnection.con);
                sql_cmd.Parameters.Add(":COD_TESTSUITE", testSuite.Id);

                OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();

                //while (sql_dataReader.Read())
                //{

                //}

                //MessageBox.Show("Exists? = " + sql_dataReader.HasRows.ToString());

                //SE O OBJETO PROCURADO EXISTE
                if (sql_dataReader.HasRows)
                {
                    //MessageBox.Show("achou");
                    try
                    {
                        sql_cmd = new OracleCommand(@"UPDATE TESTSUITE 
                        SET 
                            COD_SISTEMA= :COD_SISTEMA,
                            TXT_CODIGO= :TXT_CODIGO,
                            TXT_DESCRICAO = :TXT_DESCRICAO

                        WHERE COD_TESTSUITE= :COD_TESTSUITE", DBConnection.con);

                        sql_cmd.Parameters.Add(":COD_SISTEMA", testSuite.sistemaPai.Id);
                        sql_cmd.Parameters.Add(":TXT_CODIGO", testSuite.Codigo);
                        sql_cmd.Parameters.Add(":TXT_DESCRICAO", testSuite.Descricao);
                        sql_cmd.Parameters.Add(":COD_TESTSUITE", testSuite.Id);


                        sql_cmd.ExecuteNonQuery();


                        //Deletando Registro dos Passos
                        sql_cmd = new OracleCommand(@"DELETE FROM PASSO_ROTEIRO 
                        WHERE  COD_TESTSUITE=:COD_TESTSUITE", DBConnection.con);
                        sql_cmd.Parameters.Add(":COD_TESTSUITE", testSuite.Id);
                        sql_cmd.ExecuteNonQuery();

                        //Colocando as Alterações e Novos Passos
                        foreach (PassoDoRoteiro passo in testSuite.PassosDoRoteiro)
                        {
                            sql_cmd = new OracleCommand(@"
                            INSERT INTO PASSO_ROTEIRO (COD_PASSO_ROTEIRO, COD_TESTSUITE, TXT_OBS, TXT_ARQ_CAMINHO, COD_SELECTED_TESTCASE, NUM_ORDEM) 
                            VALUES 
                            (seq_cod_passo_roteiro.nextval, :COD_TESTSUITE, :TXT_OBS, :TXT_ARQ_CAMINHO, :COD_SELECTED_TESTCASE, :NUM_ORDEM)", DBConnection.con);

                            sql_cmd.Parameters.Add(":COD_TESTSUITE", testSuite.Id);
                            sql_cmd.Parameters.Add(":TXT_OBS", passo.Obs);
                            sql_cmd.Parameters.Add(":TXT_ARQ_CAMINHO", passo.SelectedArq.FullName);
                            sql_cmd.Parameters.Add(":COD_SELECTED_TESTCASE", passo.SelectedCase.Id);
                            sql_cmd.Parameters.Add(":NUM_ORDEM", passo.Ordem);


                            sql_cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + ex.Source + ex.StackTrace);
                    }




                }
                    //HasRows = False - Não achou - Criar Novo
                else
                {

                    sql_cmd = new OracleCommand(@"
                        INSERT INTO TESTSUITE  
                        (COD_TESTSUITE, COD_SISTEMA, TXT_CODIGO) 
                        
                            VALUES (SEQ_COD_TESTSUITE.nextval,
                                :COD_SISTEMA,
                                :TXT_CODIGO)
                        ", DBConnection.con);

                    sql_cmd.Parameters.Add(":COD_SISTEMA", testSuite.sistemaPai.Id);
                    sql_cmd.Parameters.Add(":TXT_CODIGO", testSuite.Codigo);
                    sql_cmd.ExecuteNonQuery();

                    //                    sql_cmd = new OracleCommand(@"SELECT last_insert_rowid();
                    //                        ", DBConnection.con);

                    int lastId = Convert.ToInt32(sql_cmd.Parameters["COD_TESTSUITE"]);
                    testSuite.Id = lastId;


                    //MessageBox.Show("INSERT: " + testSuite.Id.ToString());


                    //Deletando Registro dos Passos
                    sql_cmd = new OracleCommand(@"DELETE  FROM PASSO_ROTEIRO 
                        WHERE COD_TESTSUITE=:COD_TESTSUITE", DBConnection.con);

                    sql_cmd.Parameters.Add(":COD_TESTSUITE", testSuite.Id);
                    sql_cmd.ExecuteNonQuery();

                    //Colocando as Alterações e Novos Passos
                    foreach (PassoDoRoteiro passo in testSuite.PassosDoRoteiro)
                    {
                        sql_cmd = new OracleCommand(@"
                            INSERT INTO PASSO_ROTEIRO (COD_PASSO_ROTEIRO, COD_TESTSUITE, TXT_OBS, TXT_ARQ_CAMINHO, COD_SELECTED_TESTCASE, NUM_ORDEM) 
                            VALUES 
                            (seq_cod_passo_roteiro.nextval, :COD_TESTSUITE, :TXT_OBS, :TXT_ARQ_CAMINHO, :COD_SELECTED_TESTCASE, :NUM_ORDEM)", DBConnection.con);

                        sql_cmd.Parameters.Add(":COD_TESTSUITE", testSuite.Id);
                        sql_cmd.Parameters.Add(":TXT_OBS", passo.Obs);
                        sql_cmd.Parameters.Add(":TXT_ARQ_CAMINHO", passo.SelectedArq.FullName);
                        sql_cmd.Parameters.Add(":COD_SELECTED_TESTCASE", passo.SelectedCase.Id);
                        sql_cmd.Parameters.Add(":NUM_ORDEM", passo.Ordem);


                        sql_cmd.ExecuteNonQuery();
                    }

                }

                //Fecha Conexão
                DBConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.Source + Environment.NewLine + ex.StackTrace);
            }
            //se exister salva as alterações
            //se não exister - insere novo       
        }

        public static ObservableCollection<TestSuite> getTestSuitesFromSistema(Sistema sistema)
        {
            var testSuites = new ObservableCollection<TestSuite>();
            DBConnection.Connect();


            try
            {


                //Carrega Registros
                OracleCommand sql_cmd = new OracleCommand("SELECT * FROM TESTSUITE where COD_SISTEMA = :COD_SISTEMA", DBConnection.con);
                sql_cmd.Parameters.Add(":COD_SISTEMA", sistema.Id);
                OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();


                while (sql_dataReader.Read())
                {

                    var testSuite = new TestSuite(sql_dataReader.GetString(2), sistema);
                    testSuite.Id = Convert.ToInt32(sql_dataReader.GetDecimal(0));
                    if (!sql_dataReader.IsDBNull(3))
                        testSuite.Descricao = sql_dataReader.GetString(3);
                    testSuites.Add(testSuite);
                }



                foreach (TestSuite testSuite in testSuites)
                {
                    ////MessageBox.Show(testCases.Count.ToString());
                    //if (DBConnection.con.State == ConnectionState.Closed)
                    //{
                    //    DBConnection.Connect();
                    //}
                    //MessageBox.Show(testCase.Id.ToString());
                    OracleCommand sql_cmd2 = new OracleCommand("SELECT * FROM PASSO_ROTEIRO WHERE COD_TESTSUITE=:COD_TESTSUITE ORDER BY NUM_ORDEM", DBConnection.con);
                    sql_cmd2.Parameters.Add("@COD_TESTSUITE", testSuite.Id);

                    OracleDataReader sql_dataReader2 = sql_cmd2.ExecuteReader();


                    while (sql_dataReader2.Read())
                    {
                        PassoDoRoteiro passoDoRoteiro = new PassoDoRoteiro(testSuite);
                        //passoDoRoteiro.TestCasePossiveis = new ObservableCollection<TestCase>(TestCase_DAO.GetAllFromSistema(passoDoRoteiro.TestSuitePai.sistemaPai));


                        if (passoDoRoteiro.TestCasePossiveis == null)
                        {
                            
                        }


                        testSuite.PassosDoRoteiro.Add(passoDoRoteiro);

                    }

                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message + exc.Source + exc.StackTrace);
            }
            finally
            {



            }



            DBConnection.Close();
            return testSuites;
        }

        public static void Deletar(TestSuite testSuite)
        {
            //Db existe
            DBConnection.Connect();
            //Carrega Registros
            OracleCommand sql_cmd;

            sql_cmd = new OracleCommand(@"DELETE FROM TESTSUITE WHERE COD_TESTSUITE =:COD_TESTSUITE
                        ", DBConnection.con);

            sql_cmd.Parameters.Add(":COD_TESTSUITE", testSuite.Id);
            sql_cmd.ExecuteNonQuery();

            //Fecha Conexão
            DBConnection.Close();
        }

        public static void GetPassos(TestSuite testSuite)
        {
            //Abre conexão
            DBConnection.Connect();

            //Carrega Registros

            OracleCommand sql_cmd = new OracleCommand(@"
                SELECT * FROM PASSO_ROTEIRO WHERE COD_TESTSUITE=:COD_TESTSUITE ORDER BY NUM_ORDEM", DBConnection.con);

            sql_cmd.Parameters.Add(":COD_TESTSUITE", testSuite.Id);

            OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();

            //for (int i = 0; i < this.Passos.Count; i++)
            //{
            //    this.Passos.RemoveAt(i);
            //}
            //Passos = new ObservableCollection<PassoDoTeste>();

            testSuite.PassosDoRoteiro.Clear();

            while (sql_dataReader.Read())
            {
                PassoDoRoteiro passoDoRoteiro = new PassoDoRoteiro(testSuite);


                if (!sql_dataReader.IsDBNull(11))
                {
                    FileInfo file = new FileInfo(sql_dataReader.GetString(11));
                    passoDoRoteiro.SelectedArq = file;
                }

                if (!sql_dataReader.IsDBNull(12))
                {
                   // MessageBox.Show(TestCase_DAO.GetTestCaseById(Convert.ToInt32(sql_dataReader.GetDecimal(12))).Codigo);
                   // MessageBox.Show("ID procurado: "+Convert.ToInt32(sql_dataReader.GetDecimal(12)).ToString() + " --- " + TestCase_DAO.GetTestCaseById(Convert.ToInt32(sql_dataReader.GetDecimal(12))).Codigo );
                    
                    //Carrega o testCase inteiro

                    //TestCase tc_recuperado = TestCase_DAO.GetTestCaseById(Convert.ToInt32(sql_dataReader.GetDecimal(12)));

                    passoDoRoteiro.SelectedCase = passoDoRoteiro.TestCasePossiveis.First(x => x.Id == Convert.ToInt32(sql_dataReader.GetDecimal(12)));
                    //passoDoRoteiro.SelectedCase = passoDoRoteiro.TestCasePossiveis.First(x => x.Codigo == tc_recuperado.Codigo);

                    //foreach (TestCase test in passoDoRoteiro.TestCasePossiveis)
                    //{
                    //    if (test.Codigo == tc_recuperado.Codigo)
                    //    {
                    //        passoDoRoteiro.SelectedCase = test;
                    //    }
                    //}
                }

                if (!sql_dataReader.IsDBNull(13))
                {
                    passoDoRoteiro.Ordem = Convert.ToInt32(sql_dataReader.GetDecimal(13));
                }
                else
                {
                    passoDoRoteiro.Ordem = testSuite.PassosDoRoteiro.Count + 1;
                }
             
                testSuite.PassosDoRoteiro.Add(passoDoRoteiro);
            }

            DBConnection.Close();

        }
    }
}
