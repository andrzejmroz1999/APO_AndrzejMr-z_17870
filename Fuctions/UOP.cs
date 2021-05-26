using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APO_AndrzejMróz_17870.Fuctions
{
    class UOP : IFilter
    {
        private UopDialog uopDialog;
        private Bitmap image;
        public bool hasDialog
        {
            get { return true; }
        }

        public UOP() { }

        public void Convert()
        {
            if (uopDialog.points.Count == 0)
                return;
            List<int> LUT = createLUT(uopDialog.points);

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color c = image.GetPixel(j, i);

                    image.SetPixel(j, i, Color.FromArgb(LUT[c.R], LUT[c.R], LUT[c.R]));
                }
            }
        }

        private List<int> createLUT(List<UopDialog.Point> points)
        {
            UopDialog.Point p = new UopDialog.Point(0, 250);
            List<int> LUT = new List<int>(256);

            float a, b, x1, x2, y1, y2;

            points.Add(new UopDialog.Point(255, 0));

            for (int i = 0; i < points.Count; i++)
            {
                if (p.X != points[i].X)
                {
                    x1 = p.X;
                    x2 = points[i].X;
                    y1 = 255 - p.Y;
                    y2 = 255 - points[i].Y;
                    a = (float)(y1 - y2) / (x1 - x2);
                    b = (float)(y1 * x2 - y2 * x1) / (x2 - x1);
                    for (int j = p.X; j <= points[i].X; j++)
                    {
                        LUT.Add((int)(a * j + b));
                    }
                }
                p = points[i];
            }

            return LUT;
        }

        public void setImage(System.Drawing.Image image)
        {
            this.image = (Bitmap)image;
        }

        public bool showDialog()
        {
            uopDialog = new UopDialog();
            if (uopDialog.ShowDialog() != DialogResult.OK)
                return false;
            return true;
        }
    }

}

