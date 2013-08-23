using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSF_Server_A3.Classes;
using System.Diagnostics;

namespace FSF_Server_A3.Classes
{
    class Var
    {        
        static public FenetrePrincipale fenetrePrincipale;
        static public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        static public string RepertoireDeSauvegarde = DefRepertoireDeSauvegarde();
        static public string RepertoireApplication = AppDomain.CurrentDomain.BaseDirectory;
        static public string VersionArma3Exe()
        {
            try
            {
                FileVersionInfo version = FileVersionInfo.GetVersionInfo(Var.fenetrePrincipale.textBox18.Text + @"\arma3.exe");
                return version.FileMajorPart + "." + version.FileMinorPart;
            }
            catch
            {
                return "null";
            }
        }   

        // DEF
        static private string DefRepertoireDeSauvegarde()
        {
            if (RepertoireDeSauvegarde != null) { return RepertoireDeSauvegarde; }
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + @"\FSFServerA3\"; 
        }
 

        
    }
}
