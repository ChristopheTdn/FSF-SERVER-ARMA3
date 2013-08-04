using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Reflection;


namespace FSF_Server_A3.Classes
{
    class SurveillanceProcess
    {


        public SurveillanceProcess(string ligneCmd, string param)
        {
            Process myProcess = new Process();
            myProcess.EnableRaisingEvents = true;
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.Exited += new EventHandler(ProcessExited);

            if (Var.fenetrePrincipale.checkBox24.Checked)
            {
                myProcess.StartInfo.Verb = "runas";
            }

            myProcess.StartInfo.FileName = ligneCmd;
            myProcess.StartInfo.Arguments = param;
            myProcess.Start();
        }

        public void ProcessExited(object sender, EventArgs e)
        {
            // Invocation d'une méthode du thread graphique
            Var.fenetrePrincipale.Invoke(new MethodInvoker(ProcessDisabled));
        }

        static public void ProcessDisabled()
        {
            Var.fenetrePrincipale.button16.Enabled = true;
        }

    }
}


