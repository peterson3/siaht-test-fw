using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tester_webapp.ViewModel
{
    public class DashboardViewModel
    {
        public int testCount { get; set; }
        public int suiteCount { get; set; }
        public int sistemaCount { get; set; }
        public string avarageTime { get; set; }
        public int aprTests { get; set; }
        public int repTests { get; set; }
        public int pendTests { get; set; }


        public DashboardViewModel()
        {
            testCount = UI_test_player_TD.DB.TestCase_DAO.GetCount();
            suiteCount = UI_test_player_TD.DB.TestSuite_DAO.GetCount();
            sistemaCount = UI_test_player_TD.DB.Sistema_DAO.GetCount();
            avarageTime = UI_test_player_TD.DB.TestCase_DAO.GetAvarageTime().ToString(@"mm\:ss");
            aprTests = UI_test_player_TD.DB.TestCase_DAO.GetCountApprovedTests();
            repTests = UI_test_player_TD.DB.TestCase_DAO.GetCountErrorTests();
            pendTests = UI_test_player_TD.DB.TestCase_DAO.GetCountNotExecutedTests();

        }
    }
}