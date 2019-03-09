using ScreenShotDemo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.CrossCutting
{
    public static class PrintUtils
    {
        public static int qtdPrints = 0;

        public static string takeSS()
        {
            
            ScreenCapture sc = new ScreenCapture();
            // capture entire screen, and save it to a file
            Image img = sc.CaptureScreen();
            // display image in a Picture control named imageDisplay
            string printstring = @"printutils\" + PrintUtils.qtdPrints + ".jpeg";
            img.Save( printstring, ImageFormat.Jpeg);
            PrintUtils.qtdPrints++;
            return printstring;
        }
    }
}
