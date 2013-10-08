using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSF_Server_A3.Classes;
using System.Windows.Forms;
using System.Xml;

namespace FSF_Server_A3.Classes
{
    class TabPriority
    {

        // GESTION TABS PRIORITE


        static public void genereTabPriorite(string profil)
        {
            Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Clear();
            if (System.IO.File.Exists(Var.RepertoireDeSauvegarde + profil + ".profilPriorite.xml"))
            {
                XmlTextReader fichierProfilPrioriteXML = new XmlTextReader(Var.RepertoireDeSauvegarde + profil + ".profilPriorite.xml");
                while (fichierProfilPrioriteXML.Read())
                {
                    fichierProfilPrioriteXML.ReadToFollowing("MODS");
                    string ligne = fichierProfilPrioriteXML.ReadString();
                    if (ligne != "")
                    {
                        Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Add(ligne);
                    }
                }
                fichierProfilPrioriteXML.Close();
            }
            else
            {
                TabPriority.actualisePrioriteMods();
                GestionProfil.SauvegardeConfigProfilXML("");
            };

        }
        static public void actualisePrioriteMods()
        {           // recupere tous les Mods coché dans une liste
            // Compare Liste Mods avec Liste Tab prioritaire


            // Efface ceux qui ne sont plus selectionné
            // Ajoute ceux qui manque en fin de liste

            // Affiche la liste par priorité dans la listeBox
            foreach (string ligne in compareListeModsValidesEtListePrioritaire(ListeModsValide(), ListeModsPrioritaire()))
            {
                Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Add(ligne);
            }

        }
        static private List<string> compareListeModsValidesEtListePrioritaire(List<string> listModsValide, List<string> listModsPrioritaire)
        {

            // efface de la liste prioritaire les mods non valide
            List<string> Intersection = listModsPrioritaire.Intersect(listModsValide).ToList();
            List<string> Ajout = listModsValide.Except(listModsPrioritaire).ToList();


            // efface le listCheckBox priorité
            Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Clear();
            return Intersection.Union(Ajout).ToList();
        }
        static private List<string> ListeModsPrioritaire()
        {
            List<string> listeModsPrioritaire = new List<string>();
            int compteur = 0;
            foreach (string lignes in Var.fenetrePrincipale.ctrlListModPrioritaire.Items)
            {
                listeModsPrioritaire.Add(lignes);
                compteur++;
            }
            return listeModsPrioritaire;
        }
        static private List<string> ListeModsValide()
        {
            List<string> listeModsValide = new List<string>();
            // recupere tous les Mods coché dans une seule liste
            // Template
            foreach (string ligne in ExtractionListeModsValides(Var.fenetrePrincipale.checkedListBox7, @"@FSF\@TEMPLATE\"))
            {
                listeModsValide.Add(ligne);
                if (ligne == @"@FSF\@TEMPLATE\@FSFUnits_Cfg")
                {
                    if (Var.fenetrePrincipale.comboBox2.Text != "")
                    {
                        listeModsValide.Add(@"@FSF\@TEMPLATE\@FSFSkin_" + Var.fenetrePrincipale.comboBox2.Text);
                    }
                }
            }
            // Casque Perso
            if (Var.fenetrePrincipale.radioButton20.Checked) { listeModsValide.Add(@"@FSF\@TEMPLATE\@FSFUnit_HelmetsST"); };
            if (Var.fenetrePrincipale.radioButton21.Checked) { listeModsValide.Add(@"@FSF\@TEMPLATE\@FSFUnit_HelmetsXT"); };

            // FRAMEWORK
            foreach (string ligne in ExtractionListeModsValides(Var.fenetrePrincipale.checkedListBox8, @"@FSF\@FRAMEWORK\"))
            {
                listeModsValide.Add(ligne);
            }

            // Islands
            foreach (string ligne in ExtractionListeModsValides(Var.fenetrePrincipale.checkedListBox1, @"@FSF\@ISLANDS\"))
            {
                listeModsValide.Add(ligne);
            }
            // Units
            foreach (string ligne in ExtractionListeModsValides(Var.fenetrePrincipale.checkedListBox2, @"@FSF\@UNITS\"))
            {
                listeModsValide.Add(ligne);
            }
            // Materiel
            foreach (string ligne in ExtractionListeModsValides(Var.fenetrePrincipale.checkedListBox3, @"@FSF\@MATERIEL\"))
            {
                listeModsValide.Add(ligne);
            }
            // Client
            foreach (string ligne in ExtractionListeModsValides(Var.fenetrePrincipale.checkedListBox6, @"@FSF\@CLIENT\"))
            {
                listeModsValide.Add(ligne);
            }
            // test
            foreach (string ligne in ExtractionListeModsValides(Var.fenetrePrincipale.checkedListBox4, @"@FSF\@TEST\"))
            {
                listeModsValide.Add(ligne);
            }
            // Autres MODS
            foreach (string ligne in ExtractionListeModsValides(Var.fenetrePrincipale.checkedListBox5, ""))
            {
                listeModsValide.Add(ligne);
            }

            return listeModsValide;
        }
        static private List<string> ExtractionListeModsValides(CheckedListBox ListBox, string cheminModsFSF)
        {
            List<string> listeModsValide = new List<string>();
            int compteur = 0;
            foreach (string lignes in ListBox.Items)
            {
                if (ListBox.GetItemChecked(compteur)) { listeModsValide.Add(cheminModsFSF + lignes); }
                compteur++;
            }
            return listeModsValide;

        }


        /*
         *  CONTROL BOUTONS Du FORM
         */
        static public void topPrioriteMod()
        {
            if (Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                int index;
                string valeur;
                while (Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex > 0)
                {
                    valeur = Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    index = Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    Var.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index - 1, valeur);
                    Var.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index - 1, true);
                }
            }
        }
        static public void downPrioriteMod()
        {
            if (Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                while (Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex < Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Count - 1)
                {
                    string valeur = Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    int index = Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    Var.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index + 1, valeur);
                    Var.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index + 1, true);
                }
            }
        }
        static public void augmentePrioriteMod()
        {
            if (Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                string valeur;
                int index;
                if (Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex > 0)
                {
                    valeur = Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    index = Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    Var.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index - 1, valeur);
                    Var.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index - 1, true);
                }
            }
        }
        static public void diminuePrioriteMod()
        {
            if (Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex.ToString() != "-1")
            {
                if (Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex < Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Count - 1)
                {
                    string valeur = Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedItem.ToString();
                    int index = Var.fenetrePrincipale.ctrlListModPrioritaire.SelectedIndex;
                    Var.fenetrePrincipale.ctrlListModPrioritaire.Items.RemoveAt(index);
                    Var.fenetrePrincipale.ctrlListModPrioritaire.Items.Insert(index + 1, valeur);
                    Var.fenetrePrincipale.ctrlListModPrioritaire.SetSelected(index + 1, true);
                }
            }
        }
    }
}

