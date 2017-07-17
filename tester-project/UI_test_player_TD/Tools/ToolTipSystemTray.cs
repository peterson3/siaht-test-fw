using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_test_player_TD.Tools
{
    static class ToolTipSystemTray
    {
        public static  void showAprBalloon(string title, string body)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = new Icon("Resources/ok.ico");

            if (title != null)
            {
                notifyIcon.BalloonTipTitle = title;
            }

            if (body != null)
            {
                notifyIcon.BalloonTipText = body;
            }

            notifyIcon.ShowBalloonTip(100000);
            notifyIcon.Dispose();
        }

        public static void showErrorBalloon(string title, string body)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Visible = true;
            notifyIcon.Icon = new Icon("Resources/erro.ico");

            if (title != null)
            {
                notifyIcon.BalloonTipTitle = title;
            }

            if (body != null)
            {
                notifyIcon.BalloonTipText = body;
            }

            notifyIcon.ShowBalloonTip(100000);
            notifyIcon.Dispose();
        }

    }
}
