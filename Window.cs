using OpenCvSharp;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace OpencvSharpUtility
{
    public class Window
    {
        public static void Histogram(Mat img, string name)
        {
            TableLayoutPanel layoutPanel = new TableLayoutPanel
            {
                GrowStyle = TableLayoutPanelGrowStyle.AddColumns,
                Dock = DockStyle.Fill,
                AutoSizeMode = AutoSizeMode.GrowOnly,
                ColumnCount = 1,
                RowCount = 1,
            };

            //Note: OpenCV uses BGR color order
            Mat[] channels = img.Split();

            for (int i = 0; i < channels.Length; i++)
            {
                Chart chart = new Chart();
                chart.Dock = DockStyle.Fill;
                chart.Titles.Add("channel" + i.ToString());
                chart.Series.Add("series");
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

                //draw data in chart
                for (int j = 0; j < hdims[0]; ++j)
                {
                    points.AddXY(j, hist.Get<float>(j));
                }

                layoutPanel.ColumnCount = i;
                layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                layoutPanel.Controls.Add(chart, -1, -1);
            }

            Form form = new Form
            {
                Text = name,
                Size = new System.Drawing.Size(300 * channels.Length, 400),
                AutoSizeMode = AutoSizeMode.GrowOnly,
            };
            form.Controls.Add(layoutPanel);
            form.ShowDialog();
        }
    }
}
