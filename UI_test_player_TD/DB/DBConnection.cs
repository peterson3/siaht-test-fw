using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;

namespace UI_test_player_TD.DB
{
    public static class DBConnection
    {
        public static string conString = @"Data Source=
                              (DESCRIPTION =
                                (ADDRESS_LIST =
                                  (ADDRESS = (PROTOCOL = TCP)(HOST = 223.223.2.180)(PORT = 1521))
                                )
                                (CONNECT_DATA =
                                  (SERVICE_NAME = desenv11)
                                )
                              );"
                        + "User Id=test_player;Password=td;";


        //public static OracleConnection con { get; set; } 

        //public static void Connect()
        //{
        //    DBConnection.con = new OracleConnection();
        //    DBConnection.con.ConnectionString = DBConnection.conString;

        //    if (DBConnection.con.State != System.Data.ConnectionState.Open)
        //        DBConnection.con.Open();
        //}

        //public static void Close()
        //{
        //    DBConnection.con.Close();
        //    DBConnection.con.Dispose();
        //}



    }
}
