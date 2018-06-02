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
using OpenCvSharp.UserInterface;

namespace OpencvSharpUtility
{
    public partial class PanAndZoomPictureBox : PictureBoxIpl
    {
        public Mat Img
        {
            set
            {
                this.ImageIpl = value;
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
