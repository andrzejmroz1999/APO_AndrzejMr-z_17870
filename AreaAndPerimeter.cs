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
    public partial class AreaAndPerimeter : Form
    {
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
        public static List<string> ContoursCircuit(Bitmap bitmap)
        {
            List<string> CircuitsList = new List<string>();
          
            var img = bitmap.ToImage<Gray, byte>();
            var imgDst = new Image<Gray, byte>(bitmap.Width, bitmap.Height, new Gray(0));
            Emgu.CV.CvInvoke.Threshold(img, imgDst, 127, 255, thresholdType: Emgu.CV.CvEnum.ThresholdType.Binary);
            Emgu.CV.Util.VectorOfVectorOfPoint cont = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();
            Emgu.CV.CvInvoke.FindContours(imgDst, cont, hier, mode: Emgu.CV.CvEnum.RetrType.List, method: Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            for (int i = 0; i < cont.Size; i++)
            {
                CircuitsList.Add(Convert.ToString(Math.Round(Emgu.CV.CvInvoke.ArcLength(cont[i], true), 3)));
            }

            return CircuitsList;
        }
        public AreaAndPerimeter(Bitmap bitmap)
        {
            InitializeComponent();
            long x = 1, y = 1;
            var list1 = ContoursArea(bitmap);
            foreach (var item in list1)
            {
                listBox1.Items.Add($"Object {x++}:  " + item);
            }

            var list2 = ContoursCircuit(bitmap);
            foreach (var item in list2)
            {
                listBox2.Items.Add($"Object {y++}:  " + item);
            }
        }
    }
}
