using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpencvSharpUtility.Algorithm;
using OpencvSharpUtility.UI;

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


            Mat circle = Cv2.ImRead(@"magnifier.png", ImreadModes.AnyDepth | ImreadModes.AnyColor);
            //Mat circle = Cv2.ImRead(@"circle.png", ImreadModes.GrayScale);
            BaseFinder semiCircle = new SemiCircleFinder(circle);
            semiCircle.Find();
            picbox.SrcImg = semiCircle.OutputImg;
            f.ShowDialog();


            //Mat magnifier = Cv2.ImRead(@"magnifier.png", ImreadModes.AnyColor | ImreadModes.AnyDepth);
            //BaseFinder findline = new LineFinder(magnifier);
            //findline.Find();
            //picbox.SrcImg = findline.OutputImg;
            //f.ShowDialog();
            f.Controls.Clear();


            UserControlBaseFinder controlfinder = new UserControlBaseFinder
            {
                Dock = DockStyle.Fill,
            };

            controlfinder.FinderReference = semiCircle;
            f.Controls.Add(controlfinder);
            f.ShowDialog();



        }
    }
}
