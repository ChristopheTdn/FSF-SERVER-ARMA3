﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FSF_Server_A3
{
    public partial class DIAL_Unlock : Form
    {
        public DIAL_Unlock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FSF_Server_A3.Classes.Core.SetKeyValue(@"Software\Clan FSF\FSF Server A3\", "UnlockPass", textBox2.Text);
            this.Close();
        }

        private void DIAL_Unlock_Load(object sender, EventArgs e)
        {

        }
        
    }
}
