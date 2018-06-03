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
        private Mat[] channels;
        public UserControlPicBox()
        {
            InitializeComponent();
        }
        
        public Mat SrcImg
        {
            get
            {
                return panAndZoomPictureBox1.Img;
            }
            set
            {
                statusStrip1.Items.Clear();
                Mat img = new Mat(value.Rows, value.Cols, MatType.CV_8UC(value.Channels()));
                value.AssignTo(img, MatType.CV_8UC1);
                panAndZoomPictureBox1.Img = img;
                panAndZoomPictureBox1.MouseMove += mouseMove;
                channels = img.Split();
                foreach (Mat element in channels)
                {
                    statusStrip1.Items.Add("ch");
                }
            }
        }

     
        private void mouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < channels.Length; ++i)
            {
                statusStrip1.Items[i].Text = channels[i].Get<Byte>(e.Y, e.X).ToString();
            }
        }//mouseMove

    }
}
