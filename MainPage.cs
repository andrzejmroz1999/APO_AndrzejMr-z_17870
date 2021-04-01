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

        private void useFunction(IOperations operation)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            operation.SetImage(activeChild.bitmap);



            operation.Convert();

            activeChild.refresh();
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
           

            ImageForm picture = new ImageForm(this);
            picture.MdiParent = this;
            picture.Text = new StringBuilder("Image: ").Append(++Counter).ToString();
            picture.LoadImage(openFileDialog1.FileName);
            picture.Show();
        }

        private void konwertujDoSzarości8BitToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            useFunction(new GrayScale());
        
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

        private void inforamcjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoForm f = new InfoForm();
            f.Show();
        }

        private void rozciąganieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;                  
            activeChild.rozciaganie(0,256);
        }

        private void negacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild != null)
            {
                activeChild.negacja();
            }
        }

        private void progowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("Progowanie", "Podaj wartość do progowania");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.progowanie(Convert.ToInt32(dialog.value));
        }

        private void operatorProgowaniazZachowaniemPoziomówSzarościToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("ProgowanieLevel", "Podaj wartości do progowania");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.progowanieLevel(Convert.ToInt32(dialog.combovalue1), Convert.ToInt32(dialog.combovalue2));
        }
    }
}
