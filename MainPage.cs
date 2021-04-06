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
            activeChild.RozciaganieHistogram(0,256);
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

        private void ąganieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("Rozciaganie", "Podaj wartości do rozciagania:");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.rozciaganie(Convert.ToInt32(dialog.combovalue1), Convert.ToInt32(dialog.combovalue2));
        }

        private void redukcjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("Redukcja", "Podaj wartości 4 wartości (p1>p2>p3>p4:");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.Redukcja(Convert.ToInt32(dialog.combovalue1), Convert.ToInt32(dialog.combovalue2), Convert.ToInt32(dialog.combovalue3), Convert.ToInt32(dialog.combovalue4));
        }

        private void segmentacjaWododziałowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("Segmentacja Wododziałowa", "Ilość kroków wygładzania (1:50):", "Wypełnienie regionów (1-z obr,2-śre. regi.)");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;
            activeChild.wododzial(Convert.ToInt32(dialog.value), Convert.ToInt32(dialog.value2));
        }
    }
}
