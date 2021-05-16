using Emgu.CV;
using Emgu.CV.Structure;
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
using Emgu.CV.Cuda;





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
        private const int MIN_VALUE = 0;
        private const int MAX_VALUE = 255;
       
      


        public int HistogramMinValue()
        {
            int minValue = 0;
            int minCunter = 0;

            while (minValue == 0)
            {
                if (histogram[minCunter] > 0) { minValue = minCunter; }
                minCunter += 1;
            }

            return minValue;
        }

        public int HistogramMaxValue()
        {
            int maxValue = 0;
            int maxCunter = 255;

            while (maxValue == 0)
            {
                if (histogram[maxCunter] > 0) { maxValue = maxCunter; }
                maxCunter -= 1;
            }

            return maxValue;
        }
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


        public void RozciaganieHistogram(int start, int koniec)
        {
            drawHistogram();
            int minValueHistogram = HistogramMinValue();
            int maxValueHistogram = HistogramMaxValue();
            int newValuePixel = 0;

            int[] stretchHist = new int[MAX_VALUE + 1];

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    Color c = bitmap.GetPixel(x, y);

                    if (c.R > minValueHistogram && c.R <= maxValueHistogram)
                    {
                        newValuePixel = ((c.R - minValueHistogram) * 255) / (maxValueHistogram - minValueHistogram);
                    }
                    else
                    {
                        newValuePixel = 0;
                    }

                    stretchHist[newValuePixel] += 1;

                    Color newColor = Color.FromArgb(255, newValuePixel, newValuePixel, newValuePixel);
                    bitmap.SetPixel(x, y, newColor);


                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }
        public void negacja()
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);

                    bitmap.SetPixel(j, i, Color.FromArgb(255 - c.R, 255 - c.R, 255 - c.R));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void progowanie(int prog)
        {

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


                    bitmap.SetPixel(x, y, newColor);

                }
            }


            pictureBox1.Refresh();
            drawHistogram();
        }

        public void progowanieOdcienie(int p1, int p2)
        {



            Bitmap bmpNew = new Bitmap(pictureBox1.Image);


            int newValuePixel = 0;



            for (int x = 0; x < bmpNew.Width; ++x)
            {
                for (int y = 0; y < bmpNew.Height; ++y)
                {
                    Color c = bmpNew.GetPixel(x, y);

                    if (c.R >= p1 && c.R <= p2)
                    {
                        newValuePixel = c.R;
                    }
                    else
                    {
                        newValuePixel = 0;
                    }



                    Color newColor = Color.FromArgb(255, newValuePixel, newValuePixel, newValuePixel);


                    bmpNew.SetPixel(x, y, newColor);

                }
            }
            pictureBox1.Image = bmpNew;
            pictureBox1.Refresh();
            bitmap = bmpNew;
            drawHistogram();


        }

        public void rozciaganie(int p1, int p2)
        {

            // zmienic zakres 4 parametry

            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                //if (p1 < p2)
                {
                    if (bitmap.GetPixel(x, y).R > p1 && bitmap.GetPixel(x, y).R <= p2)
                    {
                        var av = ((bitmap.GetPixel(x, y).R - p1) * ((256 - 1) / (p2 - p1)));
                        bitmap.SetPixel(x, y, Color.FromArgb(bitmap.GetPixel(x, y).A, av, av, av));
                    }
                }


            pictureBox1.Refresh();
            drawHistogram();
            Refresh();


        }

        public void Redukcja(int p1)
        {
            byte[] upo = new byte[256];
            float param1 = 255.0f / (p1 - 1);
            float param2 = 256.0f / (p1);
            for (int i = 0; i < 256; ++i)
            {
                upo[i] = (byte)((byte)(i / param2) * param1);
            }
            for (int i = 0; i < bitmap.Size.Width; ++i)
            {
                for (int j = 0; j < bitmap.Size.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    byte newColor = upo[color.R];
                    bitmap.SetPixel(i, j, Color.FromArgb(color.A, newColor, newColor, newColor));
                }
            }



            pictureBox1.Refresh();
            drawHistogram();
            Refresh();


        }

        public void Wygladzanie(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9)
        {
            float[,] matrix = new float[,] { { v1, v2, v3 }, { v4, v5, v6 }, { v7, v8, v9 } };
            ConvolutionKernelF kernel = new ConvolutionKernelF(matrix);
            var img = ((Bitmap)pictureBox1.Image).ToImage<Bgr, byte>();
            var imgDst = new Image<Bgr, byte>(pictureBox1.Image.Width,
            pictureBox1.Image.Height, new Bgr(0, 0, 0));
            Emgu.CV.CvInvoke.Filter2D(img, imgDst, kernel, new Point(-1, 1), 0, borderType: Emgu.CV.CvEnum.BorderType.Replicate);
            pictureBox1.Image = imgDst.ToBitmap();
            GC.Collect();

            pictureBox1.Refresh();
            drawHistogram();
            Refresh();

        }
        private int Mediana(int[] intArray)
        {
            int centerItem = Convert.ToInt32((double)intArray.Length / 2);

            Array.Sort(intArray);

            return intArray[centerItem];
        }

        public void Mediana3x3()
        {
            int newValuePixel = 0;



            for (int x = 1; x < bitmap.Width - 1; ++x)
            {
                for (int y = 1; y < bitmap.Height - 1; ++y)
                {
                    Color c1 = bitmap.GetPixel(x - 1, y - 1);
                    Color c2 = bitmap.GetPixel(x, y - 1);
                    Color c3 = bitmap.GetPixel(x + 1, y - 1);
                    Color c4 = bitmap.GetPixel(x - 1, y);
                    Color c5 = bitmap.GetPixel(x, y);
                    Color c6 = bitmap.GetPixel(x + 1, y);
                    Color c7 = bitmap.GetPixel(x - 1, y + 1);
                    Color c8 = bitmap.GetPixel(x, y + 1);
                    Color c9 = bitmap.GetPixel(x + 1, y + 1);

                    int[] environment = new int[] { c1.R, c2.R, c3.R, c4.R, c5.R, c6.R, c7.R, c8.R, c9.R };

                    newValuePixel = Mediana(environment);

                    Color newColor = Color.FromArgb(255, newValuePixel, newValuePixel, newValuePixel);

                    bitmap.SetPixel(x, y, newColor);
                }
            }

            pictureBox1.Refresh();
            drawHistogram();
            Refresh();
        }
        public void Mediana5x5()
        {
            int newValuePixel = 0;





            for (int x = 2; x < bitmap.Width - 2; ++x)
            {
                for (int y = 2; y < bitmap.Height - 2; ++y)
                {
                    // Row 1
                    Color c1 = bitmap.GetPixel(x - 2, y - 2);
                    Color c2 = bitmap.GetPixel(x - 1, y - 2);
                    Color c3 = bitmap.GetPixel(x, y - 2);
                    Color c4 = bitmap.GetPixel(x + 1, y - 2);
                    Color c5 = bitmap.GetPixel(x + 2, y - 2);

                    // Row 2
                    Color c6 = bitmap.GetPixel(x - 2, y - 1);
                    Color c7 = bitmap.GetPixel(x - 1, y - 1);
                    Color c8 = bitmap.GetPixel(x, y - 1);
                    Color c9 = bitmap.GetPixel(x + 1, y - 1);
                    Color c10 = bitmap.GetPixel(x + 2, y - 1);

                    // Row 3
                    Color c11 = bitmap.GetPixel(x - 2, y);
                    Color c12 = bitmap.GetPixel(x - 1, y);
                    Color c13 = bitmap.GetPixel(x, y);
                    Color c14 = bitmap.GetPixel(x + 1, y);
                    Color c15 = bitmap.GetPixel(x + 2, y);

                    // Row 4
                    Color c16 = bitmap.GetPixel(x - 2, y + 1);
                    Color c17 = bitmap.GetPixel(x - 1, y + 1);
                    Color c18 = bitmap.GetPixel(x, y + 1);
                    Color c19 = bitmap.GetPixel(x + 1, y + 1);
                    Color c20 = bitmap.GetPixel(x + 2, y + 1);

                    // Row 5
                    Color c21 = bitmap.GetPixel(x - 2, y + 2);
                    Color c22 = bitmap.GetPixel(x - 1, y + 2);
                    Color c23 = bitmap.GetPixel(x, y + 2);
                    Color c24 = bitmap.GetPixel(x + 1, y + 2);
                    Color c25 = bitmap.GetPixel(x + 2, y + 2);

                    int[] environment = new int[] { c1.R, c2.R, c3.R, c4.R, c5.R, c6.R, c7.R, c8.R, c9.R, c10.R,
                                                    c11.R, c12.R, c13.R, c14.R, c15.R, c16.R, c17.R, c18.R, c19.R, c20.R,
                                                    c21.R, c22.R, c23.R, c24.R, c25.R};

                    newValuePixel = Mediana(environment);

                    Color newColor = Color.FromArgb(255, newValuePixel, newValuePixel, newValuePixel);

                    bitmap.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
            Refresh();
        }
        public void Mediana7x7()
        {
            int newValuePixel = 0;


            for (int x = 3; x < bitmap.Width - 3; ++x)
            {
                for (int y = 3; y < bitmap.Height - 3; ++y)
                {
                    // Row 1
                    Color c1 = bitmap.GetPixel(x - 3, y - 3);
                    Color c2 = bitmap.GetPixel(x - 2, y - 3);
                    Color c3 = bitmap.GetPixel(x - 1, y - 3);
                    Color c4 = bitmap.GetPixel(x, y - 3);
                    Color c5 = bitmap.GetPixel(x + 1, y - 3);
                    Color c6 = bitmap.GetPixel(x + 2, y - 3);
                    Color c7 = bitmap.GetPixel(x + 3, y - 3);

                    // Row 2
                    Color c8 = bitmap.GetPixel(x - 3, y - 2);
                    Color c9 = bitmap.GetPixel(x - 2, y - 2);
                    Color c10 = bitmap.GetPixel(x - 1, y - 2);
                    Color c11 = bitmap.GetPixel(x, y - 2);
                    Color c12 = bitmap.GetPixel(x + 1, y - 2);
                    Color c13 = bitmap.GetPixel(x + 2, y - 2);
                    Color c14 = bitmap.GetPixel(x + 3, y - 2);

                    // Row 3
                    Color c15 = bitmap.GetPixel(x - 3, y - 1);
                    Color c16 = bitmap.GetPixel(x - 2, y - 1);
                    Color c17 = bitmap.GetPixel(x - 1, y - 1);
                    Color c18 = bitmap.GetPixel(x, y - 1);
                    Color c19 = bitmap.GetPixel(x + 1, y - 1);
                    Color c20 = bitmap.GetPixel(x + 2, y - 1);
                    Color c21 = bitmap.GetPixel(x + 3, y - 1);

                    // Row 4
                    Color c22 = bitmap.GetPixel(x - 3, y);
                    Color c23 = bitmap.GetPixel(x - 2, y);
                    Color c24 = bitmap.GetPixel(x - 1, y);
                    Color c25 = bitmap.GetPixel(x, y);
                    Color c26 = bitmap.GetPixel(x + 1, y);
                    Color c27 = bitmap.GetPixel(x + 2, y);
                    Color c28 = bitmap.GetPixel(x + 3, y);

                    // Row 5
                    Color c29 = bitmap.GetPixel(x - 3, y + 1);
                    Color c30 = bitmap.GetPixel(x - 2, y + 1);
                    Color c31 = bitmap.GetPixel(x - 1, y + 1);
                    Color c32 = bitmap.GetPixel(x, y + 1);
                    Color c33 = bitmap.GetPixel(x + 1, y + 1);
                    Color c34 = bitmap.GetPixel(x + 2, y + 1);
                    Color c35 = bitmap.GetPixel(x + 3, y + 1);

                    // Row 6
                    Color c36 = bitmap.GetPixel(x - 3, y + 2);
                    Color c37 = bitmap.GetPixel(x - 2, y + 2);
                    Color c38 = bitmap.GetPixel(x - 1, y + 2);
                    Color c39 = bitmap.GetPixel(x, y + 2);
                    Color c40 = bitmap.GetPixel(x + 1, y + 2);
                    Color c41 = bitmap.GetPixel(x + 2, y + 2);
                    Color c42 = bitmap.GetPixel(x + 3, y + 2);

                    // Row 7
                    Color c43 = bitmap.GetPixel(x - 3, y + 3);
                    Color c44 = bitmap.GetPixel(x - 2, y + 3);
                    Color c45 = bitmap.GetPixel(x - 1, y + 3);
                    Color c46 = bitmap.GetPixel(x, y + 3);
                    Color c47 = bitmap.GetPixel(x + 1, y + 3);
                    Color c48 = bitmap.GetPixel(x + 2, y + 3);
                    Color c49 = bitmap.GetPixel(x + 3, y + 3);

                    int[] environment = new int[] { c1.R, c2.R, c3.R, c4.R, c5.R, c6.R, c7.R, c8.R, c9.R, c10.R,
                                                    c11.R, c12.R, c13.R, c14.R, c15.R, c16.R, c17.R, c18.R, c19.R, c20.R,
                                                    c21.R, c22.R, c23.R, c24.R, c25.R, c26.R, c27.R, c28.R, c29.R, c30.R,
                                                    c31.R, c32.R, c33.R, c34.R, c35.R, c36.R, c37.R, c38.R, c39.R, c40.R,
                                                    c41.R, c42.R, c43.R, c44.R, c45.R, c46.R, c47.R, c48.R, c49.R };

                    newValuePixel = Mediana(environment);

                    Color newColor = Color.FromArgb(255, newValuePixel, newValuePixel, newValuePixel);

                    bitmap.SetPixel(x, y, newColor);
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
            Refresh();
        }

        public void Prewitt(int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9)
        {
           
            float[,] matrix = new float[,] { { v1, v2, v3 }, { v4, v5, v6 }, { v7, v8, v9 } };
            ConvolutionKernelF kernel = new ConvolutionKernelF(matrix);                                    
            var img = ((Bitmap)pictureBox1.Image).ToImage<Bgr, byte>();
            var imgDst = new Image<Bgr, byte>(pictureBox1.Image.Width,
            pictureBox1.Image.Height, new Bgr(0, 0, 0));
            Emgu.CV.CvInvoke.Filter2D(img, imgDst, kernel, new Point(-1, 1), 0, borderType: Emgu.CV.CvEnum.BorderType.Replicate);
            pictureBox1.Image = imgDst.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
            Refresh();

        }

        public void and(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = c.R & d.R;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
            Refresh();
        }

        public void or(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = c.R | d.R;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
            Refresh();
        }

        public void xor(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = c.R ^ d.R;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
            Refresh();
        }
        public void add(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = (c.R + d.R) / 2;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void sub(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = Math.Abs(c.R - d.R);

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public  void Erozja(Emgu.CV.CvEnum.BorderType type, Emgu.CV.CvEnum.ElementShape shape)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            Mat kernel = CvInvoke.GetStructuringElement(shape, new Size(5, 5), new Point(-1, -1));
            var img = bitmap.ToImage<Bgr, byte>();
            var imgDst = new Image<Bgr, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Bgr(0, 0, 0));
            Emgu.CV.CvInvoke.Erode(img, imgDst, kernel, new Point(-1, -1), 2, type, new MCvScalar(1.0));
            pictureBox1.Image = imgDst.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
        }
        public void Dylacja( Emgu.CV.CvEnum.BorderType type, Emgu.CV.CvEnum.ElementShape shape)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            Mat kernel = CvInvoke.GetStructuringElement(shape, new Size(5, 5), new Point(-1, -1));
            var img = bitmap.ToImage<Bgr, byte>();
            var imgDst = new Image<Bgr, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Bgr(0, 0, 0));
            Emgu.CV.CvInvoke.Dilate(img, imgDst, kernel, new Point(-1, -1), 2, type, new MCvScalar(1.0));
            pictureBox1.Image = imgDst.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
        }
        public void Otwarcie( Emgu.CV.CvEnum.BorderType type, Emgu.CV.CvEnum.ElementShape shape)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            Mat kernel = CvInvoke.GetStructuringElement(shape, new Size(5, 5), new Point(-1, -1));
            var img = bitmap.ToImage<Bgr, byte>();
            var imgDst = new Image<Bgr, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Bgr(0, 0, 0));
            Emgu.CV.CvInvoke.MorphologyEx(img, imgDst, Emgu.CV.CvEnum.MorphOp.Open, kernel, new Point(-1, -1), 2, type, new MCvScalar(1.0));
            pictureBox1.Image = imgDst.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
        }
        public  void Zamkniecie( Emgu.CV.CvEnum.BorderType type, Emgu.CV.CvEnum.ElementShape shape)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            Mat kernel = CvInvoke.GetStructuringElement(shape, new Size(5, 5), new Point(-1, -1));
            var img = bitmap.ToImage<Bgr, byte>();
            var imgDst = new Image<Bgr, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Bgr(0, 0, 0));
            Emgu.CV.CvInvoke.MorphologyEx(img, imgDst, Emgu.CV.CvEnum.MorphOp.Close, kernel, new Point(-1, -1), 2, type, new MCvScalar(1.0));
            pictureBox1.Image = imgDst.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
        }

        public  void Szkieletyzacja( Emgu.CV.CvEnum.BorderType type, Emgu.CV.CvEnum.ElementShape shape)
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            Mat kernel = CvInvoke.GetStructuringElement(shape, new Size(3, 3), new Point(-1, -1));

            var original = bitmap.ToImage<Gray, byte>();
            var skel = new Image<Gray, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Gray(0));
            var imgCopy = bitmap.ToImage<Gray, byte>();

            while (true)
            {
                var imgOpen = new Image<Gray, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Gray(0));
                Emgu.CV.CvInvoke.MorphologyEx(imgCopy, imgOpen, Emgu.CV.CvEnum.MorphOp.Open, kernel, new Point(-1, -1), 1, type, new MCvScalar(1.0));
                var imgTemp = new Image<Gray, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Gray(0));
                Emgu.CV.CvInvoke.Subtract(imgCopy, imgOpen, imgTemp);
                var imgEroded = new Image<Gray, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Gray(0));
                Emgu.CV.CvInvoke.Erode(imgCopy, imgEroded, kernel, new Point(-1, -1), 1, type, new MCvScalar(1.0));
                Emgu.CV.CvInvoke.BitwiseOr(skel, imgTemp, skel);
                imgCopy = imgEroded;
                if (Emgu.CV.CvInvoke.CountNonZero(imgCopy) == 0)
                {
                    break;
                }
            }

            pictureBox1.Image = skel.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
        }
        public void FiltracjaJednoetapowa(Emgu.CV.CvEnum.BorderType type)
        {
            var mask = new float[,]
            {
                {1, -1, 0, -1, 1},
                { -1, 1, 0, 1, -1},
                { 0, 0, 0, 0, 0},
                { -1, 1, 0, 1, -1},
                { 1, -1, 0, -1, 1}
            };
            ConvolutionKernelF kernel = new ConvolutionKernelF(mask);
            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            var img = bitmap.ToImage<Bgr, byte>();

            var imgDst = new Image<Bgr, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Bgr(0, 0, 0));
            Emgu.CV.CvInvoke.Filter2D(img, imgDst, kernel, new Point(-1, -1), 0, type);
            pictureBox1.Image = imgDst.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
        }
        public void FiltracjaDwuetapowa(Emgu.CV.CvEnum.BorderType type, int v1, int v2, int v3, int v4, int v5, int v6, int v7, int v8, int v9, int v10, int v11, int v12, int v13, int v14, int v15, int v16, int v17, int v18)
        {
            // dodawanie masek 
            var maskWygladzanie = new float[,] { { v1, v2, v3 }, { v4, v5, v6 }, { v7, v8, v9 } };
            var maskWyostrzanie = new float[,] { { v10, v11, v12 }, { v13, v14, v15 }, { v16, v17, v18 } };

            ConvolutionKernelF kernelWygladzanie = new ConvolutionKernelF(maskWygladzanie);
            ConvolutionKernelF kernelWyostrzanie = new ConvolutionKernelF(maskWyostrzanie);
            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            var img = bitmap.ToImage<Bgr, byte>();

            var imgRes1 = new Image<Bgr, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Bgr(0, 0, 0));
            Emgu.CV.CvInvoke.Filter2D(img, imgRes1, kernelWygladzanie, new Point(-1, -1), 0, type);           
            var imgRes2 = new Image<Bgr, byte>(pictureBox1.Image.Width, pictureBox1.Image.Height, new Bgr(0, 0, 0));
            Emgu.CV.CvInvoke.Filter2D(imgRes1, imgRes2, kernelWyostrzanie, new Point(-1, -1), 0, type);
            pictureBox1.Image = imgRes2.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
        }
        public void ProgowanieAdaptacyjne()
        {
           
            var img = bitmap.ToImage<Gray, byte>();
            var imgDst = new Image<Gray, byte>(bitmap.Width, bitmap.Height, new Gray(0));

            Emgu.CV.CvInvoke.AdaptiveThreshold(img, imgDst, 255, adaptiveType: Emgu.CV.CvEnum.AdaptiveThresholdType.MeanC,
                thresholdType: Emgu.CV.CvEnum.ThresholdType.Binary, 11, 5);
            pictureBox1.Image = imgDst.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
        }
        public  void ProgowanieOtsu()
        {
           
            var img = bitmap.ToImage<Gray, byte>();
            var imgDst = new Image<Gray, byte>(bitmap.Width, bitmap.Height, new Gray(0));

            Emgu.CV.CvInvoke.Threshold(img, imgDst, 0, 255, thresholdType: Emgu.CV.CvEnum.ThresholdType.Otsu);
            pictureBox1.Image = imgDst.ToBitmap();
            GC.Collect();
            pictureBox1.Refresh();
            drawHistogram();
        }
        public void Watershed()
        {
            var img = new Bitmap(pictureBox1.Image)
                   .ToImage<Bgr, byte>();
            var mask = img.Convert<Gray, byte>()
                .ThresholdBinaryInv(new Gray(150), new Gray(255));
            Mat distanceTransofrm = new Mat();
            CvInvoke.DistanceTransform(mask, distanceTransofrm, null, Emgu.CV.CvEnum.DistType.L2, 3);
            CvInvoke.Normalize(distanceTransofrm, distanceTransofrm, 0, 255, Emgu.CV.CvEnum.NormType.MinMax);
            var markers = distanceTransofrm.ToImage<Gray, byte>()
                .ThresholdBinary(new Gray(50), new Gray(255));
            CvInvoke.ConnectedComponents(markers, markers);
            var finalMarkers = markers.Convert<Gray, Int32>();

            CvInvoke.Watershed(img, finalMarkers);

            Image<Gray, byte> boundaries = finalMarkers.Convert<byte>(delegate (Int32 x)
            {
                return (byte)(x == -1 ? 255 : 0);
            });

            boundaries._Dilate(1);
            img.SetValue(new Bgr(0, 255, 0), boundaries);
           // AddImage(img, "Watershed Segmentation");
            pictureBox1.Image = img.ToBitmap();


            pictureBox1.Refresh();
            drawHistogram();
        }

        public void wododzial(int kr, int opcja)
        {
            int wynik, x, Wi, Hi, i, j;
            int poziom;
            int krok = 0;

            int[,] zdj = new int[bitmap.Width, bitmap.Height];

            Wi = bitmap.Width;
            Hi = bitmap.Height;

            Bitmap ft = new Bitmap(bitmap);
            Bitmap temp = new Bitmap(bitmap);

            // Gauss
            // ilość kroków -> kr

            for (x = 0; x < kr; x++)
            {
                krok = x + 1;


                for (i = 1; i < bitmap.Height - 1; i++)
                {
                    for (j = 1; j < bitmap.Width - 1; j++)
                    {

                        wynik = 1 * temp.GetPixel(j - 1, i - 1).R + 2 * temp.GetPixel(j, i - 1).R + 1 * temp.GetPixel(j + 1, i - 1).R;
                        wynik += 2 * temp.GetPixel(j - 1, i).R + 4 * temp.GetPixel(j, i).R + 2 * temp.GetPixel(j + 1, i).R;
                        wynik += 1 * temp.GetPixel(j - 1, i + 1).R + 2 * temp.GetPixel(j, i + 1).R + 1 * temp.GetPixel(j + 1, i + 1).R;

                        wynik = (int)(wynik / 16);

                        if (wynik > 255)
                        {
                            wynik = 255;
                        }

                        ft.SetPixel(j, i, Color.FromArgb(wynik, wynik, wynik));

                    }
                }

                temp = new Bitmap(ft);

            }



            // Gradient morfologiczny
            int pam;
            int dilate, erosion;

            for (i = 1; i < bitmap.Height - 1; i++)
            {
                for (j = 1; j < bitmap.Width - 1; j++)
                {
                    // Dylatacja - maks z sąsiedztwa
                    pam = ft.GetPixel(j, i).R;

                    if (pam < ft.GetPixel(j + 1, i).R)
                    {
                        pam = ft.GetPixel(j + 1, i).R;
                    }

                    if (pam < ft.GetPixel(j + 1, i + 1).R)
                    {
                        pam = ft.GetPixel(j + 1, i + 1).R;
                    }

                    if (pam < ft.GetPixel(j, i + 1).R)
                    {
                        pam = ft.GetPixel(j, i + 1).R;
                    }

                    if (pam < ft.GetPixel(j - 1, i + 1).R)
                    {
                        pam = ft.GetPixel(j - 1, i + 1).R;
                    }

                    if (pam < ft.GetPixel(j - 1, i).R)
                    {
                        pam = ft.GetPixel(j - 1, i).R;
                    }

                    if (pam < ft.GetPixel(j - 1, i - 1).R)
                    {
                        pam = ft.GetPixel(j - 1, i - 1).R;
                    }

                    if (pam < ft.GetPixel(j, i - 1).R)
                    {
                        pam = ft.GetPixel(j, i - 1).R;
                    }

                    if (pam < ft.GetPixel(j + 1, i - 1).R)
                    {
                        pam = ft.GetPixel(j + 1, i - 1).R;
                    }

                    dilate = pam;

                    // Erozja - min z sąsiedztwa
                    pam = ft.GetPixel(j, i).R;

                    if (pam > ft.GetPixel(j + 1, i).R)
                    {
                        pam = ft.GetPixel(j + 1, i).R;
                    }

                    if (pam > ft.GetPixel(j + 1, i + 1).R)
                    {
                        pam = ft.GetPixel(j + 1, i + 1).R;
                    }

                    if (pam > ft.GetPixel(j, i + 1).R)
                    {
                        pam = ft.GetPixel(j, i + 1).R;
                    }

                    if (pam > ft.GetPixel(j - 1, i + 1).R)
                    {
                        pam = ft.GetPixel(j - 1, i + 1).R;
                    }

                    if (pam > ft.GetPixel(j - 1, i).R)
                    {
                        pam = ft.GetPixel(j - 1, i).R;
                    }

                    if (pam > ft.GetPixel(j - 1, i - 1).R)
                    {
                        pam = ft.GetPixel(j - 1, i - 1).R;
                    }

                    if (pam > ft.GetPixel(j, i - 1).R)
                    {
                        pam = ft.GetPixel(j, i - 1).R;
                    }

                    if (pam > ft.GetPixel(j + 1, i - 1).R)
                    {
                        pam = ft.GetPixel(j + 1, i - 1).R;
                    }

                    erosion = pam;

                    pam = dilate - erosion;

                    temp.SetPixel(j, i, Color.FromArgb(pam, pam, pam));
                }
            }

            ft = new Bitmap(temp);

            // Watershed
            int region = 1;
            int min = 0, max = 0;
            int c, d, cx, dy;
            int punkty = 0, wall = 0;


            int[,] water = new int[bitmap.Width, bitmap.Height];
            int[,] water_t = new int[bitmap.Width, bitmap.Height];

            for (i = 0; i < bitmap.Height; i++)
            {
                for (j = 0; j < bitmap.Width; j++)
                {
                    if (j > 0 && j < bitmap.Width - 1 && i > 0 && i < bitmap.Height - 1)
                    {
                        if (j == 1 && i == 1)
                        {
                            min = ft.GetPixel(j, i).R;
                            max = ft.GetPixel(j, i).R;
                        }
                        else
                        {
                            if (ft.GetPixel(j, i).R < min)
                            {
                                min = ft.GetPixel(j, i).R;
                            }

                            if (ft.GetPixel(j, i).R > max)
                            {
                                max = ft.GetPixel(j, i).R;
                            }
                        }

                        water[j, i] = -1;
                    }
                    else
                    {
                        water[j, i] = 0;
                        water_t[j, i] = 0;
                    }

                    zdj[j, i] = ft.GetPixel(j, i).R;
                }
            }

            try
            {

                for (poziom = min; poziom <= max; poziom++)
                {


                    if (poziom == min)
                    {
                        // Krok 1 - pierwsze regiony
                        for (i = 1; i < Hi - 1; i++)
                        {
                            for (j = 1; j < Wi - 1; j++)
                            {
                                if (zdj[j, i] == poziom && water[j, i] == -1)
                                {
                                    zalewanie(j, i, region, poziom, zdj, ref water);

                                    region++;
                                }
                            }
                        }

                        for (i = 1; i < Hi - 1; i++)
                        {
                            for (j = 1; j < Wi - 1; j++)
                            {
                                water_t[j, i] = water[j, i];
                            }
                        }
                    }
                    else
                    {
                        // rozbudowa regionów
                        do //while (punkty > 0)
                        {
                            punkty = 0;

                            for (i = 1; i < Hi - 1; i++)
                            {
                                for (j = 1; j < Wi - 1; j++)
                                {
                                    if (water[j, i] > 0 && (water[j - 1, i - 1] == -1 || water[j - 1, i] == -1 || water[j - 1, i + 1] == -1 || water[j, i - 1] == -1 || water[j, i + 1] == -1 || water[j + 1, i - 1] == -1 || water[j + 1, i] == -1 || water[j + 1, i + 1] == -1))
                                    {
                                        for (d = i - 1; d <= i + 1; d++)
                                        {
                                            for (c = j - 1; c <= j + 1; c++)
                                            {
                                                if (water_t[c, d] == -1 && zdj[c, d] == poziom)
                                                {
                                                    wall = 0;

                                                    for (dy = d - 1; dy <= d + 1; dy++)
                                                    {
                                                        for (cx = c - 1; cx <= c + 1; cx++)
                                                        {
                                                            if (water_t[cx, dy] > 0 && water_t[cx, dy] != water[j, i])
                                                            {
                                                                wall = 1;

                                                                cx = c + 2;
                                                                dy = d + 2;
                                                            }
                                                        }
                                                    }

                                                    if (wall == 1)
                                                    {
                                                        water_t[c, d] = -4;
                                                    }
                                                    else
                                                    {
                                                        water_t[c, d] = water[j, i];
                                                    }

                                                    punkty++;

                                                } // end if water_t[c,d] == -1
                                            }
                                        }// end for c,d

                                    } // end if glowny
                                }
                            } // end for j,i

                            for (i = 1; i < Hi - 1; i++)
                            {
                                for (j = 1; j < Wi - 1; j++)
                                {
                                    water[j, i] = water_t[j, i];
                                }
                            }

                        } while (punkty > 0);

                        // nowe regiony
                        for (i = 1; i < Hi - 1; i++)
                        {
                            for (j = 1; j < Wi - 1; j++)
                            {
                                if (zdj[j, i] == poziom && water[j, i] == -1)
                                {
                                    zalewanie(j, i, region, poziom, zdj, ref water);

                                    region++;
                                }
                            }
                        }

                    }

                }// for poziom



                // obliczenie średniej regionów
                int[] sr = new int[region];

                if (opcja == 2)
                {
                    int[] li = new int[region];

                    for (i = 0; i < region; i++)
                    {
                        sr[i] = 0;
                        li[i] = 0;
                    }

                    for (i = 1; i < Hi - 1; i++)
                    {
                        for (j = 1; j < Wi - 1; j++)
                        {
                            if (water[j, i] > 0)
                            {
                                sr[(water[j, i] - 1)] += bitmap.GetPixel(j, i).R;
                                li[(water[j, i] - 1)]++;
                            }
                        }
                    }

                    for (i = 0; i < region; i++)
                    {
                        if (li[i] != 0)
                        {
                            sr[i] = sr[i] / li[i];
                        }
                    }

                }


                for (i = 1; i < bitmap.Height - 1; i++)
                {
                    for (j = 1; j < bitmap.Width - 1; j++)
                    {
                        // granice wododziałów
                        if (opcja == 1)
                        {
                            if (water[j, i] == -4)
                            {
                                bitmap.SetPixel(j, i, Color.FromArgb(255, 0, 0));
                            }
                        }

                        // średnie regionów
                        if (opcja == 2)
                        {
                            if (water[j, i] > 0)
                            {
                                bitmap.SetPixel(j, i, Color.FromArgb(sr[(water[j, i] - 1)], sr[(water[j, i] - 1)], sr[(water[j, i] - 1)]));
                            }
                            else
                            {
                                if (water[j, i] == -4)
                                {
                                    bitmap.SetPixel(j, i, Color.FromArgb(255, 0, 0));
                                }
                            }
                        }

                    }
                }
                pictureBox1.Image = bitmap;
                drawHistogram();

                // odrysowanie liczby regionów na obrazie
                Graphics nabitmap = Graphics.FromImage(bitmap);
                Graphics ekran = CreateGraphics();

                Font liczba = new Font("Verdana", ekran.DpiY / nabitmap.DpiY * Font.SizeInPoints, FontStyle.Bold);

                nabitmap.FillRectangle(Brushes.Snow, bitmap.Width - 55, 5, 55, 15);
                nabitmap.DrawString((region - 1).ToString(), liczba, Brushes.DarkSlateGray, new Point(bitmap.Width - 55, 5));

                nabitmap.Dispose();
                ekran.Dispose();
                pictureBox1.Image = bitmap;
                pictureBox1.Refresh();

                Invalidate();

            }
            catch (System.StackOverflowException ex)
            {
                Invalidate();

                MessageBox.Show("Przekroczono maksymalny rozmiar stosu w środowisku C# !!! \nProwdopododnie zbyt duża ilość pikseli tej samej barwy."
                    + "\n\n---> Pomniejsz zdjęcie i spróbuj ponownie.\n\n" + ex, "Błąd środowiska C#", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void zalewanie(int x, int y, int reg, int poz, int[,] z, ref int[,] wat)
        {
            if (z[x, y] == poz && wat[x, y] == -1)
            {
                wat[x, y] = reg;

                zalewanie(x - 1, y - 1, reg, poz, z, ref wat);
                zalewanie(x, y - 1, reg, poz, z, ref wat);
                zalewanie(x + 1, y - 1, reg, poz, z, ref wat);

                zalewanie(x - 1, y, reg, poz, z, ref wat);
                zalewanie(x + 1, y, reg, poz, z, ref wat);

                zalewanie(x - 1, y + 1, reg, poz, z, ref wat);
                zalewanie(x, y + 1, reg, poz, z, ref wat);
                zalewanie(x + 1, y + 1, reg, poz, z, ref wat);
            }
        }
    }

}

