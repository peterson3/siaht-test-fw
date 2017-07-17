using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_test_player_TD.Model;

namespace UI_test_player_TD.DB
{
    public static class Tela_DAO
    {
        public static ObservableCollection<Tela> getAllTelas(Sistema sistema)
        {
            ObservableCollection<Tela> telas = new ObservableCollection<Tela>();
 
                //Conectar no BD
                DBConnection.Connect();

                OracleCommand sql_cmd = new OracleCommand(@"SELECT * FROM TELA WHERE Cod_Sistema= :Cod_Sistema"
                    , DBConnection.con);
                sql_cmd.Parameters.Add(":Cod_Sistema", sistema.Id);

                OracleDataReader dr = sql_cmd.ExecuteReader();

                while (dr.Read())
                {
                    Tela tela = new Tela(dr.GetString(1), sistema);
                    tela.Id = Convert.ToInt32(dr.GetDecimal(0));
                    telas.Add(tela);
                }
                //Fecha Conexão
                DBConnection.Close();
            

            return telas;
        }

        public static void Salvar(Tela tela)
        {
            //verificar se o caso de teste existe no banco 

            //Conectar ao banco
            DBConnection.Connect();

            //Carrega Registros
            OracleCommand sql_cmd;

            sql_cmd = new OracleCommand(@"INSERT INTO TELA  (Cod_Tela, nome, Cod_Sistema) 
                        VALUES (seq_cod_tela.nextval, :nome, :Cod_Sistema)
                    ", DBConnection.con);

            sql_cmd.Parameters.Add(":nome", tela.Nome);
            sql_cmd.Parameters.Add(":Cod_Sistema", tela.SistemaPai.Id);
            sql_cmd.ExecuteNonQuery();

            int lastId = Convert.ToInt32(sql_cmd.Parameters["Cod_tela"]);
            tela.Id = lastId;

            //Fecha Conexão
            DBConnection.Close();
            
        }

        public static void Deletar(Tela tela)
        {
            DBConnection.Connect();

                //Carrega Registros
                OracleCommand sql_cmd;

                sql_cmd = new OracleCommand(@"DELETE FROM Tela WHERE Cod_Tela = :Cod_Tela
                        ", DBConnection.con);

                sql_cmd.Parameters.Add("@Cod_Tela", tela.Id);
                sql_cmd.ExecuteNonQuery();
                
            //Fecha Conexão
            DBConnection.Close();
            
        }
    }
}
