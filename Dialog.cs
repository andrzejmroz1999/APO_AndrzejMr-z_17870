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
        public int combovalue1;
        public int combovalue2;

       

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
            if ( trackBar2.Visible == true && trackBar1.Visible == true)
            {
                try
                {
                    combovalue1 = trackBar1.Value;
                    combovalue2 = trackBar2.Value;
                }
                catch (Exception error) { }
            }
           

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
    }
}
