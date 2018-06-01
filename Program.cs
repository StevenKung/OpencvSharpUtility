using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace OpencvSharpUtility
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Mat src = Cv2.ImRead(@"lenna.png", ImreadModes.AnyDepth | ImreadModes.AnyColor);
            Window.Histogram(src, "ttt");
        }
    }
}
