using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSF_Server_A3.Classes;
using System.Xml;

namespace FSF_Server_A3.Classes
{
    class TabMissions
    {
        // GESTION TABS MISSIONS
        static public void genereTabMissions(string profil)
        {
            Var.fenetrePrincipale.checkedListBoxMissions.Items.Clear();
            List<string> listIle = new List<string>();
            List<string> Intersection = ListeMissionsProfil(profil).Intersect(ListeMissionsSurDisque(profil)).ToList();
            List<string> Ajout = ListeMissionsSurDisque(profil).Except(ListeMissionsProfil(profil)).ToList();
            foreach (var ligne in Intersection)
            {
                Var.fenetrePrincipale.checkedListBoxMissions.Items.Add(ligne,true);
                // recupere le nom de l ile                {
                string[] Tstr = ligne.Split('.');
                string nomIle = Tstr[Tstr.Length - 2];
                if (!listIle.Contains(nomIle, StringComparer.OrdinalIgnoreCase))
                {
                    listIle.Add(nomIle);
                }
            }
            foreach (var ligne in Ajout)
            {
                Var.fenetrePrincipale.checkedListBoxMissions.Items.Add(ligne, false);
                // recupere le nom de l ile                {
                string[] Tstr = ligne.Split('.');
                string nomIle = Tstr[Tstr.Length - 2];
                if (!listIle.Contains(nomIle, StringComparer.OrdinalIgnoreCase))
                {
                    listIle.Add(nomIle);
                }
            }
            // Initialise le bouton de Selection d'Iles
            Var.fenetrePrincipale.comboBox5.Items.Clear();
            listIle.Sort();
            listIle.Insert(0, "Toutes");

            foreach (var nomile in listIle)
            {
                Var.fenetrePrincipale.comboBox5.Items.Add(nomile);
            }
            Var.fenetrePrincipale.comboBox5.SelectedIndex = 0;
            Var.fenetrePrincipale.comboBox6.SelectedIndex = 0;

        }
        static private List<string> ListeMissionsSurDisque(string profil)
        {
            List<string> listeFichier = new List<string>();
            try
            {
                string[] tableauFichier = Directory.GetFiles(Var.fenetrePrincipale.textBox18.Text + @"\MPMissions\", "*.pbo");
                foreach (var ligne in tableauFichier)
                {
                    listeFichier.Add(ligne.Replace(Var.fenetrePrincipale.textBox18.Text + @"\MPMissions\", ""));
                }
            }
            catch
            {
            }
            return listeFichier;
        }
        static private List<string> ListeMissionsProfil(string profil)
        {
            List<string> listeMissionsChecked = new List<string>();
            try
            {
                if (System.IO.File.Exists(Var.RepertoireDeSauvegarde + profil + ".profilMissions.xml"))
                {
                    XmlTextReader fichierProfilMissionsXML = new XmlTextReader(Var.RepertoireDeSauvegarde + profil + ".profilMissions.xml");
                    while (fichierProfilMissionsXML.Read())
                    {
                        fichierProfilMissionsXML.ReadToFollowing("FICHIER");
                        string ligne = fichierProfilMissionsXML.ReadString();
                        if (ligne != "")
                        {
                            listeMissionsChecked.Add(ligne);
                        }
                    }
                    fichierProfilMissionsXML.Close();
                }

            }
            catch {
            }
            return listeMissionsChecked;
        }

        static public void changeFiltreMissions(string Profil)
        {
            //Var.fenetrePrincipale.checkedListBoxMissions.Items.Clear();
            List<string> listIle = new List<string>();
            /*
            foreach (string ligne in GenereListeTabMission(Profil))
            {
                if (ligne.Contains(Var.fenetrePrincipale.comboBox5.SelectedItem.ToString()) || Var.fenetrePrincipale.comboBox5.SelectedIndex == 0)
                {
                    Var.fenetrePrincipale.checkedListBoxMissions.Items.Add(ligne);
                }

            }
             * */
        }
        static public void actualiseMissions()
        {

        }
    }
}
