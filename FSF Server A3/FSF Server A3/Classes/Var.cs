using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSF_Server_A3.Classes;

namespace FSF_Server_A3.Classes
{
    class Var
    {        
        static public FenetrePrincipale fenetrePrincipale;
        static public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        static public string RepertoireDeSauvegarde = DefRepertoireDeSauvegarde();
        static public string RepertoireApplication = AppDomain.CurrentDomain.BaseDirectory;
      

        // DEF
        static private string DefRepertoireDeSauvegarde()
        {
            if (RepertoireDeSauvegarde != null) { return RepertoireDeSauvegarde; }
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + @"\FSFServerA3\"; 
        }

        
    }
}
