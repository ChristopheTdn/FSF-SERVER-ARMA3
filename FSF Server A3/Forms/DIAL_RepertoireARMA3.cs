using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSF_Server_A3.Forms
{
    public partial class DIAL_RepertoireARMA3 : Form
    {
        public DIAL_RepertoireARMA3()
        {
            InitializeComponent();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog CheminA3Server = new FolderBrowserDialog();
            CheminA3Server.SelectedPath = "";
            CheminA3Server.ShowDialog();
            textBox88.Text = CheminA3Server.SelectedPath;
        }

        private void DIAL_RepertoireARMA3_Load(object sender, EventArgs e)
        {

        }
    }
}
