using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO_AndrzejMróz_17870.Fuction
{
    class GrayScale : IOperations
    {               
       private Image image;

        public GrayScale()
        {
            
        }

        public void SetImage(Image image)
        {
            this.image = image;
        }

        public void Convert()
        {
            //Download a bitmap form image
            Graphics g = Graphics.FromImage(image);

            //create grayscale matrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
                  {
                     new float[] {.3f, .3f, .3f, 0, 0},
                     new float[] {.59f, .59f, .59f, 0, 0},
                     new float[] {.11f, .11f, .11f, 0, 0},
                     new float[] {0, 0, 0, 1, 0},
                     new float[] {0, 0, 0, 0, 1}
                  });

            //adds attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale matrix
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
               0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);

            //dispose the bitmap
            g.Dispose();
        }

       
    }
}
