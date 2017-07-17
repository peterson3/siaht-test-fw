using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_test_player_TD.Model
{
    public static class Settings
    {
        public static string ctfsPath = Environment.CurrentDirectory + "\\ctf\\created";
        public static string BrowserDesc = "IE10";
        public static string ServerUrl = "10.10.100.147";
        public static string DataBase = "HOMO_MED";
        public static string ProductName = "TopSaúde - Gestão";
        public static string ProductVersion = "11";
        public static string Tester = "Auto TestPlayer";

        public static bool IgnorarFalha = false;
        public static bool CloseBrowserAfterTestCase = true;

    }
}
