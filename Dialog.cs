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
    public partial class Dialog : Form
    {
        public String value;
        public String value2;
        public int combovalue;
        public int combovalue1;
        public int combovalue2;
        public int combovalue3;
        public int combovalue4;



        private class Item
        {
            public string Name;
            public int Value;
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {

                return Name;
            }
        };

        public Dialog(String title, String label)
        {
            InitializeComponent();
            Text = title;
            labelDesc.Text = label;


            if (title == "Progowanie")
            {
                trackBar1.Visible = true;

            }

            if (title == "ProgowanieLevel")
            {
                trackBar1.Visible = true;
                trackBar2.Visible = true;
            }
            trackBar1.Minimum = 0;
            trackBar2.Minimum = 0;

            trackBar1.Maximum = 255;
            trackBar2.Maximum = 255;

            trackBar1.Value = 0;
            trackBar2.Value = 255;

            textBox1.Text = Convert.ToString(trackBar1.Value);
            textBox2.Text = Convert.ToString(trackBar2.Value);

            if (title == "Rozciaganie" )
            {
                trackBar1.Visible = true;
                trackBar2.Visible = true;
            }

            if (title == "Redukcja")
            {
                trackBar1.Visible = true;





                textBox1.Text = Convert.ToString(trackBar1.Value);

                textBox1.Visible = true;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;

            }
            if (title == "Operacja logiczna AND" || title == "Operacja logiczna OR" || title == "Operacja logiczna XOR" || title == "Odejmowanie" || title == "Dodawanie")
            {
                textBox1.Visible = true;
                comboBox1.Visible = true;
                textBox1.Visible = false;
                textBox2.Visible = false;
            }

        }

        public Dialog(String title, String label, Form[] forms)
        {
            InitializeComponent();
            Text = title;
            labelDesc.Text = label;
            textBox.Visible = false;
            comboBox1.Visible = true;
            int i = 0;
            foreach (Form form in forms)
            {
                comboBox1.Items.Add(new Item(form.Text, i++));
            }


        }

        public Dialog(String title, String label, String label2)
        {
            InitializeComponent();
            Text = title;
            labelDesc.Text = label;
            labelDesc2.Text = label2;
            labelDesc2.Visible = true;
            textBox2.Visible = true;
        }

        private void myAcceptButton_Click(object sender, EventArgs e)
        {
            value = textBox.Text;
            value2 = textBox2.Text;
            //  if ( trackBar2.Visible == true && trackBar1.Visible == true)
            //   {
            try
            {
                combovalue1 = trackBar1.Value;
                combovalue2 = trackBar2.Value;
                combovalue3 = trackBar3.Value;
                combovalue4 = trackBar4.Value;
            }
            catch (Exception error) { }

            //    }


            myAcceptButton.DialogResult = DialogResult.OK;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = trackBar2.Value.ToString();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Maximum = trackBar2.Value;
            trackBar2.Minimum = trackBar1.Value;

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Maximum = trackBar2.Value;
            trackBar2.Minimum = trackBar1.Value;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            trackBar1.Value = int.Parse(textBox1.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            trackBar2.Value = int.Parse(textBox3.Text);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox4.Text = trackBar3.Value.ToString();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            textBox5.Text = trackBar4.Value.ToString();
        }
    }
}
