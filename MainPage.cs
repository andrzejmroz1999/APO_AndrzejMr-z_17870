using APO_AndrzejMróz_17870.Fuction;
using APO_AndrzejMróz_17870.Fuctions;
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
        public string imgPath;
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
            imgPath = openFileDialog1.FileName;
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
            if (activeChild != null)
                useFilter(new UOP());
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

        private void isolatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Erozja(Emgu.CV.CvEnum.BorderType.Isolated, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void reflectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Erozja(Emgu.CV.CvEnum.BorderType.Reflect, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void replicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Erozja(Emgu.CV.CvEnum.BorderType.Replicate, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void isolatedToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Erozja(Emgu.CV.CvEnum.BorderType.Isolated, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void reflectedToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Erozja(Emgu.CV.CvEnum.BorderType.Reflect, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void replicateToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Erozja(Emgu.CV.CvEnum.BorderType.Replicate, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void isolatedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Dylacja(Emgu.CV.CvEnum.BorderType.Isolated, Emgu.CV.CvEnum.ElementShape.Cross);

        }

        private void reflectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Dylacja(Emgu.CV.CvEnum.BorderType.Reflect, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void isolatedToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Dylacja(Emgu.CV.CvEnum.BorderType.Isolated, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void replicateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Dylacja(Emgu.CV.CvEnum.BorderType.Replicate, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void reflectedToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Dylacja(Emgu.CV.CvEnum.BorderType.Reflect, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void replicateToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Dylacja(Emgu.CV.CvEnum.BorderType.Replicate, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void isolatedToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Otwarcie(Emgu.CV.CvEnum.BorderType.Isolated, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void reflecetedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Otwarcie(Emgu.CV.CvEnum.BorderType.Reflect, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void replicateToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Otwarcie(Emgu.CV.CvEnum.BorderType.Replicate, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void isolatedToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Zamkniecie(Emgu.CV.CvEnum.BorderType.Isolated, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void reflectedToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Zamkniecie(Emgu.CV.CvEnum.BorderType.Reflect, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void replicateToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Zamkniecie(Emgu.CV.CvEnum.BorderType.Replicate, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void isolatedToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Otwarcie(Emgu.CV.CvEnum.BorderType.Isolated, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void reflectedToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Otwarcie(Emgu.CV.CvEnum.BorderType.Reflect, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void replicateToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Otwarcie(Emgu.CV.CvEnum.BorderType.Replicate, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void isolatedToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Zamkniecie(Emgu.CV.CvEnum.BorderType.Isolated, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void reflectedToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Zamkniecie(Emgu.CV.CvEnum.BorderType.Reflect, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void replicatedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Zamkniecie(Emgu.CV.CvEnum.BorderType.Replicate, Emgu.CV.CvEnum.ElementShape.Rectangle);
        }

        private void szkieletyzacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.Szkieletyzacja(Emgu.CV.CvEnum.BorderType.Replicate, Emgu.CV.CvEnum.ElementShape.Cross);
        }

        private void isolatedToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.FiltracjaJednoetapowa(Emgu.CV.CvEnum.BorderType.Isolated);
        }

        private void reflectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.FiltracjaJednoetapowa(Emgu.CV.CvEnum.BorderType.Reflect);
        }

        private void replicateToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.FiltracjaJednoetapowa(Emgu.CV.CvEnum.BorderType.Replicate);
        }

        private void isolatedToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;

            WygladzanieForm wygladzanie = new WygladzanieForm("a");

            if (wygladzanie.ShowDialog() == DialogResult.Cancel)
                return;
            activeChild.FiltracjaDwuetapowa(Emgu.CV.CvEnum.BorderType.Isolated, Convert.ToInt32(wygladzanie.value1), Convert.ToInt32(wygladzanie.value2), Convert.ToInt32(wygladzanie.value3), Convert.ToInt32(wygladzanie.value4), Convert.ToInt32(wygladzanie.value5), Convert.ToInt32(wygladzanie.value6), Convert.ToInt32(wygladzanie.value7), Convert.ToInt32(wygladzanie.value8), Convert.ToInt32(wygladzanie.value9), Convert.ToInt32(wygladzanie.value10), Convert.ToInt32(wygladzanie.value11), Convert.ToInt32(wygladzanie.value12), Convert.ToInt32(wygladzanie.value13), Convert.ToInt32(wygladzanie.value14), Convert.ToInt32(wygladzanie.value15), Convert.ToInt32(wygladzanie.value16), Convert.ToInt32(wygladzanie.value17), Convert.ToInt32(wygladzanie.value18));
        }

        private void reflectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;

            WygladzanieForm wygladzanie = new WygladzanieForm("a");

            if (wygladzanie.ShowDialog() == DialogResult.Cancel)
                return;
            activeChild.FiltracjaDwuetapowa(Emgu.CV.CvEnum.BorderType.Reflect,Convert.ToInt32(wygladzanie.value1), Convert.ToInt32(wygladzanie.value2), Convert.ToInt32(wygladzanie.value3), Convert.ToInt32(wygladzanie.value4), Convert.ToInt32(wygladzanie.value5), Convert.ToInt32(wygladzanie.value6), Convert.ToInt32(wygladzanie.value7), Convert.ToInt32(wygladzanie.value8), Convert.ToInt32(wygladzanie.value9), Convert.ToInt32(wygladzanie.value10), Convert.ToInt32(wygladzanie.value11), Convert.ToInt32(wygladzanie.value12), Convert.ToInt32(wygladzanie.value13), Convert.ToInt32(wygladzanie.value14), Convert.ToInt32(wygladzanie.value15), Convert.ToInt32(wygladzanie.value16), Convert.ToInt32(wygladzanie.value17), Convert.ToInt32(wygladzanie.value18));
            
        }

        private void replicateToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            
            WygladzanieForm wygladzanie = new WygladzanieForm("a");

            if (wygladzanie.ShowDialog() == DialogResult.Cancel)
                return;
            activeChild.FiltracjaDwuetapowa(Emgu.CV.CvEnum.BorderType.Replicate, Convert.ToInt32(wygladzanie.value1), Convert.ToInt32(wygladzanie.value2), Convert.ToInt32(wygladzanie.value3), Convert.ToInt32(wygladzanie.value4), Convert.ToInt32(wygladzanie.value5), Convert.ToInt32(wygladzanie.value6), Convert.ToInt32(wygladzanie.value7), Convert.ToInt32(wygladzanie.value8), Convert.ToInt32(wygladzanie.value9), Convert.ToInt32(wygladzanie.value10), Convert.ToInt32(wygladzanie.value11), Convert.ToInt32(wygladzanie.value12), Convert.ToInt32(wygladzanie.value13), Convert.ToInt32(wygladzanie.value14), Convert.ToInt32(wygladzanie.value15), Convert.ToInt32(wygladzanie.value16), Convert.ToInt32(wygladzanie.value17), Convert.ToInt32(wygladzanie.value18));
        }

        private void progowanieAdaptacyjneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.ProgowanieAdaptacyjne();
        }

        private void progowanieOtsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.ProgowanieOtsu();
        }

        private void metodaWododziałowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            WatershedForm watershedForm = new WatershedForm(activeChild.bitmap,imgPath);

            if (watershedForm.ShowDialog() == DialogResult.Cancel)
                return;


            activeChild.Wododzial(watershedForm.result);


        }

        private void wyrównanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            activeChild.wyrownanie();
        }

        private void momentyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            Moments moments = new Moments(activeChild.bitmap);

            if (moments.ShowDialog() == DialogResult.Cancel)
                return;

        }

        private void obszarIObwódToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            AreaAndPerimeter areaAndPerimeter = new AreaAndPerimeter(activeChild.bitmap);

            if (areaAndPerimeter.ShowDialog() == DialogResult.Cancel)
                return;
        }

        private void wsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageForm activeChild = (ImageForm)this.ActiveMdiChild;

            if (activeChild == null)
                return;

            FormFactors formFactors = new FormFactors(activeChild.bitmap);

            if (formFactors.ShowDialog() == DialogResult.Cancel)
                return;
        }

       
    }
}
