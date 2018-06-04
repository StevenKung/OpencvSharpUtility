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
    public abstract class BaseFinder
    {
        protected Mat gray = new Mat();
        protected Mat outputimg = new Mat();

        public Mat OutputImg { get { return outputimg; } }
        private Mat sourceImg = null;
        public BaseFinder(Mat Src)
        {
            sourceImg = Src;
        }

        protected virtual void initialize()
        {
            switch (sourceImg.Channels())
            {
                case 1:
                    gray = sourceImg.Clone();
                    Cv2.CvtColor(sourceImg, outputimg, ColorConversionCodes.GRAY2BGR);
                    break;

                case 3:
                    outputimg = sourceImg.Clone();
                    Cv2.CvtColor(sourceImg, gray, ColorConversionCodes.BGR2GRAY);
                    break;

                default:
                    throw new Exception("Source image must 1 or 3 channels");
                    break;
            }
        }

        public abstract object Find();


    }
}
