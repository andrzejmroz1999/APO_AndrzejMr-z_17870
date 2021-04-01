
namespace APO_AndrzejMróz_17870
{
    partial class Dialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.labelDesc2 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.myCancelButton = new System.Windows.Forms.Button();
            this.myAcceptButton = new System.Windows.Forms.Button();
            this.labelDesc = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(11, 54);
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(224, 45);
            this.trackBar2.TabIndex = 17;
            this.trackBar2.Visible = false;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            this.trackBar2.ValueChanged += new System.EventHandler(this.trackBar2_ValueChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(11, 26);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(224, 45);
            this.trackBar1.TabIndex = 16;
            this.trackBar1.Visible = false;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(11, 26);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(198, 21);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 70);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(197, 20);
            this.textBox2.TabIndex = 14;
            this.textBox2.Visible = false;
            // 
            // labelDesc2
            // 
            this.labelDesc2.AutoSize = true;
            this.labelDesc2.Location = new System.Drawing.Point(12, 53);
            this.labelDesc2.Name = "labelDesc2";
            this.labelDesc2.Size = new System.Drawing.Size(58, 13);
            this.labelDesc2.TabIndex = 13;
            this.labelDesc2.Text = "description";
            this.labelDesc2.Visible = false;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 26);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(197, 20);
            this.textBox.TabIndex = 12;
            // 
            // myCancelButton
            // 
            this.myCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.myCancelButton.Location = new System.Drawing.Point(134, 101);
            this.myCancelButton.Name = "myCancelButton";
            this.myCancelButton.Size = new System.Drawing.Size(75, 23);
            this.myCancelButton.TabIndex = 11;
            this.myCancelButton.Text = "Anuluj";
            this.myCancelButton.UseVisualStyleBackColor = true;
            // 
            // myAcceptButton
            // 
            this.myAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.myAcceptButton.Location = new System.Drawing.Point(31, 101);
            this.myAcceptButton.Name = "myAcceptButton";
            this.myAcceptButton.Size = new System.Drawing.Size(75, 23);
            this.myAcceptButton.TabIndex = 10;
            this.myAcceptButton.Text = "OK";
            this.myAcceptButton.UseVisualStyleBackColor = true;
            this.myAcceptButton.Click += new System.EventHandler(this.myAcceptButton_Click);
            // 
            // labelDesc
            // 
            this.labelDesc.AutoSize = true;
            this.labelDesc.Location = new System.Drawing.Point(12, 9);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(58, 13);
            this.labelDesc.TabIndex = 9;
            this.labelDesc.Text = "description";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(261, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(28, 20);
            this.textBox1.TabIndex = 18;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(261, 54);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(28, 20);
            this.textBox3.TabIndex = 19;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 147);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelDesc2);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.myCancelButton);
            this.Controls.Add(this.myAcceptButton);
            this.Controls.Add(this.labelDesc);
            this.Name = "Dialog";
            this.Text = "Dialog";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label labelDesc2;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button myCancelButton;
        private System.Windows.Forms.Button myAcceptButton;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
    }
}