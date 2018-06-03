using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace OpencvSharpUtility.Algorithm
{
    class FindLines
    {

        private Mat gray = new Mat();

        private Mat outputimg = new Mat();
        public Mat OutputImg { get { return outputimg; } }

        public FindLines(Mat Src)
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

        public void Find()
        {
            // Preprocess
            Mat canny = new Mat();
            Cv2.Canny(gray, canny, 50, 200, 3, false);
            LineSegmentPoint[] segProb = Cv2.HoughLinesP(canny, 1, Math.PI / 180, 50, 50, 10);
            foreach (LineSegmentPoint s in segProb)
            {
                outputimg.Line(s.P1, s.P2, Scalar.Red, 1, LineTypes.AntiAlias, 0);
            }
        }
    }
}
