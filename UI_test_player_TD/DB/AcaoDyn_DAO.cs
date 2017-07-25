using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_test_player_TD.Model;

namespace UI_test_player_TD.DB
{
    public static class AcaoDyn_DAO
    {
        public static void Update(AcaoDyn acaoDyn)
        {
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();

                try
                {
                    //Carrega Registros

                    using (OracleCommand sql_cmd = new OracleCommand(@"UPDATE ACAO SET NOME= :NOME,
                                            TXT_TOOLTIP= :TXT_TOOLTIP,
                                            REQUERPARAMETRO= :REQUERPARAMETRO, 
                                            COD_TELA = :COD_TELA,
                                            TXT_CODESCRIPT= :TXT_CODESCRIPT
                                            WHERE COD_ACAO= :COD_ACAO
                        ", con))
                    {
                        sql_cmd.Parameters.Add(":NOME", acaoDyn.Nome);
                        sql_cmd.Parameters.Add(":TXT_TOOLTIP", acaoDyn.Tooltip);

                        int requerParam_NUM;
                        if (acaoDyn.requerParametro)
                        {
                            requerParam_NUM = 1;
                        }
                        else
                        {
                            requerParam_NUM = 0;
                        }
                        sql_cmd.Parameters.Add(":REQUERPARAMETRO", requerParam_NUM);
                        sql_cmd.Parameters.Add(":COD_TELA", acaoDyn.TelaPai.Id);
                        sql_cmd.Parameters.Add(":TXT_CODESCRIPT", acaoDyn.CodeScript);
                        sql_cmd.Parameters.Add(":COD_ACAO", acaoDyn.Id);


                        sql_cmd.ExecuteNonQuery();
                        //Fecha Conexão
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.Source + Environment.NewLine + ex.StackTrace);
                }
                finally
                {
                    con.Close();
                }
            }

         
          }

        public static void Deletar(AcaoDyn acaoDyn)
        {

                //Abre conexão
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();
                //Carrega Registros

                using (OracleCommand sql_cmd = new OracleCommand(@"DELETE FROM ACAO WHERE COD_ACAO = :COD_ACAO
                        ", con))
                {
                    sql_cmd.Parameters.Add(":COD_ACAO", acaoDyn.Id);
                    sql_cmd.ExecuteNonQuery();
                }



                //Fecha Conexão
                con.Close();
            }           
        }

        public static void Salvar(AcaoDyn acaoDyn)
        {

                //Abre conexão
               using (OracleConnection con = new OracleConnection(DBConnection.conString))
               {
                   con.Open();
                   //Carrega Registros
                   using (OracleCommand sql_cmd = new OracleCommand(@"INSERT INTO ACAO  (COD_ACAO, NOME, TXT_TOOLTIP, REQUERPARAMETRO, COD_TELA , TXT_CODESCRIPT) 
                            VALUES (seq_acaodyn.nextval, :NOME, :TXT_TOOLTIP, :REQUERPARAMETRO, :COD_TELA, :TXT_CODESCRIPT)
                        ", con))
                   {
                       sql_cmd.Parameters.Add(":NOME", acaoDyn.Nome);
                       sql_cmd.Parameters.Add(":TXT_TOOLTIP", acaoDyn.Tooltip);

                       int requerParam_NUMBER;
                       if (acaoDyn.requerParametro)
                       {
                           requerParam_NUMBER = 1;
                       }
                       else
                       {
                           requerParam_NUMBER = 0;
                       }
                       sql_cmd.Parameters.Add(":REQUERPARAMETRO", requerParam_NUMBER);


                       sql_cmd.Parameters.Add(":COD_TELA", acaoDyn.TelaPai.Id);
                       sql_cmd.Parameters.Add(":TXT_CODESCRIPT", acaoDyn.CodeScript);

                       sql_cmd.ExecuteNonQuery();


                       int lastId = Convert.ToInt32(sql_cmd.Parameters["COD_ACAO"]);
                       acaoDyn.Id = lastId;


                   }

                   //Fecha Conexão
                   con.Close();
                  
               }
            
        }

        public static ObservableCollection<AcaoDyn> getAllActionsFromTela(Tela tela)
        {
            ObservableCollection<AcaoDyn> acoes = new ObservableCollection<AcaoDyn>();
            //Conectar ao banco

                //Db existe
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();
                //Carrega Registros
                using (OracleCommand sql_cmd = new OracleCommand("SELECT * FROM ACAO where COD_TELA = :COD_TELA", con))
                {
                    sql_cmd.Parameters.Add(":COD_TELA", tela.Id);
                    OracleDataReader sql_dataReader = sql_cmd.ExecuteReader();


                    while (sql_dataReader.Read())
                    {
                        AcaoDyn acao = new AcaoDyn(sql_dataReader.GetString(1), tela);
                        acao.Id = Convert.ToInt32(sql_dataReader.GetDecimal(0));
                        if (!sql_dataReader.IsDBNull(5))
                        {
                            acao.CodeScript = sql_dataReader.GetString(5);
                        }

                        if (!sql_dataReader.IsDBNull(3))
                        {
                            acao.requerParametro = Convert.ToBoolean(sql_dataReader.GetDecimal(3));
                        }

                        if (!sql_dataReader.IsDBNull(2))
                        {
                            acao.Tooltip = sql_dataReader.GetString(2);
                        }

                        acoes.Add(acao);
                    }
                }

                //Fecha Conexão
                con.Close();
            }


            
            return acoes;
        }

        public static int GetCount()
        {
            int count = 0;
            //Db existe
            using (OracleConnection con = new OracleConnection(DBConnection.conString))
            {
                con.Open();
                using (OracleCommand sql_cmd = new OracleCommand(@"SELECT COUNT(*) FROM ACAO", con))
                {
                   count = Convert.ToInt32(sql_cmd.ExecuteScalar());
                }

                con.Close();
            }
            return count;
        }
    }
}
