using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpencvSharpUtility.UI;

namespace OpencvSharpUtility.Algorithm
{
    abstract class BaseFinder
    {
        protected Mat gray = new Mat();
        protected Mat outputimg = new Mat();
        
        public Mat OutputImg { get { return outputimg; } }

        public BaseFinder(Mat Src)
        {
            switch (Src.Channels())
            {
                case 1:
                    gray = Src.Clone();
                    Cv2.CvtColor(Src, outputimg, ColorConversionCodes.GRAY2BGR);
                    break;

                case 3:
                    outputimg = Src.Clone();
                    Cv2.CvtColor(Src, gray, ColorConversionCodes.BGR2GRAY);
                    break;

                default:
                    throw new Exception("Source image must 1 or 3 channels");
                    break;
            }
        }
        public abstract object Find();


    }
}
