using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO_AndrzejMróz_17870.Fuctions
{
   public  interface IFilter
    {
        void Convert();
        bool hasDialog { get; }

        void setImage(Image image);

        bool showDialog();
    }
}
