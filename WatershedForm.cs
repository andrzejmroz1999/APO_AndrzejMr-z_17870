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
using System.Threading;

namespace APO_AndrzejMróz_17870
{
    public partial class WatershedForm : Form
    {
        public Bitmap result;
        Bitmap bitmap;
        Bitmap pom;
        Bitmap[] bitmaps;
       
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

            if (checkBox3.Checked == false)
            {
                if (checkBox1.Checked)
                {
                    pictureBox1.Image = Wododział.Watershed((Bitmap)bitmap.Clone(), imgPath, this, factor);
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;
                }
                else if (checkBox2.Checked)
                {
                    pictureBox1.Image = Wododział.Watershed((Bitmap)bitmap.Clone(), imgPath, this, factor, trackBar1.Value);
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;
                }
            }
            if (checkBox3.Checked == true)
            {
                Bitmap bmp = new Bitmap(bitmap.Width, bitmap.Height);

                Bitmap pom2 = new Bitmap(bitmap.Width, bitmap.Height);
                pom2 = (Bitmap)bitmap.Clone();


                Single.TryParse(textBox2.Text, out factor);

                int prog = int.Parse(textBox3.Text);
                int newValuePixel = 0, newValuePicture = 0;

                int[] progowanieHist = new int[2];

                for (int x = 0; x < pom2.Width; ++x)
                {
                    for (int y = 0; y < pom2.Height; ++y)
                    {
                        Color c = pom2.GetPixel(x, y);

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

                    pictureBox1.Image = Wododział.Watershed((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone());
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;

                }
                else
                {
                    pictureBox1.Image = Wododział.Watershed((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), trackBar1.Value);
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;


                }
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
                textBox1.Visible = true;
                trackBar1.Visible = true;
                label4.Visible = false;
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

            if (checkBox3.Checked == false)
            {
                if (checkBox1.Checked)
                {
                    pictureBox1.Image = Wododział.Watershed(bitmap, imgPath, this, factor);
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;
                }
                else if (checkBox2.Checked)
                {
                    pictureBox1.Image = Wododział.Watershed(bitmap, imgPath, this, factor, trackBar1.Value);
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;
                }
            }
            if (checkBox3.Checked == true)
            {
                Bitmap bmp = new Bitmap(bitmap.Width, bitmap.Height);




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
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;

                }
                else
                {
                    pictureBox1.Image = Wododział.Watershed(bitmap, imgPath, this, factor, bmp, trackBar1.Value);
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;


                }
                button2.DialogResult = DialogResult.OK;
                result = (Bitmap)pictureBox1.Image;
                 Close();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            label2.Visible = true;
            textBox3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float factor;
            Single.TryParse(textBox2.Text, out factor);

            if (checkBox3.Checked == false)
            {
                if (checkBox1.Checked)
                {
                    pictureBox1.Image = Wododział.Watershed((Bitmap)bitmap.Clone(), imgPath, this, factor);
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;
                }
                else if (checkBox2.Checked)
                {
                    pictureBox1.Image = Wododział.Watershed((Bitmap)bitmap.Clone(), imgPath, this, factor, trackBar1.Value);
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;
                }
            }
            if (checkBox3.Checked == true)
            {
                Bitmap bmp = new Bitmap(bitmap.Width, bitmap.Height);
                Bitmap pom = (Bitmap)bitmap.Clone();



                Single.TryParse(textBox2.Text, out factor);

                int prog = int.Parse(textBox3.Text);
                int newValuePixel = 0, newValuePicture = 0;

                int[] progowanieHist = new int[2];

                for (int x = 0; x < pom.Width; ++x)
                {
                    for (int y = 0; y < pom.Height; ++y)
                    {
                        Color c = pom.GetPixel(x, y);

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

                    pictureBox1.Image = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 1);
                    pictureBox1.Refresh();
                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;

                }
                else
                {
                    bitmaps = new Bitmap[12];
                    pictureBox1.Image = bitmaps[0] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 1, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 0;                    
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[1] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 2, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 1;
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[2] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 3, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 2;
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[3] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 4, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 3;
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[4] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 5, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 4;
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[5] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 6, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 5;
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[6] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 7, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 6;
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[7] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 8, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 7;
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[8] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 9, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 8;
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[9] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 10, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 9;
                    Thread.Sleep(5000);
                    pictureBox1.Image = bitmaps[10] = Wododział.WatershedVisualization((Bitmap)bitmap.Clone(), imgPath, this, factor, (Bitmap)bmp.Clone(), 11, trackBar1.Value);
                    pictureBox1.Refresh();
                    comboBox1.SelectedIndex = 10;
                   

                    label1.Text = "Znaleziono " + ObjCounter.ToString() + " objektów";
                    label1.Visible = true;


                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                pictureBox1.Image = bitmaps[0];
            }
            if (comboBox1.SelectedIndex == 1)
            {
                pictureBox1.Image = bitmaps[1];
            }
            if (comboBox1.SelectedIndex == 2)
            {
                pictureBox1.Image = bitmaps[2];
            }
            if (comboBox1.SelectedIndex == 3)
            {
                pictureBox1.Image = bitmaps[3];
            }
            if (comboBox1.SelectedIndex == 3)
            {
                pictureBox1.Image = bitmaps[3];
            }
            if (comboBox1.SelectedIndex == 4)
            {
                pictureBox1.Image = bitmaps[4];
            }
            if (comboBox1.SelectedIndex == 5)
            {
                pictureBox1.Image = bitmaps[5];
            }
            if (comboBox1.SelectedIndex == 6)
            {
                pictureBox1.Image = bitmaps[6];
            }
            if (comboBox1.SelectedIndex == 7)
            {
                pictureBox1.Image = bitmaps[7];
            }
            if (comboBox1.SelectedIndex == 8)
            {
                pictureBox1.Image = bitmaps[8];
            }
            if (comboBox1.SelectedIndex == 9)
            {
                pictureBox1.Image = bitmaps[9];
            }
            if (comboBox1.SelectedIndex == 10)
            {
                pictureBox1.Image = bitmaps[10];
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = trackBar2.Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            int prog = trackBar2.Value;
            int newValuePixel = 0, newValuePicture = 0;
            pom = (Bitmap)bitmap.Clone();

            int[] progowanieHist = new int[2];

            for (int x = 0; x < pom.Width; ++x)
            {
                for (int y = 0; y < pom.Height; ++y)
                {
                    Color c = pom.GetPixel(x, y);

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
                    pom.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Image = pom;
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
        }
    }
}



