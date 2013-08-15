namespace FSF_Server_A3.Forms
{
    partial class DIAL_RepertoireARMA3
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
            this.label1 = new System.Windows.Forms.Label();
            this.button37 = new System.Windows.Forms.Button();
            this.textBox88 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Le programme ne parvient pas a localise rle fichier arma3serveur.exe.\r\n\r\nVeillez " +
    "indiquer son emplacement pour pouvoir poursuivre le chargement\r\ndu profil.";
            // 
            // button37
            // 
            this.button37.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button37.Location = new System.Drawing.Point(391, 131);
            this.button37.Name = "button37";
            this.button37.Size = new System.Drawing.Size(28, 20);
            this.button37.TabIndex = 51;
            this.button37.Text = "...";
            this.button37.UseVisualStyleBackColor = true;
            this.button37.Click += new System.EventHandler(this.button37_Click);
            // 
            // textBox88
            // 
            this.textBox88.Location = new System.Drawing.Point(186, 129);
            this.textBox88.Name = "textBox88";
            this.textBox88.Size = new System.Drawing.Size(198, 20);
            this.textBox88.TabIndex = 50;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label24.Location = new System.Drawing.Point(18, 132);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(0, 13);
            this.label24.TabIndex = 49;
            // 
            // DIAL_RepertoireARMA3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 194);
            this.Controls.Add(this.button37);
            this.Controls.Add(this.textBox88);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label1);
            this.Name = "DIAL_RepertoireARMA3";
            this.Text = "arma3server.exe ?";
            this.Load += new System.EventHandler(this.DIAL_RepertoireARMA3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button button37;
        internal System.Windows.Forms.TextBox textBox88;
        internal System.Windows.Forms.Label label24;

    }
}