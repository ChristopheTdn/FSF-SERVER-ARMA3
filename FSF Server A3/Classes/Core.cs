using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSF_Server_A3.Classes;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Diagnostics;

namespace FSF_Server_A3.Classes
{

    class Core
    {
        static public void InitialiseCore()
        {
            if (!Directory.Exists(Var.RepertoireDeSauvegarde))
            {
               // le repertoire n'existe pas
                GestionProfil.CreationNouveauProfil("default");
            }
            
           GestionProfil.InitialiseListeProfil();
           Var.fenetrePrincipale.comboBox4.SelectedIndex = Var.IndexProfilParDefaut();

           Interface.dessineInterface((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());

            // Timer test mise a jour synchro si Synchro FSF
           if (IsFSFInterface())
           {
               Var.timerSynchro.Tick += new EventHandler(TimerSynchroEvent);
               Var.timerSynchro.Interval = 120000;
               Var.timerSynchro.Start();
           }
        }

        private static void TimerSynchroEvent(Object myObject, EventArgs myEventArgs)
        {
            Var.timerSynchro.Stop();
            Var.fenetrePrincipale.label76.Text = Var.VersionArma3Exe();

            DirectoryInfo localDir = new DirectoryInfo(Var.fenetrePrincipale.textBox18.Text+@"\@FSF");
            FileInfo rsyncExe = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"rsync\rsync.exe");
            //String remoteServer = "127.0.0.1";
            String remoteServer = "server2.clan-fsf.fr";
            string remoteDir = Var.fenetrePrincipale.textBox8.Text;
            RSync.RSyncCall rSyncCall = new RSync.RSyncCall(Var.fenetrePrincipale, Var.fenetrePrincipale.button25, Var.fenetrePrincipale.textBox11, Var.fenetrePrincipale.progressBar3, Var.fenetrePrincipale.progressBar2, rsyncExe, remoteServer, remoteDir, localDir,Var.fenetrePrincipale.label77);            //new RSync.RSyncCall(fenetrePrincipale, BoutonSender, fenetrePrincipale.textBox11, fenetrePrincipale.progressBar3, fenetrePrincipale.progressBar2, rsyncExe, remoteServer, remoteDir, localDir);
            rSyncCall.setTotalSize(Var.fenetrePrincipale.label77);
            Var.timerSynchro.Start();
        }

        

