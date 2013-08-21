using FSF_Server_A3.Classes;
using System;
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
    public partial class DIAL_LangageSelect : Form
    {
        public DIAL_LangageSelect()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Language.ChangeLangage("fr-fr");
            };
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                Language.ChangeLangage("en-us");
            };
        }

        private void DIAL_LangageSelect_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }

    }
}
