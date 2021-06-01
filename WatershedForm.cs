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
       
        public int ObjCounter;
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
                label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                label1.Visible = true;
            }
            else
            {
                pictureBox1.Image = Wododział.Watershed(bitmap, imgPath, this, factor, int.Parse(textBox4.Text));
                label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
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
                textBox4.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                checkBox1.Checked = true;
                label4.Enabled = false;
                textBox4.Enabled = false;
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

        

        private void button2_Click(object sender, EventArgs e)
        {
            float factor;
            Single.TryParse(textBox2.Text, out factor);

            if (checkBox1.Checked)
            {

                result = Wododział.Watershed(bitmap, imgPath, this, factor);
                label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                label1.Visible = true;
            }
            else
            {
                result = Wododział.Watershed(bitmap, imgPath, this, factor, int.Parse(textBox4.Text));
                label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                label1.Visible = true;
            }
            button2.DialogResult = DialogResult.OK;
            // Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Bitmap bmp = new Bitmap(bitmap.Width, bitmap.Height);



            float factor;
            Single.TryParse(textBox2.Text, out factor);

            int prog = int.Parse(textBox3.Text);
            int newValuePixel = 0, newValuePicture = 0;

            int[] progowanieHist = new int[2];

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    Color c = bitmap.GetPixel(x, y);

                    if (c.R <= prog)
                    {
                        newValuePixel = 0;
                    }
                    else
                    {
                        newValuePixel = 1;
                    }

                    progowanieHist[newValuePixel] += 1;

                    if (newValuePixel == 1) { newValuePicture = 255; }
                    else { newValuePicture = 0; }

                    Color newColor = Color.FromArgb(255, newValuePicture, newValuePicture, newValuePicture);
                    bmp.SetPixel(x, y, newColor);
                }
            }

            if (checkBox1.Checked)
            {

                pictureBox1.Image = Wododział.Watershed(bitmap, imgPath, this, factor, bmp);
                label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                label1.Visible = true;
                
            }
            else
            {
                pictureBox1.Image = Wododział.Watershed(bitmap, imgPath, this, factor, bmp, int.Parse(textBox4.Text));
                label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                label1.Visible = true;
                

            }



        }


    }
}



