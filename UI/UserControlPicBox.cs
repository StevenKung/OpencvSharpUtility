using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace OpencvSharpUtility
{
    public partial class UserControlPicBox : UserControl
    {
        Mat[] channels;
        public UserControlPicBox(Mat Src)
        {
            Mat img = new Mat(Src.Rows, Src.Cols, MatType.CV_8UC(Src.Channels()));
            Src.AssignTo(img, MatType.CV_8UC1);
            InitializeComponent();
            panAndZoomPictureBox1.Img = img;
            panAndZoomPictureBox1.MouseMove += mouseMove;
            channels = img.Split();
            for (int i = 0; i < channels.Length; ++i)
            {
                statusStrip1.Items.Add("ch");
            }
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < channels.Length; ++i)
            {
                statusStrip1.Items[i].Text = channels[i].Get<Byte>(e.Y, e.X).ToString();
            }

        }
    }
}
