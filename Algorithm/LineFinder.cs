using System;
using System.ComponentModel;
using System.Drawing.Design;
using OpenCvSharp;
using OpencvSharpUtility.UI;

namespace OpencvSharpUtility.Algorithm
{
    class LineFinder : BaseFinder
    {
        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("canny threshold")]
        public double cannyThreshold { get; set; } = 200.0;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("距離解析度，越小表示定位要求越準確，但也較易造成應該是同條線的點判為不同線")]
        public double rho { get; set; } = 1;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("角度解析度，越小表示角度要求越準確，但也較易造成應該是同條線的點判為不同線")]
        public double theta { get; set; } = Math.PI / 180;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("累積個數閾值，超過此值的線才會存在lines這個容器內")]
        public int threshod { get; set; } = 50;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("Minimum length of line. Line segments shorter than this are rejected.")]
        public int minLineLength { get; set; } = 50;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("Maximum allowed gap between line segments to treat them as single line.")]
        public int maxLineGap { get; set; } = 10;

        [Editor(typeof(MatTypeEditor), typeof(UITypeEditor))]
        public Mat canny { get; set; } = new Mat();

        public LineFinder(Mat Src) : base(Src) { }

        public override object Find()
        {
            initialize();
            // Preprocess
            Cv2.Canny(gray, canny, cannyThreshold, cannyThreshold/2, 3, false);
            LineSegmentPoint[] segProb = Cv2.HoughLinesP(canny, rho,theta, threshod, minLineLength, maxLineGap);
            foreach (LineSegmentPoint s in segProb)
            {
                outputimg.Line(s.P1, s.P2, Scalar.Red, 1, LineTypes.AntiAlias, 0);
            }
            return segProb;
        }
    }
}
