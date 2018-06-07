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
using OpencvSharpUtility.Algorithm;

namespace OpencvSharpUtility.UI
{
    public partial class UserControlBaseFinder : UserControl
    {
        public UserControlBaseFinder()
        {
            InitializeComponent();
            propertyGrid1.PropertyValueChanged += propertyChange;
        }
        private BaseFinder finderReference = null;
        public BaseFinder FinderReference
        {
            set
            {
                finderReference = value;
                linkImage();
            }
            get { return finderReference; }
        }

        void propertyChange(object sender, EventArgs e)
        {
            finderReference.Find();
            linkImage();
        }

        void linkImage()
        {
            userControlPicBox1.SrcImg = finderReference.OutputImg;
            propertyGrid1.SelectedObject = finderReference;
        }

    }
}
