using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_test_player_TD.DB;
using UI_test_player_TD.Model;

namespace Tester.Application
{
    public class ScreenApplicationService
    {
        private readonly ScreenRepository screenRepository;

        public ScreenApplicationService()
        {
            screenRepository = new ScreenRepository();
        }

        public IEnumerable<Screen> GetAllScreens()
        {
            screenRepository.GetAllScreens();
        } 
    }
}
