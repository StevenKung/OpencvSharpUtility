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
using OpenCvSharp.Extensions;

namespace OpencvSharpUtility
{
    public partial class PanAndZoomPictureBox : PictureBox
    {
        public Mat Img
        {
            set
            {
                this.Image = value.ToBitmap();
            }
        }

        public PanAndZoomPictureBox()
        {
            InitializeComponent();
            this.MouseMove += OnMouseMove;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {

        }

    }
}
