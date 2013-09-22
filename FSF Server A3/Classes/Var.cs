using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSF_Server_A3.Classes;
using System.Diagnostics;
using System.Xml;

namespace FSF_Server_A3.Classes
{
    class Var
    {        
        static public FenetrePrincipale fenetrePrincipale;
        static public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        static public System.Windows.Forms.Timer timerSynchro = new System.Windows.Forms.Timer();
        static public string RepertoireDeSauvegarde = DefRepertoireDeSauvegarde();
        static public string RepertoireApplication = AppDomain.CurrentDomain.BaseDirectory;
        static public string VersionArma3Exe()
        {
            try
            {
                FileVersionInfo version = FileVersionInfo.GetVersionInfo(Var.fenetrePrincipale.textBox18.Text + @"\arma3.exe");
                return version.ProductVersion;
            }
            catch
            {
                return "null";
            }
        }
        static public string VersionProgramme()
        {
            string versionProg = "";
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Version version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                versionProg = "v. " + version.Major + "." + version.Minor + "." + version.Build + " (Rev. " + version.Revision + ")";
            }
            return versionProg;

        }

        static public string VersionSynchro()
        {
            Var.fenetrePrincipale.pictureBox6.Visible = false;
            string VersionSynchro;
            try
            {
                XmlTextReader fichierInfoServer = new XmlTextReader(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\version.xml");
                fichierInfoServer.ReadToFollowing("VERSION");
                VersionSynchro = fichierInfoServer.ReadString();
                fichierInfoServer.Close();

            }
            catch
            {
                return "null";
            }
            VersionSynchroCompare(VersionSynchro);
            return VersionSynchro;
        }
        static public void VersionSynchroCompare(string versionSynchroLocale)
        {
            if (Core.IsFSFInterface())
            {
                string VersionSynchroEnLigne;
                try
                {

                    string link = @"ftp://" + Var.fenetrePrincipale.textBox8.Text + ":" + Var.fenetrePrincipale.textBox7.Text + @"@" + Var.fenetrePrincipale.textBox9.Text + @"/"+Var.fenetrePrincipale.textBox19.Text+ @"/version.xml";
                    XmlTextReader fichierInfoServer = new XmlTextReader(link);
                    fichierInfoServer.ReadToFollowing("VERSION");
                    VersionSynchroEnLigne = fichierInfoServer.ReadString();
                    fichierInfoServer.Close();
                }
                catch
                {
                    VersionSynchroEnLigne = "null";
                    Var.fenetrePrincipale.pictureBox6.Visible = false;
                }
                if (versionSynchroLocale != VersionSynchroEnLigne)
                {
                    Var.fenetrePrincipale.label77.ForeColor = System.Drawing.Color.Red;
                    Var.fenetrePrincipale.pictureBox6.Visible = true;
                    Var.fenetrePrincipale.pictureBox6.Image = FSF_Server_A3.Properties.Resources.balle_fermer_cute_stop_icone_4372_64;

                }
                else
                {
                    Var.fenetrePrincipale.label77.ForeColor = System.Drawing.Color.Black;
                    Var.fenetrePrincipale.pictureBox6.Visible = true;
                    Var.fenetrePrincipale.pictureBox6.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
                }
            }

        }
           
 

        // DEF
        static private string DefRepertoireDeSauvegarde()
        {
            if (RepertoireDeSauvegarde != null) { return RepertoireDeSauvegarde; }
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + @"\FSFServerA3\"; 
        }

        static public int IndexProfilParDefaut()
        {
                int index = 0;
                string profilFavoris = Core.GetKeyValue(@"Software\Clan FSF\FSF Server A3\", "profil_favoris");
                if (profilFavoris != "00")
                {
                    int compteur = 0;
                    foreach (ComboboxItem profil in Var.fenetrePrincipale.comboBox4.Items)
                    {
                        if (profil.Text.ToString() == profilFavoris) { index = compteur; };
                        compteur++;
                    }
                }
                return index;            
        }

        
    }
}
