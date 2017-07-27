using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_test_player_TD.DB;
using UI_test_player_TD.Model;

namespace tester_webapp.Controllers
{
    public class TelaController : Controller
    {
        // GET: Tela
        public ActionResult Index()
        {
            List<Sistema> sistemas = Sistema_DAO.getAllSistemas().ToList();
            return View(sistemas);
        
        
        }

        public ActionResult TelasSistema(int sistemaId)
        {
            List<Sistema> sistemas = Sistema_DAO.getAllSistemas().ToList();
            List<Tela> telas = Tela_DAO.getAllTelas(sistemas.First(x => x.Id == sistemaId)).ToList();

            return View(telas);
        }
    }
}