using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_test_player_TD.Model;
using UI_test_player_TD.Views;
using System.IO;

namespace UI_test_player_TD.Controllers
{
    public class MainController
    {
        TestCaseView testCaseView;
        TestSuiteView testSuiteView;
        SettingsView settingsView;
        MainWindow mainWindow;
        LogView logView;
        AcoesView acoesView;
        SysView sistemasView;
        TelasView telasView;

        
        public MainController(MainWindow window)
        {
            mainWindow = window;
        }

        public void OpenBrowser()
        {
            //TopDown_QA_FrameWork.Browser.Initialize("http://topdown.com.br/");
        }

        public TestCaseView getTestCaseView()
        {
            if (testCaseView == null)
            {
                testCaseView = new TestCaseView(mainWindow);
            }
            testCaseView.refresh();
            return testCaseView;
        }

        public SettingsView getSettingsView()
        {
            if (settingsView == null)
            {
                settingsView = new SettingsView(mainWindow);
            }

            return settingsView;
        }

        public TestSuiteView getTestSuiteView()
        {
            if (testSuiteView == null)
            {
                testSuiteView = new TestSuiteView(mainWindow);
            }
            return testSuiteView;
        }

        public LogView getLogView()
        {
            if (logView == null)
            {
                logView = new LogView(mainWindow);
            }
            return logView;
        }

        public AcoesView getAcoesView()
        {
            if (acoesView == null)
            {
                acoesView = new AcoesView(mainWindow);
            }
            acoesView.refresh();
            return acoesView;
        }

        public SysView getSistemasView()
        {
            if (sistemasView == null)
            {
                sistemasView = new SysView(mainWindow);
            }
            sistemasView.refresh();

            return sistemasView;
        }

        public TelasView getTelasView()
        {
            if (telasView == null)
            {
                telasView = new TelasView(mainWindow);
            }
            telasView.refresh();
            return telasView;
        }

        public void WriteInLogView(string text)
        {
            logView.AppendText(text);
        }



    }
}
