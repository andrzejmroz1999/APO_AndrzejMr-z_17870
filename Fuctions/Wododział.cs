using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO_AndrzejMróz_17870.Fuctions
{
   public class Wododział
    {
        //Funkcja do segmentacji obrazu metodą wododziałową
        public static Bitmap Watershed(Bitmap srcimg, string imgPath, WatershedForm form, float multiplier, int blocksize = 0)
        {
            Bitmap imgBmp = srcimg;
            //Wczytanie obrazu do segmentacji
            Mat img = GetMatFromSDImage(srcimg);

            //Konwersja obrazu na czarno-biały
            Mat img_gray = new Mat();
            CvInvoke.CvtColor(img, img_gray, ColorConversion.Bgr2Gray);

            //Progowanie obrazu czyli wstępne wyznaczenie obiektów
            Mat thresh = new Mat();
            if (blocksize == 0)
            {
                CvInvoke.Threshold(img_gray, thresh, 0, 255, ThresholdType.Otsu);
            }
            else
            {
                CvInvoke.AdaptiveThreshold(img_gray, thresh, 255, AdaptiveThresholdType.MeanC, ThresholdType.Binary, blocksize, 5);
            }

            

            //Negacja obrazu, ponieważ chcemy by tło było czarne a obiekty białe
            CvInvoke.BitwiseNot(thresh, thresh);

            //Odszumianie obrazu przez operacje morfologiczne
            float[,] k = { {1, 1, 1},
                        {1, 1, 1},
                        {1, 1, 1}};
            ConvolutionKernelF kernel = new ConvolutionKernelF(k);
            Point point = new Point(-1, -1);
            Mat opening = new Mat();
            CvInvoke.MorphologyEx(thresh, opening, MorphOp.Open, kernel, point, 1, BorderType.Default, new MCvScalar());

            //Wyznaczenie jednoznacznych obszarów tła
            Mat sure_bg = new Mat();
            CvInvoke.Dilate(opening, sure_bg, kernel, point, 1, BorderType.Default, new MCvScalar());

            //Wyznaczanie transformaty odległościowej
            Mat dist_transform = new Mat();
            CvInvoke.DistanceTransform(opening, dist_transform, null, DistType.L2, 5);

            //Musimy zapisać i odczytać obraz transformaty bo inaczej nie działa
            string imgPathTemp = imgPath;
            int find = imgPathTemp.IndexOf(@".");
            string imgPathTemp2 = imgPathTemp.Substring(0, find) + "_temp.bmp";
            dist_transform.Save(imgPathTemp2);
            dist_transform = new Mat(imgPathTemp2);
            File.Delete(imgPathTemp2);

            //Określenie jednoznacznych obszarów obiektów przez progowanie obrazu transformaty odlegościowej
            Mat sure_fg = new Mat();
            CvInvoke.Threshold(dist_transform, sure_fg, multiplier * Max(dist_transform.ToBitmap()), 255, ThresholdType.Binary);

            //Wyznaczenie obszarów "niepewnych" czyli odejmujemy jednoznaczne obszary tła od jednoznacznych obszarów obiektów
            Mat unknown = new Mat();
            CvInvoke.CvtColor(sure_fg, sure_fg, ColorConversion.Bgr2Gray);
            CvInvoke.Subtract(sure_bg, sure_fg, unknown, dtype: DepthType.Cv8U);

            //Etykietowanie obiektów
            Mat markers = new Mat();
            int counter = CvInvoke.ConnectedComponents(sure_fg, markers);
            markers.Save(imgPathTemp2);
            markers = new Mat(imgPathTemp2);
            File.Delete(imgPathTemp2);

            ///Dodanie wartości 1 do etykiet, tak aby tło miało wartość 1 a nie 0.
            Bitmap markersBmp = markers.ToBitmap();
            for (int i = 0; i < markersBmp.Width; i++)
            {
                for (int j = 0; j < markersBmp.Height; j++)
                {
                    Color oc = markersBmp.GetPixel(i, j);
                    Color nc = Color.FromArgb(oc.A, oc.R, oc.G, oc.B);
                    markersBmp.SetPixel(i, j, nc);
                }
            }

            //Oznaczenie obszarów "niepewnych" jako zero
            Bitmap unknownBmp = unknown.ToBitmap();
            for (int i = 0; i < unknownBmp.Width; i++)
            {
                for (int j = 0; j < unknownBmp.Height; j++)
                {
                    Color oc = unknownBmp.GetPixel(i, j);
                    Color nc = Color.FromArgb(oc.A, 0, 0, 0);
                    if (oc.R == 255)
                    {
                        markersBmp.SetPixel(i, j, nc);
                    }
                }
            }
            CvInvoke.CvtColor(markers, markers, ColorConversion.Bgr2Gray);
            img.Save(imgPathTemp2);

            //Do przeprowadzenia alogrytmu użyjemy obiektów typu Image(na Mat coś nie działa)
            Image<Bgr, Byte> img2 = new Image<Bgr, Byte>(imgPathTemp2);
            File.Delete(imgPathTemp2);
            markers.Save(imgPathTemp2);
            Image<Gray, Int32> markers2 = new Image<Gray, Int32>(imgPathTemp2);
            File.Delete(imgPathTemp2);

            //Przeprowadzenie algorytmu wododziału z biblioteki Emgu CV
            CvInvoke.Watershed(img2, markers2);

            //Zapisanie resultatu algorytmu na obrazie pierwotnym
           // Bitmap imgBmp = img.ToBitmap();
            Bitmap markers2Bmp = markers2.ToBitmap();
            for (int i = 0; i < markers2Bmp.Width; i++)
            {
                for (int j = 0; j < markers2Bmp.Height; j++)
                {
                    Color oc = markers2Bmp.GetPixel(i, j);
                    Color nc = Color.FromArgb(oc.A, 0, 0, 255);
                    if (oc.R == 0)
                    {
                        try
                        {
                            imgBmp.SetPixel(i, j, nc);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }
            }
            form.objNum = counter;
            //return dist_transform.ToBitmap();
            return imgBmp;
        }
        //Funkcja przyjmująca obiekt typu Image i przerabiająca go na obiekt typu Mat
        public static Mat GetMatFromSDImage(Image image)
        {
            int stride = 0;
            Bitmap bmp = new Bitmap(image);

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            System.Drawing.Imaging.PixelFormat pf = bmp.PixelFormat;
            if (pf == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                stride = bmp.Width * 4;
            }
            else
            {
                stride = bmp.Width * 3;
            }

            Image<Bgra, byte> cvImage = new Image<Bgra, byte>(bmp.Width, bmp.Height, stride, (IntPtr)bmpData.Scan0);

            bmp.UnlockBits(bmpData);

            return cvImage.Mat;
        }
        public static int Max(Bitmap img)
        {
            int max = img.GetPixel(0, 0).R;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color c = img.GetPixel(x, y);
                    if (c.R > max)
                    {
                        max = c.R;
                    }
                }

            }
            return max;
        }
    }
}
