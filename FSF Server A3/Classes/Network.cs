using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO.Compression;

namespace FSF_Server_A3.Classes
{
    class Network
    {
        public static void sauvegardeVersionA3(string profil)
        {
            if (CheckConfigExportVersionValid())
            {
                if (!System.IO.File.Exists(Var.RepertoireDeSauvegarde + profil + ".infoServeur.xml"))
                {
                    Directory.CreateDirectory(Var.RepertoireDeSauvegarde);
                    FileStream fs = File.Create(Var.RepertoireDeSauvegarde + profil + "." + Var.fenetrePrincipale.textBox10.Text + ".infoServeur.xml");
                    fs.Close();
                }

                XmlTextWriter FichierProfilXML = new XmlTextWriter(Var.RepertoireDeSauvegarde + profil + "." + Var.fenetrePrincipale.textBox10.Text + ".infoServeur.xml", System.Text.Encoding.UTF8);
                FichierProfilXML.Formatting = Formatting.Indented;
                FichierProfilXML.WriteStartDocument();
                FichierProfilXML.WriteComment("Information du Serveur"); // commentaire
                FichierProfilXML.WriteStartElement("SERVER");
                FichierProfilXML.WriteElementString("VERSION", Var.VersionArma3Exe());
                FichierProfilXML.WriteEndElement();
                FichierProfilXML.Flush(); //vide le buffer
                FichierProfilXML.Close(); // ferme le document
                UploadConfigServeur(Var.RepertoireDeSauvegarde + profil + "." + Var.fenetrePrincipale.textBox10.Text + ".infoServeur.xml", @"ftp://" + Var.fenetrePrincipale.textBox2.Text + @"/" + Var.fenetrePrincipale.textBox10.Text, Var.fenetrePrincipale.textBox3.Text, Var.fenetrePrincipale.textBox4.Text);
            }
        }
        public static void UploadConfigServeur(string nomFichier, string repertoireFTP, string login, string pass)
        {
            try
            {
                // parametre : nom du fichier téléchargé sur le FTP, répertoire d'emplacement dans le FTP, emplacement ou sera enregistré le fichier
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(repertoireFTP);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(login, pass);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;
                FileStream stream = File.OpenRead(nomFichier);
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();
            }
            catch
            {

            }
        }
        private static bool CheckConfigExportVersionValid()
        {
            if (Var.fenetrePrincipale.textBox2.Text=="") return false;
            return true;
        }

        // Gestion Serveur distant LINUX
        public static void demarreServeurDistant()
        {
            GenerationPackfichierconfig((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
        }
        private static void GenerationPackfichierconfig(string profil)
        {
                string repSourceFichierServeur = Var.fenetrePrincipale.textBox18.Text + @"\@FSFServer\" + profil + @"\";
                GestionProfil.GenerefichierServeur(profil);
                // Creation repertoire dédié Linux
                if (!Directory.Exists(Var.RepertoireDeSauvegarde + @"linuxCFG\serverFSF\profile\Users\server"))
                {
                    Directory.CreateDirectory(Var.RepertoireDeSauvegarde + @"linuxCFG\serverFSF\profile\Users\server");
                };
                if (File.Exists(Var.RepertoireDeSauvegarde +"server.zip"))
                {
                    File.Delete(Var.RepertoireDeSauvegarde + "server.zip");
                };

            // copie fichier requis
                File.Copy(repSourceFichierServeur + "basic.cfg", Var.RepertoireDeSauvegarde + @"linuxCFG\serverFSF\basic.cfg", true);
                File.Copy(repSourceFichierServeur + "server.cfg", Var.RepertoireDeSauvegarde + @"linuxCFG\serverFSF\server.cfg", true);
                File.Copy(repSourceFichierServeur + @"profile\Users\server\server.Arma3Profile", Var.RepertoireDeSauvegarde + @"linuxCFG\serverFSF\profile\Users\server\server.Arma3Profile", true);
            // Zip Archive fichier Serveur
                string RepertoireSource = Var.RepertoireDeSauvegarde + @"linuxCFG";

            string zipPath = Var.RepertoireDeSauvegarde +"server.zip";
            ZipFile.CreateFromDirectory(RepertoireSource, zipPath);
        }
    }
}
  