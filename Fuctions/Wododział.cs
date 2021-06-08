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
        public static Bitmap Watershed(Bitmap sourceImage, string pathPicture, WatershedForm form, float factor, int param = 0)
        {
            Bitmap Bmp = sourceImage;
            //Wczytanie obrazu do segmentacji
            Mat img = GetMatFromSDImage(sourceImage);

            //Konwersja obrazu na czarno-biały
            Mat grayBmp = new Mat();
            CvInvoke.CvtColor(img, grayBmp, ColorConversion.Bgr2Gray);

            //Progowanie obrazu wybraną metodą
            Mat threshold = new Mat();
            if (param == 0)
            {
                CvInvoke.Threshold(grayBmp, threshold, 0, 255, ThresholdType.Otsu);
            }
            else
            {
                CvInvoke.AdaptiveThreshold(grayBmp, threshold, 255, AdaptiveThresholdType.MeanC, ThresholdType.Binary, param, 5);
            }
          



            //Negacja obrazu
            CvInvoke.BitwiseNot(threshold, threshold);

            //Odszumianie przez operacje morfologiczne
            ConvolutionKernelF kernel;
            Point point;
            Mat Open;
            noiseReduction(threshold, out kernel, out point, out Open);

            // jednoznaczne obszary tła
            Mat Bg = new Mat();
            CvInvoke.Dilate(Open, Bg, kernel, point, 1, BorderType.Default, new MCvScalar());

            // transformata odległościowa
            Mat DistanceTransform = new Mat();
            CvInvoke.DistanceTransform(Open, DistanceTransform, null, DistType.L2, 5);

            //zapis i odczyt obrazu transformaty
            string temp = TransformRecord(pathPicture, ref DistanceTransform);

            //Określamy jednoznaczne obszary obiektów poprzez progowanie obrazu transformaty odlegościowej
            Mat Fg = new Mat();
            CvInvoke.Threshold(DistanceTransform, Fg, factor * Max(DistanceTransform.ToBitmap()), 255, ThresholdType.Binary);

            //Wyznaczenie obszarów niepewnych poprzez odejmowanie jednoznacznych obszarów tła od jednoznacznych obszarów obiektów
            Mat Unknown = new Mat();
            CvInvoke.CvtColor(Fg, Fg, ColorConversion.Bgr2Gray);
            CvInvoke.Subtract(Bg, Fg, Unknown, dtype: DepthType.Cv8U);

            //Etykietowanie obiektów
            Mat Markers = new Mat();
            int counter = CvInvoke.ConnectedComponents(Fg, Markers);
            Markers.Save(temp);
            Markers = new Mat(temp);
            File.Delete(temp);

            ///Dodanie wartości 1 do etykiet, tak aby tło miało wartość 1 zamstast 0.
            Bitmap markersBmp = Markers.ToBitmap();
            for (int i = 0; i < markersBmp.Width; i++)
            {
                for (int j = 0; j < markersBmp.Height; j++)
                {
                    Color oc = markersBmp.GetPixel(i, j);
                    Color nc = Color.FromArgb(oc.A, oc.R, oc.G, oc.B);
                    markersBmp.SetPixel(i, j, nc);
                }
            }

            //Oznaczenie obszarów niepewnych jako 0
            Bitmap unknownBmp = Unknown.ToBitmap();
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
            CvInvoke.CvtColor(Markers, Markers, ColorConversion.Bgr2Gray);
            img.Save(temp);

            //algortm przeprowadzamy na obiektach typu Image
            Image<Bgr, Byte> img2 = new Image<Bgr, Byte>(temp);
            File.Delete(temp);
            Markers.Save(temp);
            Image<Gray, Int32> markers2 = new Image<Gray, Int32>(temp);
            File.Delete(temp);

            //algorytm wododziałowy z Emgu CV
            CvInvoke.Watershed(img2, markers2);

            //Zapisanie wyniku algorytmu na obrazie pierwotnym         
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
                            Bmp.SetPixel(i, j, nc);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }
            }
            form.ObjCounter = counter;

            return Bmp;
        }

        private static void noiseReduction(Mat threshold, out ConvolutionKernelF kernel, out Point point, out Mat open)
        {
            float[,] mask = { {1, 1, 1},
                        {1, 1, 1},
                        {1, 1, 1}};
            kernel = new ConvolutionKernelF(mask);
            point = new Point(-1, -1);
            open = new Mat();
            CvInvoke.MorphologyEx(threshold, open, MorphOp.Open, kernel, point, 1, BorderType.Default, new MCvScalar());
        }

        private static string TransformRecord(string pathPicture, ref Mat DistanceTransform)
        {
            string auxiliaryPath = pathPicture;
            int find = auxiliaryPath.IndexOf(@".");
            string temp = auxiliaryPath.Substring(0, find) + "_temp.bmp";
            DistanceTransform.Save(temp);
            DistanceTransform = new Mat(temp);
            File.Delete(temp);
            return temp;
        }

        //przeciązenie funkcji Watershed na potrzeby progowania
        public static Bitmap Watershed(Bitmap sourceImage, string pathPicture, WatershedForm form, float factor, Bitmap bmp, int param = 0)
        {
            Bitmap Bmp = bmp;
           
            Mat img = GetMatFromSDImage(bmp);

          
            Mat grayBmp = new Mat();
            CvInvoke.CvtColor(img, grayBmp, ColorConversion.Bgr2Gray);

           
            Mat threshold = new Mat();
            if (param == 0)
            {
                CvInvoke.Threshold(grayBmp, threshold, 0, 255, ThresholdType.Otsu);
            }
            else
            {
                CvInvoke.AdaptiveThreshold(grayBmp, threshold, 255, AdaptiveThresholdType.MeanC, ThresholdType.Binary, param, 5);
            }



          
            CvInvoke.BitwiseNot(threshold, threshold);


            ConvolutionKernelF kernel;
            Point point;
            Mat Open;
            noiseReduction(threshold, out kernel, out point, out Open);

            Mat Bg = new Mat();
            CvInvoke.Dilate(Open, Bg, kernel, point, 1, BorderType.Default, new MCvScalar());

            Mat DistanceTransform = new Mat();
            CvInvoke.DistanceTransform(Open, DistanceTransform, null, DistType.L2, 5);

            string auxiliaryPath = pathPicture;
            int find = auxiliaryPath.IndexOf(@".");
            string temp = auxiliaryPath.Substring(0, find) + "_temp.bmp";
            DistanceTransform.Save(temp);
            DistanceTransform = new Mat(temp);
            File.Delete(temp);

            Mat Fg = new Mat();
            CvInvoke.Threshold(DistanceTransform, Fg, factor * Max(DistanceTransform.ToBitmap()), 255, ThresholdType.Binary);

            Mat Unknown = new Mat();
            CvInvoke.CvtColor(Fg, Fg, ColorConversion.Bgr2Gray);
            CvInvoke.Subtract(Bg, Fg, Unknown, dtype: DepthType.Cv8U);

            Mat Markers = new Mat();
            int counter = CvInvoke.ConnectedComponents(Fg, Markers);
            Markers.Save(temp);
            Markers = new Mat(temp);
            File.Delete(temp);

            Bitmap markersBmp = Markers.ToBitmap();
            for (int i = 0; i < markersBmp.Width; i++)
            {
                for (int j = 0; j < markersBmp.Height; j++)
                {
                    Color oc = markersBmp.GetPixel(i, j);
                    Color nc = Color.FromArgb(oc.A, oc.R, oc.G, oc.B);
                    markersBmp.SetPixel(i, j, nc);
                }
            }

            Bitmap unknownBmp = Unknown.ToBitmap();
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
            CvInvoke.CvtColor(Markers, Markers, ColorConversion.Bgr2Gray);
            img.Save(temp);

            Image<Bgr, Byte> img2 = new Image<Bgr, Byte>(temp);
            File.Delete(temp);
            Markers.Save(temp);
            Image<Gray, Int32> markers2 = new Image<Gray, Int32>(temp);
            File.Delete(temp);

            CvInvoke.Watershed(img2, markers2);

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
                            sourceImage.SetPixel(i, j, nc);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }
            }
            form.ObjCounter = counter;
            return sourceImage;
        }
       
        public static Mat GetMatFromSDImage(Image image)
        {
            int step = 0;
            Bitmap bmp = new Bitmap(image);

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

            System.Drawing.Imaging.PixelFormat pf = bmp.PixelFormat;
            if (pf == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                step = bmp.Width * 4;
            }
            else
            {
                step = bmp.Width * 3;
            }

            Image<Bgra, byte> cvImage = new Image<Bgra, byte>(bmp.Width, bmp.Height, step, (IntPtr)bmpData.Scan0);

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
