using OpenCvSharp;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace OpencvSharpUtility
{
    public class Window
    {
        public static void Histogram(Mat img, string name)
        {
            FormHist form = new FormHist();

            //Note: OpenCV uses BGR color order
            Mat[] channels = img.Split();

           for (int i=0; i<channels.Length;i++)
            {
                Chart chart = new Chart
                {
                    Dock = DockStyle.Fill,
                    Height = 300,
                };
                chart.Titles.Add("channel" + i.ToString());
                chart.Series.Add("series1");
                chart.Series[0].ChartType = SeriesChartType.Column;
                chart.ChartAreas.Add(new ChartArea());
                DataPointCollection points = chart.Series[0].Points;



                // Calculate histogram
                Mat hist = new Mat();
                int[] hdims = { 256 }; // Histogram size for each dimension
                Rangef[] ranges = { new Rangef(0, 256), }; // min/max 
                Cv2.CalcHist(
                    new Mat[] { channels[i] },
                    new int[] { 0 },
                    null,
                    hist,
                    1,
                    hdims,
                    ranges);


                for (int j = 0; j < hdims[0]; ++j)
                {
                    points.AddXY(j, hist.Get<float>(j));
                }

                form.tableLayoutPanel1.Controls.Add(chart, -1,-1);
            }
            form.ShowDialog();
        }




    }
}
