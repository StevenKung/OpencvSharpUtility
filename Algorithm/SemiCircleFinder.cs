using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.ComponentModel;
using OpencvSharpUtility.UI;
using System.Drawing.Design;

namespace OpencvSharpUtility.Algorithm
{
    class SemiCircleFinder :BaseFinder
    {


        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("canny threshold")]
        public double cannyThreshold { get; set; } = 100.0;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("I偵測解析度倒數比例，假設dp=1，偵測圖和輸入圖尺寸相同，假設dp=2，偵測圖長和寬皆為輸入圖的一半")]
        public double dp { get; set; } = 1.0;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("圓彼此間的最短距離，太小的話可能會把鄰近的幾個圓視為一個，太大的話可能會錯過某些圓")]
        public double minDist { get; set; } = 45.0;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("計數閾值，超過此值的圓才會存入circles")]
        public double param2 { get; set; } = 35.0;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("最小的圓半徑")]
        public int minRadius { get; set; } = 0;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("最大的圓半徑")]
        public int maxRadius { get; set; } = 0;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("minInlierDist")]
        public double minInlierDist { get; set; } = 2.0;

        [Editor(typeof(UpDownValueEditor), typeof(UITypeEditor))]
        [Description("maxInlierDist")]
        public double maxInlierDistRatio { get; set; } = 25.0;

        [Editor(typeof(MatTypeEditor), typeof(UITypeEditor))]
        public Mat canny { get; set; } = new Mat();
        public SemiCircleFinder(Mat Src) : base(Src) { }
        public override object Find()
        {
            initialize();
            Cv2.Canny(gray, canny, cannyThreshold, cannyThreshold/2);
           // Cv2.ImShow("canny", canny.GreaterThan(0.0)); //mat > 0
            CircleSegment[] circles;
            //for more info https://docs.opencv.org/2.4/modules/imgproc/doc/feature_detection.html?highlight=houghcircles
            circles = Cv2.HoughCircles(gray, HoughMethods.Gradient, dp, minDist, cannyThreshold, param2, minRadius, maxRadius);

            // draw the circle detected
            foreach (CircleSegment element in circles)
            {
                Point center = new Point(Math.Round(element.Center.X), Math.Round(element.Center.Y));
                Cv2.Circle(outputimg, center, 3, new Scalar(0, 255, 255), -1);
                Cv2.Circle(outputimg, center, (int)element.Radius, new Scalar(0, 0, 255), 1);
            }

            //compute distance transorm
            Mat dt = new Mat();
            Mat wh = new Mat(canny.Rows, canny.Cols, MatType.CV_8UC1, new Scalar(255));
            Cv2.DistanceTransform(wh - canny.GreaterThan(0.0), dt, DistanceTypes.L2, DistanceMaskSize.Mask3);

            //test for semi-circle
            foreach (CircleSegment element in circles)
            {
                //test inlier percentage
                //sample the circle and check for distance to next edge
                uint counter = 0;
                uint inlier = 0;

                //maximal distance of inlier might depend on the size of the circle
                //the more close edge the lower distance transform value
                double maxInlierDist = element.Radius / maxInlierDistRatio;
                if (maxInlierDist < minInlierDist) maxInlierDist = minInlierDist;

                //TODO maybe parameter incrementation might depend on circle size
                for (float t = 0; t < 2 * Math.PI; t += 0.1f)
                {
                    counter++;
                    int cx = (int)(element.Radius * Math.Cos(t) + element.Center.X);
                    int cy = (int)(element.Radius * Math.Sin(t) + element.Center.Y);
                    if (cx > dt.Cols || cy >= dt.Rows) continue;
                    if (dt.Get<float>(cy, cx) < maxInlierDist) //carefor the image coordinate
                    {
                        inlier++;
                        Cv2.Circle(outputimg, cx, cy, 3, new Scalar(255, 255, 255));
                    }
                    else
                    {
                        Cv2.Circle(outputimg, cx, cy, 3, new Scalar(255, 0, 0));
                    }
                }
                Console.WriteLine("{0}% of a circle with radius {1} detected",
                 100 * inlier / counter, element.Radius);
            }//foreach circle
            return circles;
        }


    }
}
