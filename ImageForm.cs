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
        private const int MIN_VALUE = 0;
        private const int MAX_VALUE = 255;
        

        public int MinValueHistogram()
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

        public int MaxValueHistogram()
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
            int minValueHistogram = MinValueHistogram();
            int maxValueHistogram = MaxValueHistogram();
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
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);

                    int color = (c.R < prog ? 0 : 255);

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public int[] progowanieLevel(int p1, int p2)
        {
          

         
            Bitmap bmNew = new Bitmap(pictureBox1.Image);

           
            int newValuePixel = 0;

            int[] progowanieHist = new int[256];

            for (int x = 0; x < bmNew.Width; ++x)
            {
                for (int y = 0; y < bmNew.Height; ++y)
                {
                    Color c = bmNew.GetPixel(x, y);

                    if (c.R >= p1 && c.R <= p2)
                    {
                        newValuePixel = c.R;
                    }
                    else
                    {
                        newValuePixel = 0;
                    }

                    progowanieHist[newValuePixel] += 1;

                    Color newColor = Color.FromArgb(255, newValuePixel, newValuePixel, newValuePixel);

                  
                        bmNew.SetPixel(x, y, newColor);
                    
                }
            }
           pictureBox1.Image = bmNew;
            pictureBox1.Refresh();
            bitmap = bmNew;
            drawHistogram();

            return progowanieHist;
        }

        public int[] rozciaganie(int p1, int p2)
        {
            

           
            int newValuePixel = 0;

            int[] rozciaganieHist = new int[256];

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    Color c = bitmap.GetPixel(x, y);

                    if (c.R >= p1 && c.R <= p2)
                    {
                        newValuePixel = ((c.R - p1) * 255) / (p2 - p1);
                    }
                    else
                    {
                        newValuePixel = 0;
                    }

                    rozciaganieHist[newValuePixel] += 1;

                    Color newColor = Color.FromArgb(255, newValuePixel, newValuePixel, newValuePixel);

                   
                        bitmap.SetPixel(x, y, newColor);
                  
                }
            }

            pictureBox1.Refresh();
            drawHistogram();
            Refresh();

            return rozciaganieHist;
        }

        public int[] Redukcja(int p1, int p2 , int p3 , int p4)
        {
           

          

           
            int newValuePixel = 0;

            int[] redukcjaHist = new int[256];

            Random rnd = new Random();

            int random1 = rnd.Next(p1, p2);
            int random2 = rnd.Next(p2, p3);
            int random3 = rnd.Next(p3, p4);

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    Color c = bitmap.GetPixel(x, y);

                    if (c.R > p1 && c.R <= p2)
                    {
                        newValuePixel = random1;
                    }
                    else
                    {
                        if (c.R > p2 && c.R <= p3)
                        {
                            newValuePixel = random2;
                        }
                        else
                        {
                            if (c.R > p3 && c.R <= p4)
                            {
                                newValuePixel = random3;
                            }
                            else
                            {
                                newValuePixel = 255;
                            }
                        }
                    }

                    redukcjaHist[newValuePixel] += 1;

                    Color newColor = Color.FromArgb(255, newValuePixel, newValuePixel, newValuePixel);

                   
                        bitmap.SetPixel(x, y, newColor);
                   
                }
            }

            pictureBox1.Refresh();
            drawHistogram();
            Refresh();

            return redukcjaHist;
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

