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

    public partial class PrewittForm : Form
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
        public PrewittForm()
        {
            InitializeComponent();

           
            checkedListPrewitt.Items.Add("N");
            checkedListPrewitt.Items.Add("NE");
            checkedListPrewitt.Items.Add("E");
            checkedListPrewitt.Items.Add("SE");
            checkedListPrewitt.Items.Add("S");
            checkedListPrewitt.Items.Add("SW");
            checkedListPrewitt.Items.Add("W");
            checkedListPrewitt.Items.Add("NW");
        }




        private void checkedListPrewitt_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListPrewitt.Items.Count; ++ix)
            {
                if (ix != e.Index) checkedListPrewitt.SetItemChecked(ix, false);
            }
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            int mask1 = 0, mask2 = 0, mask3 = 0, mask4 = 0, mask5 = 0, mask6 = 0, mask7 = 0, mask8 = 0, mask9 = 0;
            if (checkedListPrewitt.SelectedIndex == 0)
            {
                mask1 = 3; mask2 = 3; mask3 = 3;
                mask4 = 3; mask5 = 0; mask6 = 3;
                mask7 = -5; mask8 = -5; mask9 = -5;
            }

            // NE
            if (checkedListPrewitt.SelectedIndex == 1)
            {
                mask1 = 3; mask2 = 3; mask3 = 3;
                mask4 = -5; mask5 = 0; mask6 = 3;
                mask7 = -5; mask8 = -5; mask9 = 3;
            }

            // E
            if (checkedListPrewitt.SelectedIndex == 2)
            {
                mask1 = -1; mask2 = 1; mask3 = 1;
                mask4 = -1; mask5 = -2; mask6 = 1;
                mask7 = -1; mask8 = 1; mask9 = 1;
            }

            // SE
            if (checkedListPrewitt.SelectedIndex == 3)
            {
                mask1 = -1; mask2 = -1; mask3 = 1;
                mask4 = -1; mask5 = -2; mask6 = 1;
                mask7 = 1; mask8 = 1; mask9 = 1;
            }

            // S
            if (checkedListPrewitt.SelectedIndex == 4)
            {
                mask1 = -1; mask2 = -1; mask3 = -1;
                mask4 = 1; mask5 = -2; mask6 = 1;
                mask7 = 1; mask8 = 1; mask9 = 1;
            }

            // SW
            if (checkedListPrewitt.SelectedIndex == 5)
            {
                mask1 = 1; mask2 = -1; mask3 = -1;
                mask4 = 1; mask5 = -2; mask6 = -1;
                mask7 = 1; mask8 = 1; mask9 = 1;
            }

            // W
            if (checkedListPrewitt.SelectedIndex == 6)
            {
                mask1 = 1; mask2 = 1; mask3 = -1;
                mask4 = 1; mask5 = -2; mask6 = -1;
                mask7 = 1; mask8 = 1; mask9 = -1;
            }

            // NW
            if (checkedListPrewitt.SelectedIndex == 7)
            {
                mask1 = 1; mask2 = 1; mask3 = 1;
                mask4 = 1; mask5 = -2; mask6 = -1;
                mask7 = 1; mask8 = -1; mask9 = -1;
            }
            value1 = mask1;
            value2 = mask2;
            value3 = mask3;
            value4 = mask4;
            value5 = mask5;
            value6 = mask6;
            value7 = mask7;
            value8 = mask8;
            value9 = mask9;
            buttonExecute.DialogResult = DialogResult.OK;
        }
    }
}

