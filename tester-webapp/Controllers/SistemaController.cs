using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_test_player_TD.DB;
using UI_test_player_TD.Model;

namespace tester_webapp.Controllers
{
    public class SistemaController : Controller
    {
        // GET: Sistema
        public ActionResult Index()
        {
            List<Sistema> sistemas = Sistema_DAO.getAllSistemas().ToList();
            return View(sistemas);

        }
    }
}