        // Divers
        static public string ConversionNomFichierValide(string FileName, char RemplaceChar)
        {
            char[] InvalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char InvalidFileNameChar in InvalidFileNameChars)
                if (FileName.Contains(InvalidFileNameChar.ToString()))
                    FileName = FileName.Replace(InvalidFileNameChar, RemplaceChar);
            return FileName;
        }
        static public bool IsFSFInterface()
        {
            try
            {
                if (Encoder(GetKeyValue(@"Software\Clan FSF\FSF Server A3\", "UnlockPass")) == "43b97597d8bd45aed49b393fef1223d7")
                {
                    return true;
                }
            }
            catch {
            }
            return false;

        }
        static public string Encoder(string testpass)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(testpass);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            testpass = s.ToString();
            return testpass;
        }

        // Gestion base des registres
       

        static public string GetKeyValue(string DirName, string name)
        { // recupere clé dans la base de registre
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(DirName, true);
                return key.GetValue(name).ToString();
            }
            catch
            {
                return "00";
            } 

        }
        static public void SetKeyValue(string DirName, string name, string value)
        {
            // ecrit clé dans la base de registre
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(DirName);
            key.SetValue(name, value);
        }
       
        // Gestion Action Programme

        static public void LanceServeur(string profil)
        {
            GestionProfil.GenerefichierServeur(profil);
            Var.fenetrePrincipale.button16.Enabled = false;
            new SurveillanceProcess(Var.fenetrePrincipale.textBox18.Text + @"\@FSFServer\" + profil +@"\server.bat", "");  
        }

         // Synchronisation

       /*
*           SYNCHRONISATION      
*/
       #region Synchronisation
       static public void synchroRsync()
       {
            DirectoryInfo localDir = new DirectoryInfo(Var.fenetrePrincipale.textBox18.Text+@"\@FSF");
            FileInfo rsyncExe = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"rsync\rsync.exe");
            //String remoteServer = "127.0.0.1";
            String remoteServer = "server2.clan-fsf.fr";
            string remoteDir = Var.fenetrePrincipale.textBox8.Text;
            RSync.RSyncCall rSyncCall = new RSync.RSyncCall(Var.fenetrePrincipale, Var.fenetrePrincipale.button25, Var.fenetrePrincipale.textBox11, Var.fenetrePrincipale.progressBar3, Var.fenetrePrincipale.progressBar2, rsyncExe, remoteServer, remoteDir, localDir,Var.fenetrePrincipale.label77,Var.fenetrePrincipale.labelVitesseSynchro);            //new RSync.RSyncCall(fenetrePrincipale, BoutonSender, fenetrePrincipale.textBox11, fenetrePrincipale.progressBar3, fenetrePrincipale.progressBar2, rsyncExe, remoteServer, remoteDir, localDir);
            rSyncCall.setTotalSize(Var.fenetrePrincipale.label77);
            rSyncCall.addControlToDisable(Var.fenetrePrincipale.labelSynchroEnCoursInvisible);
            rSyncCall.start();
       }
       static public void synchroTailleRsync()
       {
           DirectoryInfo localDir = new DirectoryInfo(Var.fenetrePrincipale.textBox18.Text + @"\@FSF");
           FileInfo rsyncExe = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"rsync\rsync.exe");
           //String remoteServer = "127.0.0.1";
           String remoteServer = "server2.clan-fsf.fr";
           string remoteDir = Var.fenetrePrincipale.textBox8.Text;
           RSync.RSyncCall rSyncCall = new RSync.RSyncCall(Var.fenetrePrincipale, Var.fenetrePrincipale.button25, Var.fenetrePrincipale.textBox11, Var.fenetrePrincipale.progressBar3, Var.fenetrePrincipale.progressBar2, rsyncExe, remoteServer, remoteDir, localDir, Var.fenetrePrincipale.label77);            //new RSync.RSyncCall(fenetrePrincipale, BoutonSender, fenetrePrincipale.textBox11, fenetrePrincipale.progressBar3, fenetrePrincipale.progressBar2, rsyncExe, remoteServer, remoteDir, localDir);
           rSyncCall.setTotalSize(Var.fenetrePrincipale.label77);
       }

       static private void effaceProgressBar()
       {
           Var.fenetrePrincipale.label11.Text = "";
           Var.fenetrePrincipale.label19.Text = "";
           Var.fenetrePrincipale.progressBar2.Value = 0;
           Var.fenetrePrincipale.progressBar3.Value = 0;
       }
      
       static private bool downloadnouvelleVersion(string nomFichier, string repertoireFTP, string username, string password, string destinationRepertoire)
       {
           // parametre : nom du fichier téléchargé sur le FTP, répertoire d'emplacement dans le FTP, emplacement ou sera enregistré le fichier
           try
           {

               WebClient request = new WebClient();
               request.Credentials = new NetworkCredential(username, password);
               byte[] fileData = request.DownloadData("ftp://" + repertoireFTP + "/" + nomFichier);
               FileStream file = File.Create(destinationRepertoire + nomFichier);
               file.Write(fileData, 0, fileData.Length);
               file.Close();
               return true;
           }
           catch
           {
               //MessageBox.Show("Impossible de réaliser la mise à jour automatique du programme. Nouvel essai...\n\n"+e,"Erreur Critique",MessageBoxButtons.OK,MessageBoxIcon.Warning);
               return false;
           }
       }

       #endregion

    }


}
