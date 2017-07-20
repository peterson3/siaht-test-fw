using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_test_player_TD;
using UI_test_player_TD.Model;
using UI_test_player_TD.DB;
using tester_webapp.ViewModel;

namespace tester_webapp.Controllers
{
    public class TestCaseController : Controller
    {
        // GET: TestCase/Random
        public ViewResult Random()
        {
            Random randomizer = new Random();
            List<Sistema> sistemas = Sistema_DAO.getAllSistemas().ToList();
            Sistema randomSistema = sistemas[randomizer.Next(0, sistemas.Count)];
            List<TestCase> testCases = TestCase_DAO.GetAllFromSistema(randomSistema);
            var testCase = testCases[randomizer.Next(0, testCases.Count)];
            return View(testCase);
        }

        // GET: TestCase
        public ActionResult Index()
        {
            //Recuperar Todas os TestCase possíveis
            List<Sistema> sistemas = Sistema_DAO.getAllSistemas().ToList();
            List<TestCase> allTestCases = new List<TestCase>();
            foreach (Sistema sis in sistemas)
            {
                allTestCases.AddRange(TestCase_DAO.GetAllFromSistema(sis));
            }

            return View(allTestCases);
        }

        // GET: TestCase/Editar/{id}
        public ActionResult Editar (int id)
        {

            return Content("id= " + id);
        }

        public ActionResult Novo()
        {
            CadastroCasoTesteViewModel viewModel = new CadastroCasoTesteViewModel () {
            sistemas = Sistema_DAO.getAllSistemas().AsEnumerable<Sistema>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Criar(CadastroCasoTesteViewModel viewModel)
        {
            return View();
        }
    }
}