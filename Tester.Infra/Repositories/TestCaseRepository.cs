using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UI_test_player_TD.Model;

namespace UI_test_player_TD.DB
{
    public static class TestCase_DAO
    {
        public static void Salvar(TestCase testCase)
        {
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();

                try
                {

                    //Carrega Registros
                    using (OracleCommand sql_cmd = new OracleCommand(@"
                        SELECT * FROM TESTCASE WHERE COD_TESTCASE= :COD_TESTCASE",
                    con)) 
                    {
                        sql_cmd.Parameters.Clear();
                        sql_cmd.Parameters.Add(":COD_TESTCASE", testCase.Id);

                        OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();

                        if (sql_dataReader.HasRows)
                        {
                            //MessageBox.Show("achou");

                            sql_cmd.CommandText = @"UPDATE TESTCASE 
                        SET 
                            NOME= :NOME,
                            TXT_DESCRICAO= :TXT_DESCRICAO,
                            TXT_CODIGO= :TXT_CODIGO,
                            DT_ULTIMA_EXEC= :DT_ULTIMA_EXEC,
                            TXT_FUNC_NOME= :TXT_FUNC_NOME,
                            TXT_MODULO_NOME= :TXT_MODULO_NOME,
                            TXT_PRE_COND= :TXT_PRE_COND,
                            TXT_POS_COND= :TXT_POS_COND,
                            NUM_ULTIMA_SITUACAO= :NUM_ULTIMA_SITUACAO,
                            NUM_TOTAL_EXEC= :NUM_TOTAL_EXEC,
                            NUM_TOTAL_APROV= :NUM_TOTAL_APROV,
                            TEMPO_ESTIMADO= :TEMPO_ESTIMADO

                        WHERE COD_TESTCASE= :COD_TESTCASE";

                            testCase.UltimaVezExecutado = DateTime.Now;
                            sql_cmd.Parameters.Clear();
                            sql_cmd.Parameters.Add(":NOME", testCase.Nome);
                            sql_cmd.Parameters.Add(":TXT_DESCRICAO", testCase.Descricao);
                            sql_cmd.Parameters.Add(":TXT_CODIGO", testCase.Codigo);
                            sql_cmd.Parameters.Add(":DT_ULTIMA_EXEC", testCase.UltimaVezExecutado);
                            sql_cmd.Parameters.Add(":TXT_FUNC_NOME", testCase.Funcao);
                            sql_cmd.Parameters.Add(":TXT_MODULO_NOME", testCase.Modulo);
                            sql_cmd.Parameters.Add(":TXT_PRE_COND", testCase.PreCondicao);
                            sql_cmd.Parameters.Add(":TXT_POS_COND", testCase.PosCondicao);
                            int ultimaSituacao_NUM;

                            if (testCase.UltimaSituacao.HasValue)
                            {
                                if (testCase.UltimaSituacao.Value == true)
                                {
                                    ultimaSituacao_NUM = 1;
                                }
                                else
                                {
                                    ultimaSituacao_NUM = -1;
                                }
                            }
                            else
                            {
                                ultimaSituacao_NUM = 0;
                            }
                            sql_cmd.Parameters.Add(":NUM_ULTIMA_SITUACAO", ultimaSituacao_NUM);
                            sql_cmd.Parameters.Add(":NUM_TOTAL_EXEC", testCase.TotalExecutado);
                            sql_cmd.Parameters.Add(":NUM_TOTAL_APROV", testCase.TotalApr);
                            sql_cmd.Parameters.Add(":TEMPO_ESTIMADO", testCase.tempoEstimado);
                            sql_cmd.Parameters.Add(":COD_TESTCASE", testCase.Id);

                            //MessageBox.Show(testCase.UltimaVezExecutado.ToString("dd/MM/yyyy HH:mm:ss"));

                            sql_cmd.ExecuteNonQuery();


                            //Deletando Registro dos Passos
                            sql_cmd.CommandText = @"DELETE FROM PASSODOTESTE 
                        WHERE  COD_TESTCASE=:COD_TESTCASE";
                            sql_cmd.Parameters.Clear();
                            sql_cmd.Parameters.Add(":COD_TESTCASE", testCase.Id);
                            sql_cmd.ExecuteNonQuery();

                            //Colocando as Alterações e Novos Passos
                            foreach (PassoDoTeste passo in testCase.Passos)
                            {
                                sql_cmd.CommandText = @"
                            INSERT INTO PASSODOTESTE 
                            VALUES 
                            (:COD_TESTCASE, :COD_TELA, :COD_ACAO,
                            :TXT_PARAMETRO, :DEVE_FOTOGRAFAR, :DEVE_EXECUTAR,
                            :TEVE_SUCESSO, :NUM_ORDEM_SEQ, :TXT_RETORNO, :TXT_OBS, seq_cod_passo.nextval)";
                                sql_cmd.Parameters.Clear();

                                sql_cmd.Parameters.Add(":COD_TESTCASE", testCase.Id);
                                sql_cmd.Parameters.Add(":COD_TELA", passo.telaSelecionada.Id);
                                sql_cmd.Parameters.Add(":COD_ACAO", passo.acaoSelecionada.Id);
                                sql_cmd.Parameters.Add(":TXT_PARAMETRO", passo.Parametro);
                                int deveFotografar_NUM;
                                if (passo.deveFotografar)
                                {
                                    deveFotografar_NUM = 1;
                                }
                                else
                                {
                                    deveFotografar_NUM = 0;
                                }
                                sql_cmd.Parameters.Add(":DEVE_FOTOGRAFAR", deveFotografar_NUM);

                                int deveExecutar_NUM;
                                if (passo.deveExecutar)
                                {
                                    deveExecutar_NUM = 1;
                                }
                                else
                                {
                                    deveExecutar_NUM = 0;
                                }
                                sql_cmd.Parameters.Add(":DEVE_EXECUTAR", deveExecutar_NUM);

                                int teveSucesso_NUM;

                                if (passo.teveSucesso.HasValue)
                                {
                                    if (passo.teveSucesso.Value)
                                    {
                                        teveSucesso_NUM = 1;
                                    }
                                    else
                                    {
                                        teveSucesso_NUM = -1;
                                    }
                                }
                                else
                                {
                                    teveSucesso_NUM = 0;
                                }

                                sql_cmd.Parameters.Add(":TEVE_SUCESSO", teveSucesso_NUM);


                                sql_cmd.Parameters.Add(":NUM_ORDEM_SEQ", passo.OrdemSeq);
                                sql_cmd.Parameters.Add(":TXT_RETORNO", passo.Retorno);
                                sql_cmd.Parameters.Add(":TXT_OBS", passo.Obs);


                                sql_cmd.ExecuteNonQuery();
                            }

                        }
                        else
                        {

                            sql_cmd.CommandText = @"
                        INSERT INTO TESTCASE  
                        (COD_TESTCASE, NOME, TXT_DESCRICAO, TXT_CODIGO, TXT_FUNC_NOME,
                        TXT_MODULO_NOME, TXT_PRE_COND, TXT_POS_COND, COD_SISTEMA) 
                        
                            VALUES (SEQ_COD_TESTCASE.nextval,
                                :NOME,
                                :TXT_DESCRICAO,
                                :TXT_CODIGO,
                                :TXT_FUNC_NOME,
                                :TXT_MODULO_NOME,
                                :TXT_PRE_COND,
                                :TXT_POS_COND,
                                :COD_SISTEMA)
                        ";
                            sql_cmd.Parameters.Clear();

                            sql_cmd.Parameters.Add(":NOME", testCase.Nome);
                            sql_cmd.Parameters.Add(":TXT_DESCRICAO", testCase.Descricao);
                            sql_cmd.Parameters.Add(":TXT_CODIGO", testCase.Codigo);
                            sql_cmd.Parameters.Add(":TXT_FUNC_NOME", testCase.Funcao);
                            sql_cmd.Parameters.Add(":TXT_MODULO_NOME", testCase.Modulo);
                            sql_cmd.Parameters.Add(":TXT_PRE_COND", testCase.PreCondicao);
                            sql_cmd.Parameters.Add(":TXT_POS_COND", testCase.PosCondicao);
                            sql_cmd.Parameters.Add(":COD_SISTEMA", testCase.SistemaPai.Id);

                            sql_cmd.ExecuteNonQuery();

                            //                    sql_cmd = new OracleCommand(@"SELECT last_insert_rowid();
                            //                        ", DBConnection.con);

                            int lastId = Convert.ToInt32(sql_cmd.Parameters["COD_TESTCASE"]);
                            testCase.Id = lastId;


                            //MessageBox.Show("INSERT: " + testCase.Id.ToString());


                            //Deletando Registro dos Passos
                            sql_cmd.CommandText = @"DELETE  FROM PASSODOTESTE 
                        WHERE COD_TESTCASE=:COD_TESTCASE";
                            sql_cmd.Parameters.Clear();

                            sql_cmd.Parameters.Add(":COD_TESTCASE", testCase.Id);
                            sql_cmd.ExecuteNonQuery();

                            //Colocando as Alterações e Novos Passos
                            foreach (PassoDoTeste passo in testCase.Passos)
                            {
                                sql_cmd.CommandText = @"INSERT INTO PASSODOTESTE 
                            VALUES (:COD_TESTCASE,
                                    :COD_TELA,
                                    :COD_ACAO,
                                    :TXT_PARAMETRO, 
                                    :DEVE_FOTOGRAFAR,
                                    :DEVE_EXECUTAR,
                                    :TEVE_SUCESSO,
                                    :NUM_ORDEM_SEQ,
                                    :TXT_RETORNO, 
                                    :TXT_OBS,
                                    SEQ_COD_PASSO.nextval)";
                                sql_cmd.Parameters.Clear();
                                sql_cmd.Parameters.Add(":COD_TESTCASE", testCase.Id);
                                sql_cmd.Parameters.Add(":COD_TELA", passo.telaSelecionada.Id);
                                sql_cmd.Parameters.Add(":COD_ACAO", passo.acaoSelecionada.Id);
                                sql_cmd.Parameters.Add(":TXT_PARAMETRO", passo.Parametro);
                                sql_cmd.Parameters.Add(":DEVE_FOTOGRAFAR", passo.deveFotografar);
                                sql_cmd.Parameters.Add(":DEVE_EXECUTAR", passo.deveExecutar);
                                sql_cmd.Parameters.Add(":TEVE_SUCESSO", passo.teveSucesso);
                                sql_cmd.Parameters.Add(":NUM_ORDEM_SEQ", passo.OrdemSeq);
                                sql_cmd.Parameters.Add(":TXT_RETORNO", passo.Retorno);
                                sql_cmd.Parameters.Add(":TXT_OBS", passo.Obs);
                                sql_cmd.ExecuteNonQuery();
                            }

                        }
                    }
                    




                    //Fecha Conexão
                    con.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message + Environment.NewLine + ex.Source + Environment.NewLine + ex.StackTrace);
                }
                //se exister salva as alterações
                //se não exister - insere novo       
            }
             
        }

        public static void GetPassos(TestCase testCase) 
        {

                
                
                //Abre conexão
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                //Carrega Registros
                con.Open();

                using (OracleCommand sql_cmd = new OracleCommand(@"
                SELECT * FROM PassoDoTeste WHERE COD_TESTCASE=:COD_TESTCASE ORDER BY NUM_ORDEM_SEQ", con))
                {
                    sql_cmd.Parameters.Add(":COD_TESTCASE", testCase.Id);

                    OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();

                    //for (int i = 0; i < this.Passos.Count; i++)
                    //{
                    //    this.Passos.RemoveAt(i);
                    //}
                    //Passos = new ObservableCollection<PassoDoTeste>();

                    testCase.Passos.Clear();

                    while (sql_dataReader.Read())
                    {
                        PassoDoTeste passoDoTeste = new PassoDoTeste(testCase);

                        if (!sql_dataReader.IsDBNull(3))
                        {
                            passoDoTeste.Parametro = sql_dataReader.GetString(3);
                        }

                        if (!sql_dataReader.IsDBNull(4))
                        {
                            int deveFotografar_INT = Convert.ToInt32(sql_dataReader.GetDecimal(4));
                            if (deveFotografar_INT == 0)
                            {
                                passoDoTeste.deveFotografar = false;
                            }
                            else
                            {
                                passoDoTeste.deveFotografar = true;
                            }
                        }

                        if (!sql_dataReader.IsDBNull(5))
                        {
                            int deveExecutar_INT = Convert.ToInt32(sql_dataReader.GetDecimal(5));
                            if (deveExecutar_INT == 0)
                            {
                                passoDoTeste.deveExecutar = false;
                            }
                            else
                            {
                                passoDoTeste.deveExecutar = true;
                            }
                        }

                        if (!sql_dataReader.IsDBNull(7))
                        {
                            passoDoTeste.OrdemSeq = Convert.ToInt32(sql_dataReader.GetDecimal(7));
                        }

                        if (!sql_dataReader.IsDBNull(8))
                        {
                            passoDoTeste.Retorno = sql_dataReader.GetString(8);
                        }

                        if (!sql_dataReader.IsDBNull(9))
                        {
                            passoDoTeste.Obs = sql_dataReader.GetString(9);
                        }


                        if (!sql_dataReader.IsDBNull(6))
                        {
                            int teveSucesso_INT = Convert.ToInt32(sql_dataReader.GetDecimal(6));
                            if (teveSucesso_INT == -1)
                            {
                                passoDoTeste.teveSucesso = false;
                                passoDoTeste.SituationImg = PassoDoTeste.IMG_ERRO;

                            }
                            else
                            {
                                if (teveSucesso_INT == 0)
                                {
                                    passoDoTeste.teveSucesso = null;
                                    passoDoTeste.SituationImg = PassoDoTeste.IMG_QUEST;
                                }
                                else
                                {
                                    //teveSucesso == 1
                                    passoDoTeste.teveSucesso = true;
                                    passoDoTeste.SituationImg = PassoDoTeste.IMG_APPR;

                                }
                            }

                        }
                        else
                        {
                            passoDoTeste.teveSucesso = null;
                            passoDoTeste.SituationImg = PassoDoTeste.IMG_QUEST;
                        }


                        if (passoDoTeste.telasPossiveis == null)
                        {
                            //MessageBox.Show("passoDoTest.TelasPossiveis == null");
                        }
                        else
                        {
                            int idTelaSelecionada = Convert.ToInt32(sql_dataReader.GetDecimal(1));
                            passoDoTeste.telaSelecionada = passoDoTeste.telasPossiveis.First(x => x.Id == idTelaSelecionada);
                            //foreach (Tela tela in passoDoTeste.telasPossiveis)
                            //{
                            //    if (tela.Id == idTelaSelecionada)
                            //    {
                            //        passoDoTeste.telaSelecionada = tela;
                            //    }
                            //}
                        }


                        int idAcaoSelecionada = Convert.ToInt32(sql_dataReader.GetDecimal(2));
                        passoDoTeste.acaoSelecionada = passoDoTeste.acoesPossiveis.First(x => x.Id == idAcaoSelecionada);
                        //foreach (AcaoDyn acao in passoDoTeste.acoesPossiveis)
                        //{
                        //    if (acao.Id == idAcaoSelecionada)
                        //    {
                        //        passoDoTeste.acaoSelecionada = acao;
                        //    }
                        //}

                        testCase.Passos.Add(passoDoTeste);
                    }
                }

               
            }
    }

        public static void Deletar(TestCase testCase)
        {
                //Db existe
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();

                using (OracleCommand sql_cmd = new OracleCommand(@"DELETE FROM TESTCASE WHERE COD_TESTCASE = :COD_TESTCASE
                        ", con))
                {
                    sql_cmd.Parameters.Add(":COD_TESTCASE", testCase.Id);
                    sql_cmd.ExecuteNonQuery();
                }

                //Fecha Conexão
                con.Close();
            }

        }

        //public static List<TestCase> GetAll()
        //{
        //    List<TestCase> testCases = new List<TestCase>();

        //    //Abre conexão
        //    DBConnection.Connect();

        //        //Carrega Registros
        //        OracleCommand sql_cmd = new OracleCommand("SELECT * FROM TESTCASE", DBConnection.con);
        //        OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();


        //        while (sql_dataReader.Read())
        //        {
        //            TestCase testCase = new TestCase(sql_dataReader.GetString(3));

        //            testCase.Id = Convert.ToInt32(sql_dataReader.GetDecimal(0));
        //            if (!sql_dataReader.IsDBNull(1))
        //            {
        //                testCase.Nome = sql_dataReader.GetString(1);
        //            }
        //            if (!sql_dataReader.IsDBNull(2))
        //            {
        //                testCase.Descricao = sql_dataReader.GetString(2);
        //            }


        //            if (!sql_dataReader.IsDBNull(4))
        //            {
        //                testCase.UltimaVezExecutado = sql_dataReader.GetDateTime(4);
        //            }

        //            if (!sql_dataReader.IsDBNull(6))
        //                testCase.Funcao = sql_dataReader.GetString(6);

        //            if (!sql_dataReader.IsDBNull(7))
        //                testCase.Modulo = sql_dataReader.GetString(7);



        //            if (!sql_dataReader.IsDBNull(8))
        //                testCase.TotalExecutado = Convert.ToInt32(sql_dataReader.GetDecimal(8));
        //            if (!sql_dataReader.IsDBNull(10))
        //            {
        //                testCase.TotalApr = Convert.ToInt32(sql_dataReader.GetDecimal(10));
        //                testCase.TotalErr = testCase.TotalExecutado - testCase.TotalApr;
        //            }

        //            if (!sql_dataReader.IsDBNull(9))
        //            {
        //                testCase.UltimaSituacao = Convert.ToBoolean(sql_dataReader.GetDecimal(9));
        //                if (testCase.UltimaSituacao == true)
        //                {
        //                    testCase.testSituationImg = TestCase.IMG_APPR;
        //                }
        //                else
        //                {
        //                    testCase.testSituationImg = TestCase.IMG_ERRO;
        //                }
        //            }

        //            if (!sql_dataReader.IsDBNull(11))
        //                testCase.PreCondicao = sql_dataReader.GetString(11);

        //            if (!sql_dataReader.IsDBNull(12))
        //                testCase.PosCondicao = sql_dataReader.GetString(12);


        //            if (!sql_dataReader.IsDBNull(13))
        //                testCase.tempoEstimado = sql_dataReader.GetTimeSpan(13);
        //                //testCase.tempoEstimado = TimeSpan.Parse(DateTime.Parse(sql_dataReader["TempoEstimado"].ToString()).ToLongTimeString());


        //            testCases.Add(testCase);
        //        }



        //        foreach (TestCase testCase in testCases)
        //        {
        //            //MessageBox.Show(testCase.Id.ToString());
        //            OracleCommand sql_cmd2 = new OracleCommand("SELECT * FROM PassoDoTeste WHERE COD_TESTCASE=:COD_TESTCASE", DBConnection.con);
        //            sql_cmd2.Parameters.Add(":COD_TESTCASE", testCase.Id);


        //            OracleDataReader sql_dataReader2 = sql_cmd2.ExecuteReader();


        //            while (sql_dataReader2.Read())
        //            {
        //                PassoDoTeste passoDoTeste = new PassoDoTeste(testCase);
        //                passoDoTeste.TestCaseParent.Id = Convert.ToInt32(sql_dataReader2.GetDecimal(0));
        //                passoDoTeste.TelaParent.Id = Convert.ToInt32(sql_dataReader2.GetDecimal(1));
        //                foreach (Tela tela in passoDoTeste.telasPossiveis)
        //                {
        //                    if (tela.Id == passoDoTeste.TelaParent.Id)
        //                    {
        //                        passoDoTeste.telaSelecionada = tela;
        //                    }
        //                }
        //                passoDoTeste.acaoSelecionada.Id = Convert.ToInt32(sql_dataReader2.GetDecimal(2));
        //                foreach (AcaoDyn acao in passoDoTeste.acoesPossiveis)
        //                {
        //                    if (acao.Id == passoDoTeste.acaoSelecionada.Id)
        //                    {
        //                        passoDoTeste.acaoSelecionada = acao;
        //                    }
        //                }
        //                passoDoTeste.Parametro = sql_dataReader2.GetString(3);
        //                passoDoTeste.deveFotografar = Convert.ToBoolean(sql_dataReader2.GetDecimal(4));
        //                passoDoTeste.deveExecutar = Convert.ToBoolean(sql_dataReader2.GetDecimal(5));
        //                passoDoTeste.OrdemSeq = Convert.ToInt32(sql_dataReader2.GetDecimal(7));
        //                if (!sql_dataReader2.IsDBNull(6))
        //                {
        //                    passoDoTeste.teveSucesso = Convert.ToBoolean(sql_dataReader2.GetDecimal(6));
        //                    if (passoDoTeste.teveSucesso == true)
        //                    {
        //                        passoDoTeste.SituationImg = PassoDoTeste.IMG_APPR;
        //                    }
        //                    else
        //                    {
        //                        passoDoTeste.SituationImg = PassoDoTeste.IMG_ERRO;
        //                    }
        //                }
        //                else
        //                {
        //                    passoDoTeste.SituationImg = PassoDoTeste.IMG_QUEST;
        //                }

        //                passoDoTeste.Retorno = sql_dataReader2.GetString(8);
        //                passoDoTeste.Obs = sql_dataReader2.GetString(9);



        //                testCase.Passos.Add(passoDoTeste);
        //            }
        //        }






        //        //Fecha Conexão
        //        DBConnection.Close();
            

        //    //Retornar Lista
        //    return testCases;
        //}

        public static List<TestCase> GetAllFromSistema(Sistema sistema)
        {
            
            List<TestCase> testCases = new List<TestCase>();

                //Conectar ao banco
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();

                
                try
                {


                    //Carrega Registros
                    using (OracleCommand sql_cmd = new OracleCommand("SELECT * FROM TESTCASE where COD_SISTEMA = :COD_SISTEMA", con))
                    {
                        sql_cmd.Parameters.Add(":COD_SISTEMA", sistema.Id);
                        OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();


                        while (sql_dataReader.Read())
                        {
                            TestCase testCase = new TestCase(sql_dataReader.GetString(3));
                            testCase.SistemaPai = sistema;
                            testCase.Id = Convert.ToInt32(sql_dataReader.GetDecimal(0));
                            if (!sql_dataReader.IsDBNull(1))
                            {
                                testCase.Nome = sql_dataReader.GetString(1);
                            }
                            if (!sql_dataReader.IsDBNull(2))
                            {
                                testCase.Descricao = sql_dataReader.GetString(2);
                            }
                            if (!sql_dataReader.IsDBNull(4))
                            {
                                testCase.UltimaVezExecutado = sql_dataReader.GetDateTime(4);
                            }
                            if (!sql_dataReader.IsDBNull(6))
                                testCase.Funcao = sql_dataReader.GetString(6);

                            if (!sql_dataReader.IsDBNull(7))
                                testCase.Modulo = sql_dataReader.GetString(7);

                            if (!sql_dataReader.IsDBNull(8))
                                testCase.TotalExecutado = Convert.ToInt32(sql_dataReader.GetDecimal(8));


                            if (!sql_dataReader.IsDBNull(10))
                            {
                                testCase.TotalApr = Convert.ToInt32(sql_dataReader.GetDecimal(10));
                                testCase.TotalErr = testCase.TotalExecutado - testCase.TotalApr;
                            }

                            if (!sql_dataReader.IsDBNull(9))
                            {
                                int ultimaSituacao_NUM = Convert.ToInt32(sql_dataReader.GetDecimal(9));
                                if (ultimaSituacao_NUM == 1)
                                {
                                    testCase.testSituationImg = TestCase.IMG_APPR;
                                    testCase.UltimaSituacao = true;
                                }
                                else
                                {
                                    if (ultimaSituacao_NUM == 0)
                                    {
                                        testCase.UltimaSituacao = null;
                                        testCase.testSituationImg = TestCase.IMG_QUEST;
                                    }
                                    else
                                    {
                                        //-1
                                        testCase.UltimaSituacao = false;
                                        testCase.testSituationImg = TestCase.IMG_ERRO;
                                    }
                                }

                            }

                            if (!sql_dataReader.IsDBNull(11))
                                testCase.PreCondicao = sql_dataReader.GetString(11);

                            if (!sql_dataReader.IsDBNull(12))
                                testCase.PosCondicao = sql_dataReader.GetString(12);


                            if (!sql_dataReader.IsDBNull(13))
                            {
                                //testCase.tempoEstimado = TimeSpan.Parse(DateTime.Parse(sql_dataReader["TempoEstimado"].ToString()).ToLongTimeString());
                                testCase.tempoEstimado = sql_dataReader.GetTimeSpan(13);
                            }


                            testCases.Add(testCase);
                        }



                    }
                    
                    //foreach (TestCase testCase in testCases)
                    //{
                    //    ////MessageBox.Show(testCases.Count.ToString());
                    //    //if (DBConnection.con.State == ConnectionState.Closed)
                    //    //{
                    //    //    DBConnection.Connect();
                    //    //}
                    //    //MessageBox.Show(testCase.Id.ToString());
                    //    using (OracleCommand sql_cmd2 = new OracleCommand("SELECT * FROM PassoDoTeste WHERE COD_TESTCASE=:COD_TESTCASE", con))
                    //    {
                    //        sql_cmd2.Parameters.Add("@COD_TESTCASE", testCase.Id);

                    //        OracleDataReader sql_dataReader2 = sql_cmd2.ExecuteReader();


                    //        while (sql_dataReader2.Read())
                    //        {
                    //            PassoDoTeste passoDoTeste = new PassoDoTeste(testCase);


                    //            if (passoDoTeste.telasPossiveis == null)
                    //            {
                    //                //MessageBox.Show("passoDoTest.TelasPossiveis == null");
                    //            }

                    //            if (!sql_dataReader2.IsDBNull(3))
                    //                passoDoTeste.Parametro = sql_dataReader2.GetString(3);


                    //            if (!sql_dataReader2.IsDBNull(4))
                    //            {
                    //                int deveFotografar_INT = Convert.ToInt32(sql_dataReader2.GetDecimal(4));
                    //                if (deveFotografar_INT == 0)
                    //                {
                    //                    passoDoTeste.deveFotografar = false;
                    //                }
                    //                else
                    //                {
                    //                    passoDoTeste.deveFotografar = true;
                    //                }
                    //            }

                    //            if (!sql_dataReader2.IsDBNull(5))
                    //            {
                    //                int deveExecutar_INT = Convert.ToInt32(sql_dataReader2.GetDecimal(5));
                    //                if (deveExecutar_INT == 0)
                    //                {
                    //                    passoDoTeste.deveExecutar = false;
                    //                }
                    //                else
                    //                {
                    //                    passoDoTeste.deveExecutar = true;
                    //                }
                    //            }

                    //            if (!sql_dataReader2.IsDBNull(7))
                    //                passoDoTeste.OrdemSeq = Convert.ToInt32(sql_dataReader2.GetDecimal(7));

                    //            if (!sql_dataReader2.IsDBNull(6))
                    //            {
                    //                int teveSucesso_INT = Convert.ToInt32(sql_dataReader2.GetDecimal(6));
                    //                if (teveSucesso_INT == -1)
                    //                {
                    //                    passoDoTeste.teveSucesso = false;
                    //                }
                    //                else
                    //                {
                    //                    if (teveSucesso_INT == 0)
                    //                    {
                    //                        passoDoTeste.teveSucesso = null;
                    //                    }
                    //                    else
                    //                    {
                    //                        //teveSucesso == 1
                    //                        passoDoTeste.teveSucesso = true;
                    //                    }
                    //                }

                    //                if (passoDoTeste.teveSucesso == true)
                    //                {
                    //                    passoDoTeste.SituationImg = PassoDoTeste.IMG_APPR;
                    //                }
                    //                else
                    //                {
                    //                    passoDoTeste.SituationImg = PassoDoTeste.IMG_ERRO;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                passoDoTeste.teveSucesso = null;
                    //                passoDoTeste.SituationImg = PassoDoTeste.IMG_QUEST;
                    //            }



                    //            if (passoDoTeste.telasPossiveis == null)
                    //            {
                    //                //MessageBox.Show("passoDoTest.TelasPossiveis == null");
                    //            }
                    //            else
                    //            {
                    //                int idTelaSelecionada = Convert.ToInt32(sql_dataReader2.GetDecimal(1));
                    //                foreach (Tela tela in passoDoTeste.telasPossiveis)
                    //                {
                    //                    if (tela.Id == idTelaSelecionada)
                    //                    {
                    //                        passoDoTeste.telaSelecionada = tela;
                    //                    }
                    //                }
                    //            }


                    //            int idAcaoSelecionada = Convert.ToInt32(sql_dataReader2.GetDecimal(2));

                    //            foreach (AcaoDyn acao in passoDoTeste.acoesPossiveis)
                    //            {
                    //                if (acao.Id == idAcaoSelecionada)
                    //                {
                    //                    passoDoTeste.acaoSelecionada = acao;
                    //                }
                    //            }


                    //            testCase.Passos.Add(passoDoTeste);

                    //        }
                    //    }
                       

                    //}
                }
                catch (Exception exc)
                {
                    //MessageBox.Show(exc.Message + exc.Source + exc.StackTrace);
                }
                finally
                {

                    con.Close();     


                }


                //Fecha Conexão
            }
            //Retornar Lista
            return testCases;
        }

        public static TestCase GetTestCaseById(int id)
        {
            TestCase testCase;
            //Db existe
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();

                //Carrega Registros
                using (OracleCommand sql_cmd = new OracleCommand(@"SELECT * FROM TESTCASE WHERE COD_TESTCASE = :COD_TESTCASE
                        ", con))
                {
                    sql_cmd.Parameters.Add(":COD_TESTCASE", id);

                    OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();

                    sql_dataReader.Read();
                     testCase = new TestCase(sql_dataReader.GetString(3));

                    testCase.Id = Convert.ToInt32(sql_dataReader.GetDecimal(0));

                    if (!sql_dataReader.IsDBNull(14))
                    {
                        testCase.SistemaPai = Sistema_DAO.getSistemaByID(Convert.ToInt32(sql_dataReader.GetDecimal(14)));
                    }

                    if (!sql_dataReader.IsDBNull(1))
                    {
                        testCase.Nome = sql_dataReader.GetString(1);
                    }

                    if (!sql_dataReader.IsDBNull(2))
                    {
                        testCase.Descricao = sql_dataReader.GetString(2);
                    }
                    if (!sql_dataReader.IsDBNull(4))
                    {
                        testCase.UltimaVezExecutado = sql_dataReader.GetDateTime(4);
                    }
                    if (!sql_dataReader.IsDBNull(6))
                        testCase.Funcao = sql_dataReader.GetString(6);

                    if (!sql_dataReader.IsDBNull(7))
                        testCase.Modulo = sql_dataReader.GetString(7);

                    if (!sql_dataReader.IsDBNull(8))
                        testCase.TotalExecutado = Convert.ToInt32(sql_dataReader.GetDecimal(8));


                    if (!sql_dataReader.IsDBNull(10))
                    {
                        testCase.TotalApr = Convert.ToInt32(sql_dataReader.GetDecimal(10));
                        testCase.TotalErr = testCase.TotalExecutado - testCase.TotalApr;
                    }

                    if (!sql_dataReader.IsDBNull(9))
                    {
                        int ultimaSituacao_NUM = Convert.ToInt32(sql_dataReader.GetDecimal(9));
                        if (ultimaSituacao_NUM == 1)
                        {
                            testCase.testSituationImg = TestCase.IMG_APPR;
                            testCase.UltimaSituacao = true;
                        }
                        else
                        {
                            if (ultimaSituacao_NUM == 0)
                            {
                                testCase.UltimaSituacao = null;
                                testCase.testSituationImg = TestCase.IMG_QUEST;
                            }
                            else
                            {
                                //-1
                                testCase.UltimaSituacao = false;
                                testCase.testSituationImg = TestCase.IMG_ERRO;
                            }
                        }

                    }

                    if (!sql_dataReader.IsDBNull(11))
                        testCase.PreCondicao = sql_dataReader.GetString(11);

                    if (!sql_dataReader.IsDBNull(12))
                        testCase.PosCondicao = sql_dataReader.GetString(12);


                    if (!sql_dataReader.IsDBNull(13))
                    {
                        //testCase.tempoEstimado = TimeSpan.Parse(DateTime.Parse(sql_dataReader["TempoEstimado"].ToString()).ToLongTimeString());
                        testCase.tempoEstimado = sql_dataReader.GetTimeSpan(13);
                    }

                    TestCase_DAO.GetPassos(testCase);
                }

                con.Close();

        
            }
           
            return testCase;
            //Fecha Conexão
            
        }

        public static void CopyTestCase(string nomeCtf, TestCase selectedCase)
        {
            try
            {


                using (OracleConnection con = new OracleConnection(DBConnection.conString))
                {
                    con.Open();
                    //Carrega Registros
                    using (OracleCommand sql_cmd = new OracleCommand(@"
                INSERT into testCase (Cod_Testcase, Nome, Txt_Descricao, Txt_Codigo, 
                Txt_Func_Nome, Txt_Modulo_Nome, Txt_Pre_Cond, Txt_Pos_Cond, Cod_Sistema)
              
                VALUES (SEQ_COD_TESTCASE.NEXTVAL, :Nome, :Txt_Descricao, :NewCTF_TXT_COD, 
                :Txt_Func_Nome, :Txt_Modulo_Nome, :Txt_Pre_Cond, :Txt_Pos_Cond, :Cod_Sistema)

                RETURNING Cod_Testcase INTO :newId
                ",
                    con))
                    {
                        sql_cmd.Parameters.Add(":Nome", selectedCase.Nome);
                        sql_cmd.Parameters.Add(":Txt_Descricao", selectedCase.Descricao);
                        sql_cmd.Parameters.Add(":NewCTF_TXT_COD", nomeCtf);
                        sql_cmd.Parameters.Add(":Txt_Func_Nome", selectedCase.Funcao);
                        sql_cmd.Parameters.Add(":Txt_Modulo_Nome", selectedCase.Modulo);
                        sql_cmd.Parameters.Add(":Txt_Pre_Cond", selectedCase.PreCondicao);
                        sql_cmd.Parameters.Add(":Txt_Pos_Cond", selectedCase.PosCondicao);
                        sql_cmd.Parameters.Add(":Cod_Sistema", selectedCase.SistemaPai.Id);
                        sql_cmd.Parameters.Add(":newId", OracleDbType.Decimal, ParameterDirection.ReturnValue);

                        sql_cmd.ExecuteNonQuery();

                        int newId = Convert.ToInt32(sql_cmd.Parameters[":newId"].Value.ToString());
                        //for (int i = 0; i < sql_cmd.Parameters.Count; i++)
                        //{
                        //    MessageBox.Show(i.ToString() + ": " + sql_cmd.Parameters[i] + "..  value: " + sql_cmd.Parameters[i].Value.ToString());

                        //}

                        //Copiar Todos os ids dos Passos
                        List<int> idPassos = new List<int>();
                        using (OracleCommand sql_cmd3 = new OracleCommand(@"SELECT * FROM passodoteste WHERE cod_testcase = :cod_testcase ", con))
                        {
                            sql_cmd3.Parameters.Add(":cod_testcase", selectedCase.Id);
                            var sql_cmd3_reader = sql_cmd3.ExecuteReader();
                            while (sql_cmd3_reader.Read())
                            {
                                idPassos.Add(Convert.ToInt32(sql_cmd3_reader.GetDecimal(10)));
                            }
                        }


                        foreach (var passo in idPassos)
                        {
                            //MessageBox.Show("Copying Passo w/ ID: " + passo.ToString());

                            using (OracleCommand sql_cmd2 = new OracleCommand(@"
                            INSERT INTO PASSODOTESTE (COD_TESTCASE,COD_TELA,COD_ACAO,TXT_PARAMETRO,DEVE_FOTOGRAFAR,
                                DEVE_EXECUTAR,TEVE_SUCESSO,NUM_ORDEM_SEQ,TXT_RETORNO,TXT_OBS, COD_PASSO)
                
                            SELECT :COD_NEW_ID,COD_TELA,COD_ACAO,TXT_PARAMETRO,DEVE_FOTOGRAFAR,DEVE_EXECUTAR,
                                NULL,NUM_ORDEM_SEQ,TXT_RETORNO,TXT_OBS,SEQ_COD_PASSO.NEXTVAL 
                                    FROM PASSODOTESTE WHERE COD_PASSO = :COD_PASSO
                            ", con))
                            {
                                sql_cmd2.Parameters.Add(":COD_NEW_ID", newId); //Id do novo caso de teste
                                sql_cmd2.Parameters.Add(":COD_PASSO", passo); //Id do Passo
                                sql_cmd2.ExecuteNonQuery();
                            }


                        }


                    }


                    con.Close();
                }

               
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + ex.Source + ex.StackTrace);
            }
        }

        public static int GetCount()
        {
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();

                using (OracleCommand sql_cmd = new OracleCommand(@"SELECT COUNT(*) FROM TESTCASE", con))
                {

                    int count = Convert.ToInt32(sql_cmd.ExecuteScalar());
                    return count;
                }

            }

        }

        public static TimeSpan GetAvarageTime()
        {
            List<TimeSpan> temposDeExecucao = new List<TimeSpan>();

            //Conectar ao banco
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();
                try
                {
                    //Carrega Registros
                    using (OracleCommand sql_cmd = new OracleCommand("SELECT * FROM TESTCASE", con))
                    {
                        OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();


                        while (sql_dataReader.Read())
                        {
                            if (!sql_dataReader.IsDBNull(13))
                            {
                                temposDeExecucao.Add(sql_dataReader.GetTimeSpan(13));
                            }
                        }
                    }
                   
                }
                catch (Exception exc)
                {
                    //MessageBox.Show(exc.Message + exc.Source + exc.StackTrace);
                }
                finally
                {

                    con.Close();

                }

            }



            double doubleAverageTicks = temposDeExecucao.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

            //Retornar Lista
            return new TimeSpan(longAverageTicks);
        }

        public static int GetCountApprovedTests()
        {
            int count;
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();

                using (OracleCommand sql_cmd = new OracleCommand(@"SELECT COUNT(*) FROM TESTCASE where num_ultima_situacao=1", con))
                {

                    count = Convert.ToInt32(sql_cmd.ExecuteScalar());

                }


            }
            return count;
        }

        public static int GetCountErrorTests()
        {
            int count;
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();

                using (OracleCommand sql_cmd = new OracleCommand(@"SELECT COUNT(*) FROM TESTCASE where num_ultima_situacao=-1", con))
                {

                    count = Convert.ToInt32(sql_cmd.ExecuteScalar());

                }


            }
            return count;
        }

        public static int GetCountNotExecutedTests()
        {
            int count;
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();

                using (OracleCommand sql_cmd = new OracleCommand(@"SELECT COUNT(*) FROM TESTCASE where num_ultima_situacao=0 or num_ultima_situacao is null", con))
                {

                    count = Convert.ToInt32(sql_cmd.ExecuteScalar());

                }


            }
            return count;
        }
    }
}
