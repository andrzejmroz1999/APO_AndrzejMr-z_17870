using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APO_AndrzejMróz_17870
{

    public partial class ImageForm : Form
    {
        private int[] histogram;
        private int[] histR;
        private int[] histG;
        private int[] histB;
        public Bitmap bitmap, rbmp, gbmp, bbmp;
        private MainPage parent;
        bool l = true;
        string H;
        double[,] red, green, blue;


        public ImageForm(MainPage parent)
        {
            this.parent = parent;
            InitializeComponent();
        }
        public ImageForm(ImageForm picture)
        {
            InitializeComponent();

            bitmap = new Bitmap(picture.bitmap);
            pictureBox1.Image = bitmap;

            int height = bitmap.Size.Height;
            int width = bitmap.Size.Width;
            pictureBox1.Width = width * 420 / height;
        }
        public void LoadImage(String path)
        {
            bitmap = CreateImage(new Bitmap(path));

            pictureBox1.Image = bitmap;

            int height = bitmap.Size.Height;
            int width = bitmap.Size.Width;
            pictureBox1.Width = width * 420 / height;

        }
        public Bitmap CreateImage(Image src)
        {
            Bitmap newImage = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);
            Graphics gfx = Graphics.FromImage(newImage);
            gfx.DrawImage(src, 0, 0);
            return newImage;

        }
        public void refresh()
        {


            pictureBox1.Refresh();
            drawHistogram();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Table(byte[,] arr, Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;


            for (int i = 1; i <= width; i++)
            {

                dataGridView1.Columns.Add("", i.ToString());
                dataGridView1.Columns[i - 1].FillWeight = 1;


            }

            for (int i = 0; i < height; i++)
            {
                string[] row = new string[arr.GetLength(1)];
                for (int j = 0; j < width; j++)
                {
                    row[j] = arr[i, j].ToString();

                }


                dataGridView1.Rows.Add(row);

            }

        }
        public void ImageTo2DByteArray(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            byte[] bytes = new byte[height * data.Stride];
            try
            {
                Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
            }
            finally
            {
                bmp.UnlockBits(data);
            }

            byte[,] result = new byte[height, width];
            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    int offset = y * data.Stride + x * 3;
                    result[y, x] = (byte)((bytes[offset + 0] + bytes[offset + 1] + bytes[offset + 2]) / 3);

                }
            }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            Table(result, bmp);
        }


        private int[] GetHistogram(Bitmap picture)
        {
            int[] Histogram = new int[256];

            BitmapData bitmapData = picture.LockBits(new Rectangle(0, 0, picture.Size.Width, picture.Size.Height),
                            System.Drawing.Imaging.ImageLockMode.ReadOnly,
                            picture.PixelFormat);

            int PixelSize = 0;
            switch (picture.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    PixelSize = 4;
                    break;
                case PixelFormat.Format24bppRgb:
                    PixelSize = 3;
                    break;
            }

            unsafe
            {
                for (int y = 0; y < bitmapData.Height; y++)
                {
                    byte* row = (byte*)bitmapData.Scan0 + (y * bitmapData.Stride);

                    for (int x = 0; x < bitmapData.Width; x++)
                    {
                        byte color = (byte)((row[x * PixelSize] + row[x * PixelSize + 1] + row[x * PixelSize + 2]) / 3);
                        Histogram[color]++;
                    }
                }
            }

            picture.UnlockBits(bitmapData);

            return Histogram;
        }

        private void drawHistogram()
        {
            histogram = GetHistogram(bitmap);

            Graphics graphicsObj = panel3.CreateGraphics();
            Pen pen = new Pen(System.Drawing.Color.Black, 1);

            long max = histogram.Max();
            graphicsObj.Clear(panel3.BackColor);
            for (int i = 0; i < 256; i++)
            {
                graphicsObj.DrawLine(pen, i, 150, i, 150 - histogram[i] * 150 / max);
            }
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //  drawHistogram();

        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            H = "r";
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            H = "g";
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            H = "b";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            drawHistogram();
            H = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ImageTo2DByteArray(bitmap);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (H == "r")
            {
                label3.Text = e.X.ToString();
                label4.Text = histR[e.X].ToString();
            }
            else if (H == "g")
            {
                label3.Text = e.X.ToString();
                label4.Text = histG[e.X].ToString();
            }
            else if (H == "b")
            {
                label3.Text = e.X.ToString();
                label4.Text = histB[e.X].ToString();
            }
            else
            {
                label3.Text = e.X.ToString();
                label4.Text = histogram[e.X].ToString();
            }

           

           
        }



        private void radioButton1_Click(object sender, EventArgs e)
        {

            if (l == true)
            {
                this.ClientSize = new System.Drawing.Size(pictureBox1.Width + 400, 150);

                l = false;
                drawHistogram();


            }
            else
            {
                this.ClientSize = new System.Drawing.Size(292, 300);
                l = true;
                radioButton1.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPG(*.JPG)|*.jpg";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(sf.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            int w = bitmap.Width;
            int h = bitmap.Height;
            red = new double[w, h];
            green = new double[w, h];
            blue = new double[w, h];
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    red[i, j] = bitmap.GetPixel(i, j).R;
                    green[i, j] = bitmap.GetPixel(i, j).G;
                    blue[i, j] = bitmap.GetPixel(i, j).B;

                }
            }
            rbmp = new Bitmap(w, h);
            gbmp = new Bitmap(w, h);
            bbmp = new Bitmap(w, h);

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {

                    rbmp.SetPixel(i, j, Color.FromArgb((int)red[i, j], 0, 0));
                    gbmp.SetPixel(i, j, Color.FromArgb(0, (int)green[i, j], 0));
                    bbmp.SetPixel(i, j, Color.FromArgb(0, 0, (int)blue[i, j]));

                }
            }
            int[] His_R = new int[h];
            int[] His_G = new int[h];
            int[] His_B = new int[h];
            HistogramRGB(w, h, red, ref His_R);
            HistogramRGB(w, h, green, ref His_G);
            HistogramRGB(w, h, blue, ref His_B);


            Graphics graphicsObj = panel3.CreateGraphics();
            Pen pen = new Pen(System.Drawing.Color.Red, 1);

            long max = His_G.Max();
            graphicsObj.Clear(panel3.BackColor);
            for (int i = 0; i < 256; i++)
            {
                graphicsObj.DrawLine(pen, i, 256, i, 150 - His_R[i] * 150 / max);
                
            }
            Pen penG = new Pen(System.Drawing.Color.Green, 1);
            max = His_G.Max();
            for (int i = 0; i < 256; i++)
            {
                graphicsObj.DrawLine(penG, i, 256, i, 150 - His_G[i] * 150 / max);
            }
            Pen penB = new Pen(System.Drawing.Color.Blue, 1);
            max = His_B.Max();
            for (int i = 0; i < 256; i++)
            {
                graphicsObj.DrawLine(penB, i, 256, i, 150 - His_B[i] * 150 / max);
            }

            if (radioButton2.Checked == true)
            {
                graphicsObj.Clear(panel3.BackColor);
                Pen r = new Pen(System.Drawing.Color.Red, 1);
               
                long maxR = His_G.Max();
                
                for (int i = 0; i < 256; i++)
                {
                    graphicsObj.DrawLine(r, i, 256, i, 150 - His_R[i] * 150 / max);

                }
            }

            if (radioButton3.Checked == true)
            {
                graphicsObj.Clear(panel3.BackColor);
                Pen B = new Pen(System.Drawing.Color.Blue, 1);
                max = His_B.Max();
                for (int i = 0; i < 256; i++)
                {
                    graphicsObj.DrawLine(B, i, 256, i, 150 - His_B[i] * 150 / max);
                }
            }

            if (radioButton4.Checked == true)
            {
                graphicsObj.Clear(panel3.BackColor);
                Pen G = new Pen(System.Drawing.Color.Green, 1);
                max = His_G.Max();
                for (int i = 0; i < 256; i++)
                {
                    graphicsObj.DrawLine(G, i, 256, i, 150 - His_G[i] * 150 / max);
                }
            }
            histB = His_B;
            histG = His_G;
            histR = His_R;

            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

        }
        private void HistogramRGB(int width, int height, double[,] I, ref int[] His)
        {
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    if (I[i, j] > 255) I[i, j] = 255;
                    if (I[i, j] < 0) I[i, j] = 0;
                    His[(int)(I[i, j])]++;

                }
            }
        }
       
    }
}
