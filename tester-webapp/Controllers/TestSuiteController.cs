using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_test_player_TD.Model;
using UI_test_player_TD;
using UI_test_player_TD.DB;

namespace tester_webapp.Controllers
{
    public class TestSuiteController : Controller
    {
        // GET: TestSuite
        public ActionResult Index()
        {
            //Recuperar Todas os TestSuite possíveis
            List<Sistema> sistemas = Sistema_DAO.getAllSistemas().ToList();
            List<TestSuite> allTestSuites = new List<TestSuite>();
            foreach (Sistema sis in sistemas)
            {
                allTestSuites.AddRange(TestSuite_DAO.getTestSuitesFromSistema(sis));
            }

            return View(allTestSuites);
        }
    }
}