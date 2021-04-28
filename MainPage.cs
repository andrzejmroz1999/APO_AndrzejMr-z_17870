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

            activeChild.progowanie(Convert.ToInt32(dialog.combovalue1));
        }

        private void operatorProgowaniazZachowaniemPoziomówSzarościToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("ProgowanieLevel", "Podaj wartości do progowania");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.progowanieOdcienie(Convert.ToInt32(dialog.combovalue1), Convert.ToInt32(dialog.combovalue2));
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

            activeChild.Redukcja(Convert.ToInt32(dialog.combovalue1));
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

        private void linioweWygładzanieWyostrzanieDetekcjaKrawedziToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            WygladzanieForm wygladzanie = new WygladzanieForm();

            if (wygladzanie.ShowDialog() == DialogResult.Cancel)
                return;
            activeChild.Wygladzanie(Convert.ToInt32(wygladzanie.value1), Convert.ToInt32(wygladzanie.value2), Convert.ToInt32(wygladzanie.value3), Convert.ToInt32(wygladzanie.value4), Convert.ToInt32(wygladzanie.value5), Convert.ToInt32(wygladzanie.value6), Convert.ToInt32(wygladzanie.value7), Convert.ToInt32(wygladzanie.value8), Convert.ToInt32(wygladzanie.value9));
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;
            activeChild.Mediana3x3();
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;
            activeChild.Mediana5x5();
        }

        private void x7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;
            activeChild.Mediana7x7();
        }

        private void prewittToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            PrewittForm prewittForm = new PrewittForm();

            if (prewittForm.ShowDialog() == DialogResult.Cancel)
                return;
            activeChild.Prewitt(Convert.ToInt32(prewittForm.value1), Convert.ToInt32(prewittForm.value2), Convert.ToInt32(prewittForm.value3), Convert.ToInt32(prewittForm.value4), Convert.ToInt32(prewittForm.value5), Convert.ToInt32(prewittForm.value6), Convert.ToInt32(prewittForm.value7), Convert.ToInt32(prewittForm.value8), Convert.ToInt32(prewittForm.value9));
        }

        private void aNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("Operacja logiczna AND",
                "Wybierz obraz", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.and(((ImageForm)this.MdiChildren[dialog.combovalue]).bitmap);
        }

        private void oRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("Operacja logiczna OR",
                "Wybierz obraz", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.or(((ImageForm)this.MdiChildren[dialog.combovalue]).bitmap);
        }

        private void xORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("Operacja logiczna XOR",
                "Wybierz obraz", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.xor(((ImageForm)this.MdiChildren[dialog.combovalue]).bitmap);
        }

        private void dodawanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("Dodawanie",
                "Wybierz obraz który chcesz dodać", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.add(((ImageForm)this.MdiChildren[dialog.combovalue]).bitmap);
        }

        private void odejmowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Dialog dialog = new Dialog("Odejmowanie",
                "Wybierz obraz który chcesz odjąć", this.MdiChildren);

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            activeChild.sub(((ImageForm)this.MdiChildren[dialog.combovalue]).bitmap);
        }
    }
}
