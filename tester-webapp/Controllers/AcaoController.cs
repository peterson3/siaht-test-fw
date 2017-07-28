using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tester_webapp.Controllers
{
    public class AcaoController : Controller
    {
        // GET: Acao
        public ActionResult Index()
        {
            return Content("Index");
        }


        public ActionResult SelecionaTela(int? sistemaId, int? telaId, int? acaoId)
        {
            if (!sistemaId.HasValue)
            {
                return Content("Index");
            }
            else
            {
                if (!telaId.HasValue)
                {
                    return Content("Sistema Id=" + sistemaId.Value);
                }
                else
                {
                    //Sistema Id and Telaid foram passadas
                    if (!acaoId.HasValue)
                    {
                        return Content("SistemaId=" + sistemaId.Value + "TelaId=" + telaId.Value);
                    }
                    else
                    {
                        ///Foram passados os 3 valores
                        return Content("SistemaId=" + sistemaId.Value + "TelaId=" + telaId.Value + "AcaoId=" + acaoId.Value);
                    }
                }
            }
        }
    }
}