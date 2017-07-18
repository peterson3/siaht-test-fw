using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tester_webapp.Controllers
{
    public class TelaController : Controller
    {
        // GET: Tela
        public ActionResult Index()
        {
            return View();
        }
    }
}