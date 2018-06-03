using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpencvSharpUtility.Algorithm;

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


            UserControlPicBox picbox = new UserControlPicBox();
            picbox.Dock = DockStyle.Fill;
            Form f = new Form
            {
                AutoSize = true,
            };
            f.Controls.Add(picbox);

            Mat src = Cv2.ImRead(@"lenna.png", ImreadModes.AnyDepth | ImreadModes.AnyColor);
            picbox.SrcImg = src;
            f.ShowDialog();
            Window.Histogram(src, "ttt");


            Mat circle = Cv2.ImRead(@"circle.png", ImreadModes.AnyDepth | ImreadModes.AnyColor);
            //Mat circle = Cv2.ImRead(@"circle.png", ImreadModes.GrayScale);
            FindSemiCircle semiCircle = new FindSemiCircle(circle);
            semiCircle.Find();
            picbox.SrcImg = circle;
            f.ShowDialog();


            Mat magnifier = Cv2.ImRead(@"magnifier.png", ImreadModes.AnyColor | ImreadModes.AnyDepth);
            FindLines findline = new FindLines(magnifier);
            findline.Find();
            picbox.SrcImg = findline.OutputImg;
            f.ShowDialog();
        }
    }
}
