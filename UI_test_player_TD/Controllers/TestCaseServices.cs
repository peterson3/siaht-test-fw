//using System;
//using System.Collections.Generic;
using System.Collections.ObjectModel;
using UI_test_player_TD.Model;
//using System.Data;
//using System.Data.SQLite;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using TopDown_QA_FrameWork;
//using TopDown_QA_FrameWork.Geradores;
//using UI_test_player_TD.DB;
//using UI_test_player_TD.Model;

namespace UI_test_player_TD.Controllers
{
    public class TestCaseServices
    {

        public static Screen getTela(int Id, Sistema sistema)
        {
            ObservableCollection<Screen> telas = Tela_DAO.getAllTelas(sistema);
            Screen found = null;
            foreach (Screen tela in telas)
            {
                if (tela.Id == Id)
                {
                    found = tela;
                }
            }
            return found;
        }

    }
}
