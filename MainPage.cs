using APO_AndrzejMróz_17870.Fuction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APO_AndrzejMróz_17870
{
    public partial class MainPage : Form
    {
        private int Counter = 0;
        public MainPage()
        {
            InitializeComponent();
        }

        private void useFilter(IFilter filter)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            filter.setImage(activeChild.bitmap);

            if (filter.hasDialog)
                if (!filter.showDialog())
                    return;

            filter.Convert();

            activeChild.refresh();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
           

            ImageForm picture = new ImageForm(this);
            picture.MdiParent = this;
            picture.Text = new StringBuilder("Image: ").Append(++Counter).ToString();
            picture.loadImage(openFileDialog1.FileName);
            picture.Show();
        }

        private void konwertujDoSzarości8BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            useFilter(new GrayScale());
        
        }

        private void duplikujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild != null)
            {
                ImageForm newChild = new ImageForm(activeChild);
                newChild.Text = new StringBuilder("Obraz ").Append(++Counter).ToString();
                newChild.MdiParent = this;
                newChild.Show();
            }
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPG(*.JPG)|*.jpg";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                ActiveMdiChild.Controls.GetType
            }
        }
    }
}
