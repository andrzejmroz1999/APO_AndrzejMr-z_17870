
namespace APO_AndrzejMróz_17870
{
    partial class PrewittForm
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
            this.checkedListPrewitt = new System.Windows.Forms.CheckedListBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListPrewitt
            // 
            this.checkedListPrewitt.BackColor = System.Drawing.SystemColors.Menu;
            this.checkedListPrewitt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.checkedListPrewitt.FormattingEnabled = true;
            this.checkedListPrewitt.Location = new System.Drawing.Point(12, 12);
            this.checkedListPrewitt.Name = "checkedListPrewitt";
            this.checkedListPrewitt.Size = new System.Drawing.Size(192, 175);
            this.checkedListPrewitt.TabIndex = 32;
            this.checkedListPrewitt.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListPrewitt_ItemCheck);
            // 
            // buttonExecute
            // 
            this.buttonExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonExecute.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonExecute.Location = new System.Drawing.Point(12, 203);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.buttonExecute.Size = new System.Drawing.Size(157, 35);
            this.buttonExecute.TabIndex = 31;
            this.buttonExecute.Text = "WYKONAJ";
            this.buttonExecute.UseVisualStyleBackColor = false;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // PrewittForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 250);
            this.Controls.Add(this.checkedListPrewitt);
            this.Controls.Add(this.buttonExecute);
            this.Name = "PrewittForm";
            this.Text = "PrewittForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckedListBox checkedListPrewitt;
        private System.Windows.Forms.Button buttonExecute;
    }
}