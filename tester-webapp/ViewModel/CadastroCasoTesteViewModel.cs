using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_test_player_TD.Model;

namespace tester_webapp.ViewModel
{
    public class CadastroCasoTesteViewModel
    {
        public TestCase testCase { get; set; }
        public IEnumerable<Sistema> sistemas { get; set; }
    }
}