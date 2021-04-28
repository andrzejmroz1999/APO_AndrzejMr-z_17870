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

    public partial class WygladzanieForm : Form
    {
        public decimal value1;
        public decimal value2;
        public decimal value3;
        public decimal value4;
        public decimal value5;
        public decimal value6;
        public decimal value7;
        public decimal value8;
        public decimal value9;

        public WygladzanieForm()
        {
            InitializeComponent();


            standardMaska.Items.Add("WYGŁADZANIE / K - 1/9");
            standardMaska.Items.Add("WYGŁADZANIE / K - 1/10");
            standardMaska.Items.Add("WYGŁADZANIE / K - 1/16");
            standardMaska.Items.Add("WYOSTRZANIE / Maska a");
            standardMaska.Items.Add("WYOSTRZANIE / Maska b");
            standardMaska.Items.Add("WYOSTRZANIE / Maska c");
          //  standardMaska.Items.Add("WYOSTRZANIE / Maska d");
            standardMaska.Items.Add("DETEKCJA KRAWĘDZI / Maska 1");
            standardMaska.Items.Add("DETEKCJA KRAWĘDZI / Maska 2");
            standardMaska.Items.Add("DETEKCJA KRAWĘDZI / Maska 3");
        }

        private void buttonSelectMask_Click(object sender, EventArgs e)
        {
            if (standardMaska.SelectedIndex == 0)
            {
                mask1.Value = 1; mask2.Value = 1; mask3.Value = 1;
                mask4.Value = 1; mask5.Value = 1; mask6.Value = 1;
                mask7.Value = 1; mask8.Value = 1; mask9.Value = 1;
            }

            if (standardMaska.SelectedIndex == 1)
            {
                mask1.Value = 1; mask2.Value = 1; mask3.Value = 1;
                mask4.Value = 1; mask5.Value = 2; mask6.Value = 1;
                mask7.Value = 1; mask8.Value = 1; mask9.Value = 1;
            }

            if (standardMaska.SelectedIndex == 2)
            {
                mask1.Value = 1; mask2.Value = 2; mask3.Value = 1;
                mask4.Value = 2; mask5.Value = 4; mask6.Value = 2;
                mask7.Value = 1; mask8.Value = 2; mask9.Value = 1;
            }

            // WYOSTRZANIE
            if (standardMaska.SelectedIndex == 3)
            {
                mask1.Value = 0; mask2.Value = -1; mask3.Value = 0;
                mask4.Value = -1; mask5.Value = 4; mask6.Value = -1;
                mask7.Value = 0; mask8.Value = -1; mask9.Value = 0;
            }

            if (standardMaska.SelectedIndex == 4)
            {
                mask1.Value = -1; mask2.Value = -1; mask3.Value = -1;
                mask4.Value = -1; mask5.Value = 8; mask6.Value = -1;
                mask7.Value = -1; mask8.Value = -1; mask9.Value = -1;
            }

            if (standardMaska.SelectedIndex == 5)
            {
                mask1.Value = -1; mask2.Value = -2; mask3.Value = -1;
                mask4.Value = -2; mask5.Value = 4; mask6.Value = -2;
                mask7.Value = -1; mask8.Value = -2; mask9.Value = -1;
            }

            if (standardMaska.SelectedIndex == 6)
            {
                mask1.Value = -1; mask2.Value = -1; mask3.Value = -1;
                mask4.Value = -1; mask5.Value = 9; mask6.Value = -1;
                mask7.Value = -1; mask8.Value = -1; mask9.Value = -1;
            }

            // DETEKCJA KRAWEDZI
            if (standardMaska.SelectedIndex == 7)
            {
                mask1.Value = 1; mask2.Value = -2; mask3.Value = 1;
                mask4.Value = -2; mask5.Value = 5; mask6.Value = -2;
                mask7.Value = 1; mask8.Value = -2; mask9.Value = 1;
            }

            if (standardMaska.SelectedIndex == 8)
            {
                mask1.Value = -1; mask2.Value = -1; mask3.Value = -1;
                mask4.Value = -1; mask5.Value = 9; mask6.Value = -1;
                mask7.Value = -1; mask8.Value = -1; mask9.Value = -1;
            }

            if (standardMaska.SelectedIndex == 9)
            {
                mask1.Value = 0; mask2.Value = -1; mask3.Value = 0;
                mask4.Value = -1; mask5.Value = 5; mask6.Value = -1;
                mask7.Value = 0; mask8.Value = -1; mask9.Value = 0;
            }
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            value1 = mask1.Value;
            value2 = mask2.Value;
            value3 = mask3.Value;
            value4 = mask4.Value;
            value5 = mask5.Value;
            value6 = mask6.Value;
            value7 = mask7.Value;
            value8 = mask8.Value;
            value9 = mask9.Value;
            buttonExecute.DialogResult = DialogResult.OK;

        }
    }
}
