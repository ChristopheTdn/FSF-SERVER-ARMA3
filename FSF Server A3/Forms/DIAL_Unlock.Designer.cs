namespace FSF_Server_A3
{
    partial class DIAL_Unlock
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(248, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 44;
            this.button1.Text = "Unlock";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox2.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold);
            this.textBox2.Location = new System.Drawing.Point(194, 113);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(190, 40);
            this.textBox2.TabIndex = 43;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Bold);
            this.label22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label22.Location = new System.Drawing.Point(206, 75);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(178, 21);
            this.label22.TabIndex = 42;
            this.label22.Text = "Unlock FSF Synchronisation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 15.75F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(221, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 37);
            this.label2.TabIndex = 41;
            this.label2.Text = "FSF Server";
            // 
            // pictureBox13
            // 
            this.pictureBox13.ErrorImage = null;
            this.pictureBox13.Image = global::FSF_Server_A3.Properties.Resources.logo_fsf;
            this.pictureBox13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox13.InitialImage = null;
            this.pictureBox13.Location = new System.Drawing.Point(39, 38);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(120, 115);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox13.TabIndex = 40;
            this.pictureBox13.TabStop = false;
            // 
            // DIAL_Unlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 218);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DIAL_Unlock";
            this.Text = "FSF Unlock Synchro";
            this.Load += new System.EventHandler(this.DIAL_Unlock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox13;
    }
}