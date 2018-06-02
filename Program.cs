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
            Mat src = Cv2.ImRead(@"lenna.png", ImreadModes.AnyDepth | ImreadModes.AnyColor);

            UserControlPicBox picbox = new UserControlPicBox(src);
            picbox.Dock = DockStyle.Fill;
            Form f = new Form
            {
                AutoSize = true,
            };
            f.Controls.Add(picbox);
            f.ShowDialog();
            f.Controls.Clear();
            Window.Histogram(src, "ttt");


            Mat circle = Cv2.ImRead(@"circle.png", ImreadModes.GrayScale);
            FindSemiCircle semiCircle = new FindSemiCircle(circle);
            semiCircle.Find();
            UserControlPicBox picbox2 = new UserControlPicBox(semiCircle.OutputImg);
            picbox.Dock = DockStyle.Fill;
           
            f.Controls.Add(picbox2);
            f.ShowDialog();
        }
    }
}
