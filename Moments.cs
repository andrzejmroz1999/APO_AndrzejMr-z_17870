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
    public partial class Moments : Form
    {
        public static List<string> moments(Bitmap bitmap)
        {
          //  Bitmap bitmap = (Bitmap)picture.Image;
            var img = bitmap.ToImage<Gray, byte>();
            var m = CvInvoke.Moments(img);
            List<string> moments = new List<string>();
            moments.Add("m00: " + m.M00);
            moments.Add("m01: " + m.M01);
            moments.Add("m02: " + m.M02);
            moments.Add("m03: " + m.M03);
            moments.Add("m10: " + m.M10);
            moments.Add("m11: " + m.M11);
            moments.Add("m12: " + m.M12);
            moments.Add("m20: " + m.M20);
            moments.Add("m21: " + m.M21);
            moments.Add("m30: " + m.M30);
            moments.Add("mu02: " + m.Mu02);
            moments.Add("mu03: " + m.Mu03);
            moments.Add("mu11: " + m.Mu11);
            moments.Add("mu12: " + m.Mu12);
            moments.Add("mu20: " + m.Mu20);
            moments.Add("mu21: " + m.Mu21);
            moments.Add("mu30: " + m.Mu30);
            moments.Add("nu02: " + m.Nu02);
            moments.Add("nu03: " + m.Nu03);
            moments.Add("nu11: " + m.Nu11);
            moments.Add("nu12: " + m.Nu12);
            moments.Add("nu20: " + m.Nu20);
            moments.Add("nu21: " + m.Nu21);
            moments.Add("nu30: " + m.Nu30);
            return moments;
        }
        public Moments(Bitmap bitmap)
        {
            InitializeComponent();
            var list = moments(bitmap);
            foreach (var item in list)
            {
                listBox1.Items.Add(item);
            }
        }
    }
}
