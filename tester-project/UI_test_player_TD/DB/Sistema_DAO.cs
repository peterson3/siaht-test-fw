using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_test_player_TD.Model;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using System.Windows;

namespace UI_test_player_TD.DB
{
    public static class Sistema_DAO
    {
        public static ObservableCollection<Sistema> getAllSistemas()
        {
            ObservableCollection<Sistema> sistemas = new ObservableCollection<Sistema>();
            //Conectar Ao Banco
            DBConnection.Connect();

            //Testar uma consulta no BD;;

            string sqlQuery = @"select * from test_player.sistema";
            OracleCommand cmd = new OracleCommand(sqlQuery, DBConnection.con);
            cmd.CommandType = System.Data.CommandType.Text;

            OracleDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Sistema sistema = new Sistema(dr.GetString(1).ToString());
                sistema.Id = Convert.ToInt32(dr.GetDecimal(0));
                if (!dr.IsDBNull(2))
                    sistema.homeURL = dr.GetString(2).ToString();

                sistemas.Add(sistema);
            }
            DBConnection.Close();
            return sistemas;
        }

        public static Sistema getSistemaByID(int id)
        {
            //Conectar Ao Banco
            DBConnection.Connect();

            //Testar uma consulta no BD;;

            string sqlQuery = @"select * from test_player.sistema where cod_sistema = :cod_sistema";
            OracleCommand cmd = new OracleCommand(sqlQuery, DBConnection.con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add(":cod_sistema", id);

            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
                Sistema sistema = new Sistema(dr.GetString(1).ToString());
                sistema.Id = Convert.ToInt32(dr.GetDecimal(0));
                if (!dr.IsDBNull(2))
                    sistema.homeURL = dr.GetString(2).ToString();            

            DBConnection.Close();

            return sistema;
        }

        public static void Salvar(Sistema sistema)
        {
            //verificar se o caso de teste existe no banco 

            //Conectar ao banco
            DBConnection.Connect();

            OracleCommand conn = new OracleCommand(@"INSERT INTO SISTEMA                    
            VALUES (seq_cod_sistema.nextval, :nome, :homeUrl)" ,DBConnection.con);
            
            //Carrega Registros
            conn.Parameters.Add(":nome", sistema.Nome);
            conn.Parameters.Add(":homeUrl", sistema.homeURL);
            
            conn.ExecuteNonQuery();

            int lastId = Convert.ToInt32(conn.Parameters["cod_sistema"]);
            sistema.Id = lastId;

            ////Fecha Conexão
            DBConnection.Close();
        }

        public static void Deletar(Sistema sistema)
        {

                //Conecta
                DBConnection.Connect();

                OracleCommand conn = new OracleCommand(@"DELETE FROM SISTEMA WHERE Cod_Sistema = :Cod_Sistema
                        ", DBConnection.con);

                conn.Parameters.Add(":Cod_Sitema", sistema.Id);
                conn.ExecuteNonQuery();
                
                //Fecha Conexão
                DBConnection.Close();
            
        }

        public static void Update(Sistema sistema)
        {
            //Conectar no Banco
            DBConnection.Connect();
            
            //Abre conexão
            OracleCommand cmd = new OracleCommand(@"
            UPDATE SISTEMA SET NOME= :NOME, URL= :HOMEURL WHERE COD_SISTEMA= :COD_SISTEMA
                    ", DBConnection.con);
            
            cmd.Parameters.Add(":NOME", sistema.Nome);
            cmd.Parameters.Add(":URL", sistema.homeURL);
            cmd.Parameters.Add(":COD_SISTEMA", sistema.Id);
            
            cmd.ExecuteNonQuery();
            
            
            //Fecha Conexão
            DBConnection.Close();
        }
    }
}
