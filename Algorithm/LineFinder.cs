using System;
using System.ComponentModel;
using System.Drawing.Design;
using OpenCvSharp;
using OpencvSharpUtility.UI;

namespace OpencvSharpUtility.Algorithm
{
    class LineFinder : BaseFinder
    {
        [Editor(typeof(MatTypeEditor), typeof(UITypeEditor))]
        public Mat canny { get; set; } = new Mat();
        public LineFinder(Mat Src) : base(Src) { }

        public override object Find()
        {
            // Preprocess
            Cv2.Canny(gray, canny, 50, 200, 3, false);
            LineSegmentPoint[] segProb = Cv2.HoughLinesP(canny, 1, Math.PI / 180, 50, 50, 10);
            foreach (LineSegmentPoint s in segProb)
            {
                outputimg.Line(s.P1, s.P2, Scalar.Red, 1, LineTypes.AntiAlias, 0);
            }
            return segProb;
        }
    }
}
