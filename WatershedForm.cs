using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using APO_AndrzejMróz_17870.Fuctions;


namespace APO_AndrzejMróz_17870
{
    public partial class WatershedForm : Form
    {
        public Bitmap result;
        Bitmap bitmap;
        public int objNum;
        public string imgPath;
        public WatershedForm(Bitmap bitmap, String path)
        {
            InitializeComponent();
            pictureBox1.Image = bitmap;
            pictureBox1.Width = bitmap.Width;
            pictureBox1.Height = bitmap.Height;
            this.bitmap = bitmap;
            imgPath = path;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            float factor;
            Single.TryParse(textBox2.Text, out factor);

            if (checkBox1.Checked)
            {
                pictureBox1.Image = Wododział.Watershed(bitmap, imgPath, this, factor);
                label1.Text = "Znaleziono " + objNum.ToString() + " objektów";
                label1.Visible = true;
            }
            else
            {
                pictureBox1.Image = Wododział.Watershed(bitmap, imgPath, this, factor, trackBar1.Value);
                label1.Text = "Znaleziono " + objNum.ToString() + " objektów";
                label1.Visible = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                label4.Enabled = true;
                trackBar1.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                checkBox1.Checked = true;
                label4.Enabled = false;
                trackBar1.Enabled = false;
                textBox1.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            float number;

            bool isParsable = Single.TryParse(textBox2.Text, out number);

            if (!isParsable || number < 0 || number > 1)
            {
                button1.Enabled = false;
            }
            else button1.Enabled = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBar1.Value % 2 == 0)
            {
                trackBar1.Value += 1;
            }
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float factor;
            Single.TryParse(textBox2.Text, out factor);

            if (checkBox1.Checked)
            {

                result = Wododział.Watershed(bitmap, imgPath, this, factor);
                label1.Text = "Znaleziono " + objNum.ToString() + " objektów";
                label1.Visible = true;
            }
            else
            {
                result = Wododział.Watershed(bitmap, imgPath, this, factor, trackBar1.Value);
                label1.Text = "Znaleziono " + objNum.ToString() + " objektów";
                label1.Visible = true;
            }
            button2.DialogResult = DialogResult.OK;
           // Close();
        }

        
    }
}


