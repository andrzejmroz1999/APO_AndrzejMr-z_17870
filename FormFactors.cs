using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APO_AndrzejMróz_17870
{
    public partial class FormFactors : Form
    {
        public static List<string> AspectRatio(Bitmap bitmap)
        {
            List<string> AspectList = new List<string>();
           
            var img = bitmap.ToImage<Gray, byte>();
            var imgDst = new Image<Gray, byte>(bitmap.Width, bitmap.Height, new Gray(0));
            Emgu.CV.CvInvoke.Threshold(img, imgDst, 127, 255, thresholdType: Emgu.CV.CvEnum.ThresholdType.Binary);
            Emgu.CV.Util.VectorOfVectorOfPoint cont = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();
            Emgu.CV.CvInvoke.FindContours(imgDst, cont, hier, mode: Emgu.CV.CvEnum.RetrType.List, method: Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            for (int i = 0; i < cont.Size; i++)
            {
                var x = Emgu.CV.CvInvoke.BoundingRectangle(cont[i]);
                double aspectR = (double)x.Width / x.Height;
                AspectList.Add(Convert.ToString(aspectR));
            }
            return AspectList;
        }
        public static List<string> ContoursArea(Bitmap bitmap)
        {
            List<string> ContList = new List<string>();
           
            var img = bitmap.ToImage<Gray, byte>();

            var imgDst = new Image<Gray, byte>(bitmap.Width, bitmap.Height, new Gray(0));
            Emgu.CV.CvInvoke.Threshold(img, imgDst, 127, 255, thresholdType: Emgu.CV.CvEnum.ThresholdType.Binary);
            Emgu.CV.Util.VectorOfVectorOfPoint cont = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();
            Emgu.CV.CvInvoke.FindContours(imgDst, cont, hier, mode: Emgu.CV.CvEnum.RetrType.List, method: Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            for (int i = 0; i < cont.Size; i++)
            {
                ContList.Add(Convert.ToString(Math.Round(Emgu.CV.CvInvoke.ContourArea(cont[i]), 3)));
            }

            return ContList;
        }
        public static List<string> Extend(Bitmap bitmap)
        {
            List<string> ContList = ContoursArea(bitmap);
            List<string> ExtList = new List<string>();
           
            var img = bitmap.ToImage<Gray, byte>();
            var imgDst = new Image<Gray, byte>(bitmap.Width, bitmap.Height, new Gray(0));
            Emgu.CV.CvInvoke.Threshold(img, imgDst, 127, 255, thresholdType: Emgu.CV.CvEnum.ThresholdType.Binary);
            Emgu.CV.Util.VectorOfVectorOfPoint cont = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();
            Emgu.CV.CvInvoke.FindContours(imgDst, cont, hier, mode: Emgu.CV.CvEnum.RetrType.List, method: Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            for (int i = 0; i < cont.Size; i++)
            {
                var x = Emgu.CV.CvInvoke.BoundingRectangle(cont[i]);
                double rectArea = (double)x.Width * x.Height;
                double ext = Convert.ToDouble(ContList[i]) / rectArea;
                ExtList.Add(Convert.ToString(ext));
            }
            return ExtList;
        }
        public static List<string> EquivalentDiameter(Bitmap bitmap)
        {
            List<string> ContList = ContoursArea(bitmap);
            List<string> DiamList = new List<string>();
          
            var img = bitmap.ToImage<Gray, byte>();
            var imgDst = new Image<Gray, byte>(bitmap.Width, bitmap.Height, new Gray(0));
            Emgu.CV.CvInvoke.Threshold(img, imgDst, 127, 255, thresholdType: Emgu.CV.CvEnum.ThresholdType.Binary);
            Emgu.CV.Util.VectorOfVectorOfPoint cont = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();
            Emgu.CV.CvInvoke.FindContours(imgDst, cont, hier, mode: Emgu.CV.CvEnum.RetrType.List, method: Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            for (int i = 0; i < cont.Size; i++)
            {
                var area = Emgu.CV.CvInvoke.ContourArea(cont[i]);
                double diam = Math.Sqrt((4 * area) / Math.PI);
                DiamList.Add(Convert.ToString(diam));
            }
            return DiamList;
        }
        public FormFactors(Bitmap bitmap)
        {
            InitializeComponent();
            long x = 1, y = 1, z = 1;
            var list1 = AspectRatio(bitmap);
            foreach (var item in list1)
            {
                listBox1.Items.Add($"Object {x++}:  " + item);
            }

            var list2 = Extend(bitmap);
            foreach (var item in list2)
            {
                listBox2.Items.Add($"Object {y++}:  " + item);
            }

            var list3 = EquivalentDiameter(bitmap);
            foreach (var item in list3)
            {
                listBox3.Items.Add($"Object {z++}:  " + item);
            }
        }
    }
}
