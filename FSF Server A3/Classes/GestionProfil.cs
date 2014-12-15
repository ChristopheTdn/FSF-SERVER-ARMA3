using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSF_Server_A3.Classes;
using System.Xml;
using System.IO;

namespace FSF_Server_A3.Classes
{
    class GestionProfil
    {


        // GESTION Liste PROFIL


        public static void InitialiseListeProfil()
        {
            string[] listeProfil = Directory.GetFiles(Var.RepertoireDeSauvegarde, "*.profil.xml", SearchOption.TopDirectoryOnly);
            Var.fenetrePrincipale.listBox1.Items.Clear();
            Var.fenetrePrincipale.comboBox4.Items.Clear();
            int compteur = 0;
            foreach (var ligne in listeProfil)
            {
                string textCombo = ligne.Replace(Var.RepertoireDeSauvegarde, "");
                Interface.AjouteListeBoxNomProfil(compteur, textCombo.Replace(".profil.xml", ""));
                Interface.AjouteComboNomProfil(compteur, textCombo.Replace(".profil.xml", ""));
                compteur++;
            }
        }
        public static void CreationNouveauProfil(string profil)
        {
            Interface.effaceTousItemsOnglets();
            Interface.effaceTousparamsOnglet();
            GestionProfil.DefautConfig();
            SauvegardeProfil(profil);
            InitialiseListeProfil();
        }

        //SAUVEGARDE
        static public void SauvegardeProfil(string profil)
        {
            SauvegardeConfigProfilXML(profil);
            SauvegardePrioriteProfilXML(profil);
            SauvegardeProfilServer(profil);
            SauvegardeMissionsProfilXML(profil);
        }
        static public void SauvegardeConfigProfilXML(string nomProfil)
        {
            if (nomProfil == "") return;
            if (!System.IO.File.Exists(Var.RepertoireDeSauvegarde + nomProfil+".profil.xml"))
            {
                Directory.CreateDirectory(Var.RepertoireDeSauvegarde);
                FileStream fs = File.Create(Var.RepertoireDeSauvegarde + nomProfil+".profil.xml");
                fs.Close();
            }
            XmlTextWriter FichierProfilXML = new XmlTextWriter(Var.RepertoireDeSauvegarde + nomProfil + ".profil.xml", System.Text.Encoding.UTF8);
            FichierProfilXML.Formatting = Formatting.Indented;
            FichierProfilXML.WriteStartDocument();
            FichierProfilXML.WriteComment("Creation Du profil FSF SERVER " + nomProfil + ".profil.xml"); // commentaire
            FichierProfilXML.WriteStartElement("PROFIL");
            FichierProfilXML.WriteStartElement("MODS_FSF");
            //FRAMEWORK
            FichierProfilXML.WriteStartElement("FRAMEWORK");

            if (Var.fenetrePrincipale.checkedListBox8.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= Var.fenetrePrincipale.checkedListBox8.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@FSF\@FRAMEWORK\" + Var.fenetrePrincipale.checkedListBox8.CheckedItems[x].ToString());
                }

            }

            //ISLANDS
            FichierProfilXML.WriteStartElement("ISLANDS");
            if (Var.fenetrePrincipale.checkedListBox1.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= Var.fenetrePrincipale.checkedListBox1.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@FSF\@ISLANDS\" + Var.fenetrePrincipale.checkedListBox1.CheckedItems[x].ToString());
                }

            }
            FichierProfilXML.WriteEndElement();

            //UNITS
            FichierProfilXML.WriteStartElement("UNITS");
            if (Var.fenetrePrincipale.checkedListBox2.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= Var.fenetrePrincipale.checkedListBox2.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@FSF\@UNITS\" + Var.fenetrePrincipale.checkedListBox2.CheckedItems[x].ToString());

                }
            }
            FichierProfilXML.WriteEndElement();

            //MATERIEL
            FichierProfilXML.WriteStartElement("MATERIEL");
            if (Var.fenetrePrincipale.checkedListBox3.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= Var.fenetrePrincipale.checkedListBox3.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@FSF\@MATERIEL\" + Var.fenetrePrincipale.checkedListBox3.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();

            //TEST
            FichierProfilXML.WriteStartElement("TEST");
            if (Var.fenetrePrincipale.checkedListBox4.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= Var.fenetrePrincipale.checkedListBox4.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEST\" + Var.fenetrePrincipale.checkedListBox4.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();

            //CLIENT
            FichierProfilXML.WriteStartElement("CLIENT");
            if (Var.fenetrePrincipale.checkedListBox6.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= Var.fenetrePrincipale.checkedListBox6.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@FSF\@CLIENT\" + Var.fenetrePrincipale.checkedListBox6.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();

            //TEMPLATE
            FichierProfilXML.WriteStartElement("TEMPLATE");
            if (Var.fenetrePrincipale.checkedListBox7.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= Var.fenetrePrincipale.checkedListBox7.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEMPLATE\" + Var.fenetrePrincipale.checkedListBox7.CheckedItems[x].ToString());
                }
                // ecrire skin
                if (Var.fenetrePrincipale.comboBox2.Text != "")
                {
                    FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEMPLATE\@FSFSkin_" + Var.fenetrePrincipale.comboBox2.Text);

                }
            }
            // ecrire casque perso

            if (Var.fenetrePrincipale.radioButton20.Checked == true) { FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEMPLATE\@FSFUnit_HelmetsST"); }
            if (Var.fenetrePrincipale.radioButton21.Checked == true) { FichierProfilXML.WriteElementString("MODS", @"@FSF\@TEMPLATE\@FSFUnit_HelmetsXT"); }

            FichierProfilXML.WriteEndElement();

            //AUTRE MODS

            FichierProfilXML.WriteStartElement("AUTRES_MODS");
            if (Var.fenetrePrincipale.checkedListBox5.CheckedItems.Count != 0)
            {
                for (int x = 0; x <= Var.fenetrePrincipale.checkedListBox5.CheckedItems.Count - 1; x++)
                {
                    FichierProfilXML.WriteElementString("MODS", Var.fenetrePrincipale.checkedListBox5.CheckedItems[x].ToString());
                }
            }
            FichierProfilXML.WriteEndElement();

            // PARAMETRES
            FichierProfilXML.WriteStartElement("PARAMETRES");
            if (Var.fenetrePrincipale.checkBox9.Checked) { FichierProfilXML.WriteElementString("winXP", "true"); } else { FichierProfilXML.WriteElementString("winXP", ""); }
            if (Var.fenetrePrincipale.checkBox5.Checked) { FichierProfilXML.WriteElementString("showScriptErrors", "true"); } else { FichierProfilXML.WriteElementString("showScriptErrors", ""); }
            if (Var.fenetrePrincipale.checkBox4.Checked) { FichierProfilXML.WriteElementString("worldEmpty", "true"); } else { FichierProfilXML.WriteElementString("worldEmpty", ""); }
            if (Var.fenetrePrincipale.checkBox2.Checked) { FichierProfilXML.WriteElementString("noPause", "true"); } else { FichierProfilXML.WriteElementString("noPause", ""); }
            if (Var.fenetrePrincipale.checkBox1.Checked) { FichierProfilXML.WriteElementString("nosplash", "true"); } else { FichierProfilXML.WriteElementString("nosplash", ""); }
            if (Var.fenetrePrincipale.checkBox3.Checked) { FichierProfilXML.WriteElementString("window", "true"); } else { FichierProfilXML.WriteElementString("window", ""); }
            if (Var.fenetrePrincipale.checkBox6.Checked) { FichierProfilXML.WriteElementString("maxMem", Var.fenetrePrincipale.textBox5.Text); } else { FichierProfilXML.WriteElementString("maxMem", ""); }
            if (Var.fenetrePrincipale.checkBox7.Checked) { FichierProfilXML.WriteElementString("cpuCount", Var.fenetrePrincipale.textBox6.Text); } else { FichierProfilXML.WriteElementString("cpuCount", ""); }
            if (Var.fenetrePrincipale.checkBox8.Checked) { FichierProfilXML.WriteElementString("noCB", "true"); } else { FichierProfilXML.WriteElementString("noCB", ""); }
            if (Var.fenetrePrincipale.checkBox19.Checked) { FichierProfilXML.WriteElementString("minimize", "true"); } else { FichierProfilXML.WriteElementString("minimize", ""); }
            if (Var.fenetrePrincipale.checkBox23.Checked) { FichierProfilXML.WriteElementString("noFilePatching", "true"); } else { FichierProfilXML.WriteElementString("noFilePatching", ""); }
            if (Var.fenetrePrincipale.checkBox22.Checked) { FichierProfilXML.WriteElementString("VideomaxMem", Var.fenetrePrincipale.textBox20.Text); } else { FichierProfilXML.WriteElementString("VideomaxMem", ""); }
            if (Var.fenetrePrincipale.checkBox21.Checked) { FichierProfilXML.WriteElementString("threadMax", Var.fenetrePrincipale.comboBox3.SelectedIndex.ToString()); } else { FichierProfilXML.WriteElementString("threadMax", ""); }
            if (Var.fenetrePrincipale.checkBox24.Checked) { FichierProfilXML.WriteElementString("adminmode", "true"); } else { FichierProfilXML.WriteElementString("adminmode", ""); }
            if (Var.fenetrePrincipale.checkBox10.Checked) { FichierProfilXML.WriteElementString("nologs", "true"); } else { FichierProfilXML.WriteElementString("nologs", ""); }
            if (Var.fenetrePrincipale.checkBox11.Checked) { FichierProfilXML.WriteElementString("customCMDLine", Var.fenetrePrincipale.textBox22.Text); } else { FichierProfilXML.WriteElementString("customCMDLine", ""); }
            if (Var.fenetrePrincipale.checkBox_enableHT.Checked) { FichierProfilXML.WriteElementString("enableHT", "true"); } else { FichierProfilXML.WriteElementString("enableHT", ""); }       
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.Flush(); //vide le buffer
            FichierProfilXML.Close(); // ferme le document
            SauvegardePrioriteProfilXML(nomProfil);
        }
        static public void SauvegardePrioriteProfilXML(string nomProfil)
        {
            if (nomProfil == "") return;
            TabPriority.actualisePrioriteMods();
            if (!System.IO.File.Exists(Var.RepertoireDeSauvegarde + nomProfil + ".profilPriorite.xml"))
            {
                Directory.CreateDirectory(Var.RepertoireDeSauvegarde);
                FileStream fs = File.Create(Var.RepertoireDeSauvegarde + nomProfil + ".profilPriorite.xml");
                fs.Close();
            }
            XmlTextWriter FichierProfilXML = new XmlTextWriter(Var.RepertoireDeSauvegarde + nomProfil + ".profilPriorite.xml", System.Text.Encoding.UTF8);
            FichierProfilXML.Formatting = Formatting.Indented;
            FichierProfilXML.WriteStartDocument();
            FichierProfilXML.WriteComment("Creation Du profil FSF LAUNCHER " + nomProfil + ".profil.xml"); // commentaire
            FichierProfilXML.WriteComment("Détermination de la priorité par ordre d'affichage (le plus haut est le plus important"); // commentaire
            FichierProfilXML.WriteComment("> le plus haut est le plus important"); // commentaire
            FichierProfilXML.WriteStartElement("PROFIL");
            FichierProfilXML.WriteStartElement("PRIORITE");
            foreach (string ligne in Var.fenetrePrincipale.ctrlListModPrioritaire.Items)
            {
                FichierProfilXML.WriteElementString("MODS", ligne);
            }
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.Flush(); //vide le buffer
            FichierProfilXML.Close(); // ferme le document
        }
        static public void SauvegardeMissionsProfilXML(string nomProfil)
        {
            if (nomProfil == "") return;
            TabMissions.actualiseMissions();
            if (!System.IO.File.Exists(Var.RepertoireDeSauvegarde + nomProfil + ".profilMissions.xml"))
            {
                Directory.CreateDirectory(Var.RepertoireDeSauvegarde);
                FileStream fs = File.Create(Var.RepertoireDeSauvegarde + nomProfil + ".profilMissions.xml");
                fs.Close();
            }
            XmlTextWriter FichierProfilXML = new XmlTextWriter(Var.RepertoireDeSauvegarde + nomProfil + ".profilMissions.xml", System.Text.Encoding.UTF8);
            FichierProfilXML.Formatting = Formatting.Indented;
            FichierProfilXML.WriteStartDocument();
            FichierProfilXML.WriteComment("Creation Du profil FSF LAUNCHER " + nomProfil + ".profilMissions.xml"); // commentaire
            FichierProfilXML.WriteComment("Détermination de la priorité par ordre d'affichage (le plus haut est le plus important"); // commentaire
            FichierProfilXML.WriteComment("> le plus haut est le plus important"); // commentaire
            FichierProfilXML.WriteStartElement("PROFIL");
            FichierProfilXML.WriteStartElement("MISSIONS");
            foreach (string ligne in Var.fenetrePrincipale.checkedListBoxMissions.CheckedItems)
            {
                FichierProfilXML.WriteElementString("FICHIER", ligne);
            }
            FichierProfilXML.WriteEndElement();
            FichierProfilXML.WriteStartElement("DIFFICULTE");
            FichierProfilXML.WriteElementString("DIFFICULTE_ID", Var.fenetrePrincipale.comboBox6.SelectedIndex.ToString());
            FichierProfilXML.WriteEndElement();

            FichierProfilXML.WriteEndElement();
            FichierProfilXML.Flush(); //vide le buffer
            FichierProfilXML.Close(); // ferme le document
        }
        static public void SauvegardeProfilServer(string profil)
        {
            SerializeForm formSerializer = new SerializeForm();
            formSerializer.serialize(Var.fenetrePrincipale.ConfigServeur,Var.RepertoireDeSauvegarde + profil+@".FSFServer.bin");
            formSerializer.serialize(Var.fenetrePrincipale.groupBox_ConfigSynchro, Var.RepertoireDeSauvegarde + profil + @".FSFServerNetwork.bin");
        }
        static public void ChargeProfilServer(string profil)
        {
            DefautConfig();
            SerializeForm formSerializer = new SerializeForm();
            formSerializer.unSerialize(Var.fenetrePrincipale, Var.RepertoireDeSauvegarde + profil + @".FSFServer.bin");
            formSerializer.unSerialize(Var.fenetrePrincipale, Var.RepertoireDeSauvegarde + profil + @".FSFServerNetwork.bin");
        }
        static public void DefautConfig()
        {
            // Onglet General
            Var.fenetrePrincipale.textBox18.Text = "";

            // onglet Serveur
            Var.fenetrePrincipale.textBox12.Text = "";
            Var.fenetrePrincipale.textBox13.Text = "";
            Var.fenetrePrincipale.textBox14.Text = "";
            Var.fenetrePrincipale.numericUpDown1.Value = 10;
            Var.fenetrePrincipale.textBox15.Text = "2302";
            Var.fenetrePrincipale.textBox16.Text = "8766";
            Var.fenetrePrincipale.checkBox25.Checked = false;
            Var.fenetrePrincipale.checkBox26.Checked = false;
            Var.fenetrePrincipale.checkBox27.Checked = false;
            Var.fenetrePrincipale.checkBox20.Checked = false;
            Var.fenetrePrincipale.radioButton3.Checked = false;
            Var.fenetrePrincipale.radioButton4.Checked = false;
            Var.fenetrePrincipale.radioButton5.Checked = false;

            // onglet regles

            Var.fenetrePrincipale.checkBox16.Checked = false;
            Var.fenetrePrincipale.numericUpDown3.Value = 1;
            Var.fenetrePrincipale.numericUpDown4.Value = 30;
            Var.fenetrePrincipale.checkBox13.Checked = false;
            Var.fenetrePrincipale.numericUpDown2.Value = 10;
            Var.fenetrePrincipale.checkBox14.Checked = false;
            Var.fenetrePrincipale.checkBox15.Checked = false;
            Var.fenetrePrincipale.checkBox17.Checked = false;
            Var.fenetrePrincipale.checkBox18.Checked = false;
            Var.fenetrePrincipale.textBox21.Text = "";
            Var.fenetrePrincipale.numericUpDown2.Value = 10;

            // onglet difficultés            

            Var.fenetrePrincipale.radioButton7.Checked = true;

            // recruit
            Var.fenetrePrincipale.checkBox28.Checked = true;
            Var.fenetrePrincipale.checkBox29.Checked = true;
            Var.fenetrePrincipale.checkBox30.Checked = false;
            Var.fenetrePrincipale.checkBox31.Checked = true;
            Var.fenetrePrincipale.checkBox49.Checked = true;
            Var.fenetrePrincipale.checkBox34.Checked = true;
            Var.fenetrePrincipale.checkBox35.Checked = true;
            Var.fenetrePrincipale.checkBox36.Checked = true;
            Var.fenetrePrincipale.checkBox37.Checked = true;
            Var.fenetrePrincipale.checkBox38.Checked = true;
            Var.fenetrePrincipale.checkBox39.Checked = true;
            Var.fenetrePrincipale.checkBox40.Checked = true;
            Var.fenetrePrincipale.checkBox41.Checked = true;
            Var.fenetrePrincipale.checkBox42.Checked = true;
            Var.fenetrePrincipale.checkBox43.Checked = false;
            Var.fenetrePrincipale.checkBox44.Checked = false;
            Var.fenetrePrincipale.checkBox45.Checked = true;
            Var.fenetrePrincipale.checkBox46.Checked = true;
            Var.fenetrePrincipale.checkBox47.Checked = true;
            Var.fenetrePrincipale.checkBox48.Checked = true;
            Var.fenetrePrincipale.checkBox32.Checked = true;
            Var.fenetrePrincipale.comboBox_AILevelPresetRECRUIT.SelectedIndex = 0;

            // regular

            Var.fenetrePrincipale.checkBox71.Checked = true;
            Var.fenetrePrincipale.checkBox70.Checked = true;
            Var.fenetrePrincipale.checkBox69.Checked = false;
            Var.fenetrePrincipale.checkBox66.Checked = true;
            Var.fenetrePrincipale.checkBox50.Checked = true;
            Var.fenetrePrincipale.checkBox68.Checked = true;
            Var.fenetrePrincipale.checkBox63.Checked = true;
            Var.fenetrePrincipale.checkBox64.Checked = true;
            Var.fenetrePrincipale.checkBox65.Checked = true;
            Var.fenetrePrincipale.checkBox62.Checked = true;
            Var.fenetrePrincipale.checkBox61.Checked = true;
            Var.fenetrePrincipale.checkBox60.Checked = true;
            Var.fenetrePrincipale.checkBox59.Checked = true;
            Var.fenetrePrincipale.checkBox58.Checked = true;
            Var.fenetrePrincipale.checkBox57.Checked = true;
            Var.fenetrePrincipale.checkBox56.Checked = false;
            Var.fenetrePrincipale.checkBox55.Checked = true;
            Var.fenetrePrincipale.checkBox54.Checked = false;
            Var.fenetrePrincipale.checkBox53.Checked = true;
            Var.fenetrePrincipale.checkBox52.Checked = true;
            Var.fenetrePrincipale.checkBox51.Checked = true;
            Var.fenetrePrincipale.checkBox67.Checked = true;
            Var.fenetrePrincipale.comboBox_AILevelPresetREGULAR.SelectedIndex = 1;

            // Veteran

            Var.fenetrePrincipale.checkBox115.Checked = false;
            Var.fenetrePrincipale.checkBox114.Checked = false;
            Var.fenetrePrincipale.checkBox113.Checked = false;
            Var.fenetrePrincipale.checkBox110.Checked = false;
            Var.fenetrePrincipale.checkBox94.Checked = false;
            Var.fenetrePrincipale.checkBox112.Checked = false;
            Var.fenetrePrincipale.checkBox107.Checked = true;
            Var.fenetrePrincipale.checkBox108.Checked = true;
            Var.fenetrePrincipale.checkBox109.Checked = true;
            Var.fenetrePrincipale.checkBox106.Checked = true;
            Var.fenetrePrincipale.checkBox105.Checked = false;
            Var.fenetrePrincipale.checkBox104.Checked = true;
            Var.fenetrePrincipale.checkBox103.Checked = false;
            Var.fenetrePrincipale.checkBox102.Checked = true;
            Var.fenetrePrincipale.checkBox101.Checked = true;
            Var.fenetrePrincipale.checkBox100.Checked = false;
            Var.fenetrePrincipale.checkBox99.Checked = true;
            Var.fenetrePrincipale.checkBox98.Checked = false;
            Var.fenetrePrincipale.checkBox97.Checked = true;
            Var.fenetrePrincipale.checkBox96.Checked = true;
            Var.fenetrePrincipale.checkBox95.Checked = false;
            Var.fenetrePrincipale.checkBox111.Checked = true;
            Var.fenetrePrincipale.comboBox_AILevelPresetVETERAN.SelectedIndex = 2;

            // Mercenary

            Var.fenetrePrincipale.checkBox93.Checked = false;
            Var.fenetrePrincipale.checkBox92.Checked = false;
            Var.fenetrePrincipale.checkBox91.Checked = false;
            Var.fenetrePrincipale.checkBox88.Checked = false;
            Var.fenetrePrincipale.checkBox72.Checked = false;
            Var.fenetrePrincipale.checkBox90.Checked = false;
            Var.fenetrePrincipale.checkBox85.Checked = false;
            Var.fenetrePrincipale.checkBox86.Checked = false;
            Var.fenetrePrincipale.checkBox87.Checked = false;
            Var.fenetrePrincipale.checkBox84.Checked = false;
            Var.fenetrePrincipale.checkBox83.Checked = false;
            Var.fenetrePrincipale.checkBox82.Checked = true;
            Var.fenetrePrincipale.checkBox81.Checked = false;
            Var.fenetrePrincipale.checkBox80.Checked = false;
            Var.fenetrePrincipale.checkBox79.Checked = false;
            Var.fenetrePrincipale.checkBox78.Checked = false;
            Var.fenetrePrincipale.checkBox77.Checked = true;
            Var.fenetrePrincipale.checkBox76.Checked = false;
            Var.fenetrePrincipale.checkBox75.Checked = true;
            Var.fenetrePrincipale.checkBox74.Checked = true;
            Var.fenetrePrincipale.checkBox73.Checked = false;
            Var.fenetrePrincipale.checkBox89.Checked = true;
            Var.fenetrePrincipale.comboBox_AILevelPresetMERCENARY.SelectedIndex = 2;
            
            // NetWork
            Var.fenetrePrincipale.textBox2.Text = "";
            Var.fenetrePrincipale.textBox3.Text = "";
            Var.fenetrePrincipale.textBox4.Text = "";
            Var.fenetrePrincipale.textBox7.Text = "";
            Var.fenetrePrincipale.textBox8.Text = "";
            Var.fenetrePrincipale.textBox9.Text = "";
            Var.fenetrePrincipale.textBox10.Text = "";
            Var.fenetrePrincipale.textBox19.Text = "";

            // script
            Var.fenetrePrincipale.textBox30.Text = "";
            Var.fenetrePrincipale.textBox31.Text = "";
            Var.fenetrePrincipale.textBox32.Text = "";
            Var.fenetrePrincipale.textBox33.Text = "";
            Var.fenetrePrincipale.textBox34.Text = "";
            Var.fenetrePrincipale.textBox35.Text = "";
            Var.fenetrePrincipale.textBox36.Text = "";

            // Performance
            Var.fenetrePrincipale.checkBox_ActivatePerformanceTunning.Checked = false;
            Var.fenetrePrincipale.numericUpDown_TerrainGrid.Value = 12.5M;            
            Var.fenetrePrincipale.numericUpDown_ViewDistance.Value = 3000;
            Var.fenetrePrincipale.numericUpDown_PreferredObjectViewDistance.Value = 3000;
            Var.fenetrePrincipale.textBox_MaxMsgSend.Text = "";
            Var.fenetrePrincipale.textBox_MaxSizeGaranteed.Text = "";
            Var.fenetrePrincipale.textBox_MaxSizeNONGaranteed.Text = "";
            Var.fenetrePrincipale.textBox_MinimumBandwidth.Text = "";
            Var.fenetrePrincipale.textBox_MaximumBandwith.Text = "";
            Var.fenetrePrincipale.textBox_MinErrorToSend.Text = "";
            Var.fenetrePrincipale.textBox_MinErrorToSendNear.Text = "";
            Var.fenetrePrincipale.textBox_MaxCustomFileSize.Text = "";
            Var.fenetrePrincipale.checkBox_HeadLessClientActivate.Checked = false;
            Var.fenetrePrincipale.checkBox_Affinity.Checked = false;
            Var.fenetrePrincipale.textBox_ValueAffinity.Text = "";
            Var.fenetrePrincipale.checkBox_proc0.Checked = false;
            Var.fenetrePrincipale.checkBox_proc1.Checked = false;
            Var.fenetrePrincipale.checkBox_proc2.Checked = false;
            Var.fenetrePrincipale.checkBox_proc3.Checked = false;
            Var.fenetrePrincipale.checkBox_proc4.Checked = false;
            Var.fenetrePrincipale.checkBox_proc5.Checked = false;
            Var.fenetrePrincipale.checkBox_proc6.Checked = false;
            Var.fenetrePrincipale.checkBox_proc7.Checked = false;



        }
        static public void GenerefichierServeur(string profil)
        {
            string repertoireDeTravail = Var.fenetrePrincipale.textBox18.Text + @"\@FSFServer\" + profil;
            // creation repertoire
            if (!Directory.Exists(repertoireDeTravail + @"\profile"))
            {
                // repertoire n'existe pas
                Directory.CreateDirectory(repertoireDeTravail + @"\profile\Users\server");
            };

            // genere Server.bat
            GenereServerBat(profil);
            
            // creation basic.cfg
            GenereServerCfg(profil);

            // creation basic.cfg
            GenereBasicCfg(profil);

            //creation .Arma3Profile
            GenereArma3Profile(profil);
            // genere infoServeur
            Network.sauvegardeVersionA3(profil);
        }

        // Fichier SERVEUR.BAT
        static public void GenereServerBat(string profil)
        {
            string repertoireDeTravail = Var.fenetrePrincipale.textBox18.Text + @"\@FSFServer\" + profil;
            FileStream fs = File.Create(repertoireDeTravail + @"\server.bat");
            fs.Close();

            string text = "";
            text += @":FSF" + Environment.NewLine;
            text += @"Echo  off" + Environment.NewLine;
            text += @"Cls" + Environment.NewLine;
            text += @"Echo     +------------------------+" + Environment.NewLine;
            text += @"Echo     +                        +" + Environment.NewLine;
            text += @"Echo     +    FSF LAUNCHER A3     +" + Environment.NewLine;
            text += @"Echo     +                        +" + Environment.NewLine;
            text += @"Echo     +------------------------+" + Environment.NewLine;
            text += @"Echo   Profil : [" + Var.fenetrePrincipale.comboBox4.Text + "] en cours d'execution." + Environment.NewLine;
            text += Environment.NewLine;
            text += System.IO.Directory.GetDirectoryRoot(repertoireDeTravail).Replace(@"\", "") + Environment.NewLine;
            text += @"CD " + Var.fenetrePrincipale.textBox18.Text + Environment.NewLine;
            text += @"c:\Windows\System32\cmd.exe /C START ""arma3server"" /wait " + GenereAffinityArgument()+ GenereLigneArgument("win") + GenereLigneParamServeur() + Environment.NewLine;
            text += @"Echo + Arret serveur !!!" + Environment.NewLine;
            text += @"Echo + Redemarrage Serveur. Patientez SVP !" + Environment.NewLine;
            text += @"Echo .." + Environment.NewLine;
            text += @"Echo ." + Environment.NewLine;
            text += @"timeout /T 20 /NOBREAK" + Environment.NewLine;
            text += @"Goto FSF" + Environment.NewLine;
            System.IO.File.WriteAllText(repertoireDeTravail + @"\server.bat", text);
        }
        static public string GenereAffinityArgument()
        {
            string argument = "";
            if (Var.fenetrePrincipale.checkBox_Affinity.Checked)
            {
                argument += @" /affinity " + Var.fenetrePrincipale.textBox_ValueAffinity.Text + " ";
            }
            argument += @" """ + Var.fenetrePrincipale.textBox18.Text + @"\arma3server.exe"" ";
            return argument;
        }
        static public string GenereLigneArgument(string sys)
        {  
            string listMODS = "";
            string listArguments = "";

            if (sys == "win") { listMODS = @"""-MOD="; } else { listMODS = @"-MOD="""; }

            // Ligne Mods
            TabPriority.actualisePrioriteMods();
            foreach (string ligne in Var.fenetrePrincipale.ctrlListModPrioritaire.Items)
            {
                if (sys == "win")
                {
                    listMODS += ligne + ";";
                }
                else
                {
                    listMODS += ligne.Replace(@"\","/") + ";";
                };
            }
            listMODS += @""""; 


            // PARAMETRES

            if (Var.fenetrePrincipale.checkBox9.Checked) { listArguments += "-winxp "; }
            if (Var.fenetrePrincipale.checkBox5.Checked) { listArguments += "-showScriptErrors "; }
            if (Var.fenetrePrincipale.checkBox4.Checked) { listArguments += "-world=empty "; }
            if (Var.fenetrePrincipale.checkBox2.Checked) { listArguments += "-noPause "; }
            if (Var.fenetrePrincipale.checkBox1.Checked) { listArguments += "-nosplash "; }
            if (Var.fenetrePrincipale.checkBox3.Checked) { listArguments += "-window "; }
            if (Var.fenetrePrincipale.checkBox6.Checked) { listArguments += "-maxmem=" + Var.fenetrePrincipale.textBox5.Text + " "; }
            if (Var.fenetrePrincipale.checkBox7.Checked) { listArguments += "-cpuCount=" + Var.fenetrePrincipale.textBox6.Text + " "; }
            if (Var.fenetrePrincipale.checkBox8.Checked) { listArguments += "-noCB "; }
            if (Var.fenetrePrincipale.checkBox10.Checked) { listArguments += "-nologs "; }
            if (Var.fenetrePrincipale.checkBox23.Checked) { listArguments += "-noFilePatching "; }
            if (Var.fenetrePrincipale.checkBox22.Checked) { listArguments += "-maxVRAM=" + Var.fenetrePrincipale.textBox20.Text + " "; }
            if (Var.fenetrePrincipale.checkBox21.Checked) { listArguments += "-exThreads=" + Var.fenetrePrincipale.comboBox3.Text + " "; }
            if (Var.fenetrePrincipale.checkBox11.Checked) { listArguments += " " + Var.fenetrePrincipale.textBox22.Text + " "; }
            if (Var.fenetrePrincipale.checkBox_enableHT.Checked) { listArguments += "-enableHT "; }

            return listArguments += @" " + listMODS + @" ";

        }
        static private string GenereLigneParamServeur()
        {
            string cheminCfgServer = @"@FSFServer\" + (Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() + @"\";
            string parametreServeur = " ";
            parametreServeur += @"""-config=" + cheminCfgServer + @"server.cfg"" ";
            parametreServeur += @"""-cfg=" + cheminCfgServer + @"basic.cfg"" ";
            parametreServeur += @"""-profiles=" + cheminCfgServer + @"profile"" ";
            parametreServeur += @"""-name=server"" ";
            parametreServeur += "-port=" + Var.fenetrePrincipale.textBox15.Text + " ";
            return parametreServeur;
        }

        // Fichier SERVER.CFG
        static private void GenereServerCfg(string profil)
        {

            // creation server.cfg
            FileStream fs;
            string text;
            
            string repertoireDeTravail = Var.fenetrePrincipale.textBox18.Text + @"\@FSFServer\" + profil;
            fs = File.Create(repertoireDeTravail + @"\server.cfg");
            fs.Close();
            text = "";
            text += "// ******************" + Environment.NewLine;
            text += "//     server.cfg" + Environment.NewLine;
            text += "// ******************" + Environment.NewLine;
            text += "// généré avec le FSF Launcher A3 edition" + Environment.NewLine;
            text += "// www.clan-fsf.fr" + Environment.NewLine;
            text += Environment.NewLine;
            text += "//server Info" + Environment.NewLine;
            text += @"hostname = """ + Var.fenetrePrincipale.textBox12.Text + @""";" + Environment.NewLine;
            text += @"password = """ + Var.fenetrePrincipale.textBox13.Text + @""";" + Environment.NewLine;
            text += @"passwordAdmin = """ + Var.fenetrePrincipale.textBox14.Text + @""";" + Environment.NewLine;
            if (Var.fenetrePrincipale.checkBox25.Checked) { text += @"logFile = ""Console.log"";" + Environment.NewLine; };
            if (Var.fenetrePrincipale.checkBox_HeadLessClientActivate.Checked)
            {
                text += Environment.NewLine;
                text += "//Headless Client" + Environment.NewLine;
                text += "localClient[]={127.0.0.1};" + Environment.NewLine;
                text += Environment.NewLine;
            }


            text += "//STEAM Info Port" + Environment.NewLine;
            if (Var.fenetrePrincipale.textBox16.Text != "") { text += @"steamport = " + Var.fenetrePrincipale.textBox16.Text + ";" + Environment.NewLine; };
            if (Var.fenetrePrincipale.textBox21.Text != "")
            {
                text += "//Message of the Day" + Environment.NewLine;
                text += "motd[]=" + Environment.NewLine;
                text += "{" + Environment.NewLine;
                {
                    foreach (string line in Var.fenetrePrincipale.textBox21.Lines)
                    {
                        text += @"""" + line + @"""," + Environment.NewLine;
                    }
                    text += @"""""};" + Environment.NewLine;
                }
                text += "motdInterval = " + Var.fenetrePrincipale.numericUpDown10.Value.ToString() + @";" + Environment.NewLine;
            }
            text += "// Server Param" + Environment.NewLine;
            text += "maxPlayers = " + Var.fenetrePrincipale.numericUpDown1.Value.ToString() + @";" + Environment.NewLine;
            if (Var.fenetrePrincipale.checkBox18.Checked) { text += "kickDuplicate = 1;" + Environment.NewLine; };
            if (Var.fenetrePrincipale.checkBox17.Checked) { text += "verifySignatures = 2;" + Environment.NewLine; };
            if (Var.fenetrePrincipale.checkBox14.Checked) { text += "persistent = 1;" + Environment.NewLine; };
            if (Var.fenetrePrincipale.checkBox15.Checked) { text += "BattlEye=1;" + Environment.NewLine; } else { text += "BattlEye=0;" + Environment.NewLine; };
            if (Var.fenetrePrincipale.checkBox13.Checked)
            {
                text += "// VON" + Environment.NewLine;
                text += "disableVoN = 0;" + Environment.NewLine;
                text += "vonCodecQuality = " + Var.fenetrePrincipale.numericUpDown2.Value.ToString() + @";" + Environment.NewLine;
            }
            else
            {
                text += "// VON" + Environment.NewLine;
                text += "disableVoN = 1;" + Environment.NewLine;
            };
            if (Var.fenetrePrincipale.checkBox16.Checked)
            {
                text += "// voteMissionPlayers" + Environment.NewLine;
                text += "voteMissionPlayers = " + Var.fenetrePrincipale.numericUpDown3.Value.ToString() + @";" + Environment.NewLine;
                text += "voteThreshold = " + (Var.fenetrePrincipale.numericUpDown4.Value / 100).ToString().Replace(",", ".") + @";" + Environment.NewLine;
            }
            if (Var.fenetrePrincipale.radioButton3.Checked)
            {
                text += "// Enabled rotor simulation" + Environment.NewLine;
                text += "forceRotorLibSimulation = 0;" + Environment.NewLine;
            };
            if (Var.fenetrePrincipale.radioButton4.Checked)
            {
                text += "// Enabled rotor simulation" + Environment.NewLine;
                text += "forceRotorLibSimulation = 2;" + Environment.NewLine;
            };
            if (Var.fenetrePrincipale.radioButton5.Checked)
            {
                text += "// Enabled rotor simulation" + Environment.NewLine;
                text += "forceRotorLibSimulation = 1;" + Environment.NewLine;
            };
            // serveur script
            text += "// Script Server" + Environment.NewLine;
            if (Var.fenetrePrincipale.textBox30.Text != "") { text += @"doubleIdDetected = """ + Var.fenetrePrincipale.textBox30.Text + @""";" + Environment.NewLine; };
            if (Var.fenetrePrincipale.textBox31.Text != "") { text += @"onUserConnected = """ + Var.fenetrePrincipale.textBox31.Text + @""";" + Environment.NewLine; };
            if (Var.fenetrePrincipale.textBox32.Text != "") { text += @"onUserDisconnected = """ + Var.fenetrePrincipale.textBox32.Text + @""";" + Environment.NewLine; };
            if (Var.fenetrePrincipale.textBox33.Text != "") { text += @"onHackedData = """ + Var.fenetrePrincipale.textBox33.Text + @""";" + Environment.NewLine; };
            if (Var.fenetrePrincipale.textBox34.Text != "") { text += @"onDifferentData = """ + Var.fenetrePrincipale.textBox34.Text + @""";" + Environment.NewLine; };
            if (Var.fenetrePrincipale.textBox35.Text != "") { text += @"onUnsignedData = """ + Var.fenetrePrincipale.textBox35.Text + @""";" + Environment.NewLine; };
            if (Var.fenetrePrincipale.textBox36.Text != "") { text += @"regularCheck = """ + Var.fenetrePrincipale.textBox36.Text + @""";" + Environment.NewLine; };
            text += Environment.NewLine;
            // Missions
            text += "// Missions" + Environment.NewLine;
            text += "class Missions" + Environment.NewLine;
            text += "   {" + Environment.NewLine;
            int compteur = 0;
            int indexMission = 1;
            foreach (string ligne in Var.fenetrePrincipale.checkedListBoxMissions.Items)
            {
                if (Var.fenetrePrincipale.checkedListBoxMissions.GetItemChecked(compteur))
                {
                    text += "   class Mission_" + indexMission.ToString() + Environment.NewLine;
                    text += "       {" + Environment.NewLine;
                    text += @"        template = """ + ligne.Replace(".pbo", "") + @"""; " + Environment.NewLine;
                    text += @"        difficulty = """+Var.fenetrePrincipale.comboBox6.Text+@""";" + Environment.NewLine;
                    text += "       };" + Environment.NewLine;
                    indexMission++;
                }
                compteur++;
            }
            text += "   };" + Environment.NewLine;

            System.IO.File.WriteAllText(repertoireDeTravail + @"\server.cfg", text);

        }

        //Fichier BASIC.CFG
        static private void GenereBasicCfg(string profil)
        {
            string repertoireDeTravail = Var.fenetrePrincipale.textBox18.Text + @"\@FSFServer\" + profil;
            FileStream fs = File.Create(repertoireDeTravail + @"\basic.cfg");
            fs.Close();
            string text = "";
            text += "// fichier basic.cfg" + Environment.NewLine;
            text += Environment.NewLine + Environment.NewLine;
            if (Var.fenetrePrincipale.checkBox_ActivatePerformanceTunning.Checked)
            {
                text += "// These options are important for performance tuning" + Environment.NewLine;
                if (Var.fenetrePrincipale.textBox_MinimumBandwidth.Text != "") { text += "MinBandwidth = " + Var.fenetrePrincipale.textBox_MinimumBandwidth.Text + ";			// Bandwidth the server is guaranteed to have (in bps). This value helps server to estimate bandwidth available. Increasing it to too optimistic values can increase lag and CPU load, as too many messages will be sent but discarded. Default: 131072" + Environment.NewLine; };
                if (Var.fenetrePrincipale.textBox_MaximumBandwith.Text != "") { text += "MaxBandwidth = " + Var.fenetrePrincipale.textBox_MaximumBandwith.Text + ";		// Bandwidth the server is guaranteed to never have. This value helps the server to estimate bandwidth available." + Environment.NewLine; };
                if (Var.fenetrePrincipale.textBox_MaxMsgSend.Text != "") { text += "MaxMsgSend = " + Var.fenetrePrincipale.textBox_MaxMsgSend.Text + ";			// Maximum number of messages that can be sent in one simulation cycle. Increasing this value can decrease lag on high upload bandwidth servers. Default: 128" + Environment.NewLine; };
                if (Var.fenetrePrincipale.textBox_MaxSizeGaranteed.Text != "")
                {
                    text += "MaxSizeGuaranteed = " + Var.fenetrePrincipale.textBox_MaxSizeGaranteed.Text + ";		// Maximum size of guaranteed packet in bytes (without headers). Small messages are packed to larger frames. Guaranteed messages are used for non-repetitive events like shooting. Default: 512" + Environment.NewLine;
                    if (Var.fenetrePrincipale.textBox_MaxSizeNONGaranteed.Text != "") { text += "MaxSizeNonguaranteed = " + Var.fenetrePrincipale.textBox_MaxSizeNONGaranteed.Text + ";		// Maximum size of non-guaranteed packet in bytes (without headers). Non-guaranteed messages are used for repetitive updates like soldier or vehicle position. Increasing this value may improve bandwidth requirement, but it may increase lag. Default: 256" + Environment.NewLine; };
                    if (Var.fenetrePrincipale.textBox_MinErrorToSend.Text != "") { text += "MinErrorToSend = " + Var.fenetrePrincipale.textBox_MinErrorToSend.Text + ";			// Minimal error to send updates across network. Using a smaller value can make units observed by binoculars or sniper rifle to move smoother. Default: 0.001" + Environment.NewLine; };
                    if (Var.fenetrePrincipale.textBox_MinErrorToSendNear.Text != "") { text += "MinErrorToSendNear = " + Var.fenetrePrincipale.textBox_MinErrorToSendNear.Text + ";		// Minimal error to send updates across network for near units. Using larger value can reduce traffic sent for near units. Used to control client to server traffic as well. Default: 0.01" + Environment.NewLine; };
                    if (Var.fenetrePrincipale.textBox_MaxCustomFileSize.Text != "") { text += "MaxCustomFileSize = " + Var.fenetrePrincipale.textBox_MaxCustomFileSize.Text + ";			// (bytes) Users with custom face or custom sound larger than this size are kicked when trying to connect." + Environment.NewLine; };
                }
            };
                System.IO.File.WriteAllText(repertoireDeTravail + @"\basic.cfg", text);
            
        }

        //Fichier ARMA3PROFILE.CFG
        static private void GenereArma3Profile(string profil)
        {
            string repertoireDeTravail = Var.fenetrePrincipale.textBox18.Text + @"\@FSFServer\" + profil;
            FileStream fs = File.Create(repertoireDeTravail + @"\profile\Users\server\server.Arma3Profile");
            fs.Close();
            string text = "";
            text += "// *************************" + Environment.NewLine;
            text += "// fichier Arma3Profile" + Environment.NewLine;
            text += "// *************************" + Environment.NewLine;
            text += "" + Environment.NewLine;
            text += "class Difficulties" + Environment.NewLine;
            text += " {" + Environment.NewLine;
            text += "      class recruit" + Environment.NewLine;
            text += "       {" + Environment.NewLine;
            text += "       	class Flags" + Environment.NewLine;
            text += "             {" + Environment.NewLine;
            text += "              Armor=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox28.Checked) + ";" + Environment.NewLine;
            text += "              FriendlyTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox29.Checked) + ";" + Environment.NewLine;
            text += "              EnemyTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox30.Checked) + ";" + Environment.NewLine;
            text += "              MineTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox31.Checked) + ";" + Environment.NewLine;
            text += "              HUD=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox49.Checked) + ";" + Environment.NewLine;
            text += "              HUDPerm=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox33.Checked) + ";" + Environment.NewLine;
            text += "              HUDWp=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox34.Checked) + ";" + Environment.NewLine;
            text += "              HUDWpPerm=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox35.Checked) + ";" + Environment.NewLine;
            text += "              HUDGroupInfo=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox36.Checked) + ";" + Environment.NewLine;
            text += "              AutoSpot=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox37.Checked) + ";" + Environment.NewLine;
            text += "              Map=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox38.Checked) + ";" + Environment.NewLine;
            text += "              WeaponCursor=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox39.Checked) + ";" + Environment.NewLine;
            text += "              AutoGuideAT=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox40.Checked) + ";" + Environment.NewLine;
            text += "              ClockIndicator=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox41.Checked) + ";" + Environment.NewLine;
            text += "              3rdPersonView=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox42.Checked) + ";" + Environment.NewLine;
            text += "              UltraAI=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox43.Checked) + ";" + Environment.NewLine;
            text += "              CameraShake=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox44.Checked) + ";" + Environment.NewLine;
            text += "              UnlimitedSaves=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox45.Checked) + ";" + Environment.NewLine;
            text += "              DeathMessages=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox46.Checked) + ";" + Environment.NewLine;
            text += "              NetStats=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox47.Checked) + ";" + Environment.NewLine;
            text += "              VonID=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox48.Checked) + ";" + Environment.NewLine;
            text += "              ExtendetInfoType=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox32.Checked) + ";" + Environment.NewLine;
            text += "             };" + Environment.NewLine;
            text += "   		  aiLevelPreset=" + (Var.fenetrePrincipale.comboBox_AILevelPresetRECRUIT.SelectedIndex).ToString() + ";" + Environment.NewLine;
            text += "   		  skillAI=" + (Var.fenetrePrincipale.numericUpDown_SkillAIRECRUIT.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionAI=" + (Var.fenetrePrincipale.numericUpDown_PrecisionAIRECRUIT.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "       };" + Environment.NewLine;

            text += "      class regular" + Environment.NewLine;
            text += "       {" + Environment.NewLine;
            text += "       	class Flags" + Environment.NewLine;
            text += "             {" + Environment.NewLine;
            text += "              Armor=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox71.Checked) + ";" + Environment.NewLine;
            text += "              FriendlyTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox70.Checked) + ";" + Environment.NewLine;
            text += "              EnemyTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox69.Checked) + ";" + Environment.NewLine;
            text += "              MineTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox66.Checked) + ";" + Environment.NewLine;
            text += "              HUD=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox50.Checked) + ";" + Environment.NewLine;
            text += "              HUDPerm=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox68.Checked) + ";" + Environment.NewLine;
            text += "              HUDWp=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox63.Checked) + ";" + Environment.NewLine;
            text += "              HUDWpPerm=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox64.Checked) + ";" + Environment.NewLine;
            text += "              HUDGroupInfo=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox65.Checked) + ";" + Environment.NewLine;
            text += "              AutoSpot=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox62.Checked) + ";" + Environment.NewLine;
            text += "              Map=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox61.Checked) + ";" + Environment.NewLine;
            text += "              WeaponCursor=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox60.Checked) + ";" + Environment.NewLine;
            text += "              AutoGuideAT=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox59.Checked) + ";" + Environment.NewLine;
            text += "              ClockIndicator=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox58.Checked) + ";" + Environment.NewLine;
            text += "              3rdPersonView=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox57.Checked) + ";" + Environment.NewLine;
            text += "              UltraAI=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox56.Checked) + ";" + Environment.NewLine;
            text += "              CameraShake=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox55.Checked) + ";" + Environment.NewLine;
            text += "              UnlimitedSaves=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox54.Checked) + ";" + Environment.NewLine;
            text += "              DeathMessages=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox53.Checked) + ";" + Environment.NewLine;
            text += "              NetStats=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox52.Checked) + ";" + Environment.NewLine;
            text += "              VonID=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox51.Checked) + ";" + Environment.NewLine;
            text += "              ExtendetInfoType=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox67.Checked) + ";" + Environment.NewLine;
            text += "             };" + Environment.NewLine;
            text += "   		  aiLevelPreset=" + (Var.fenetrePrincipale.comboBox_AILevelPresetREGULAR.SelectedIndex).ToString() + ";" + Environment.NewLine;
            text += "   		  skillAI=" + (Var.fenetrePrincipale.numericUpDown_SkillAIREGULAR.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionAI=" + (Var.fenetrePrincipale.numericUpDown_PrecisionAIREGULAR.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "       };" + Environment.NewLine;
            text += "      class veteran" + Environment.NewLine;
            text += "       {" + Environment.NewLine;
            text += "       	class Flags" + Environment.NewLine;
            text += "             {" + Environment.NewLine;
            text += "              Armor=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox115.Checked) + ";" + Environment.NewLine;
            text += "              FriendlyTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox114.Checked) + ";" + Environment.NewLine;
            text += "              EnemyTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox113.Checked) + ";" + Environment.NewLine;
            text += "              MineTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox110.Checked) + ";" + Environment.NewLine;
            text += "              HUD=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox94.Checked) + ";" + Environment.NewLine;
            text += "              HUDPerm=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox112.Checked) + ";" + Environment.NewLine;
            text += "              HUDWp=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox107.Checked) + ";" + Environment.NewLine;
            text += "              HUDWpPerm=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox108.Checked) + ";" + Environment.NewLine;
            text += "              HUDGroupInfo=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox109.Checked) + ";" + Environment.NewLine;
            text += "              AutoSpot=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox106.Checked) + ";" + Environment.NewLine;
            text += "              Map=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox105.Checked) + ";" + Environment.NewLine;
            text += "              WeaponCursor=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox104.Checked) + ";" + Environment.NewLine;
            text += "              AutoGuideAT=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox103.Checked) + ";" + Environment.NewLine;
            text += "              ClockIndicator=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox102.Checked) + ";" + Environment.NewLine;
            text += "              3rdPersonView=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox101.Checked) + ";" + Environment.NewLine;
            text += "              UltraAI=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox100.Checked) + ";" + Environment.NewLine;
            text += "              CameraShake=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox99.Checked) + ";" + Environment.NewLine;
            text += "              UnlimitedSaves=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox98.Checked) + ";" + Environment.NewLine;
            text += "              DeathMessages=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox97.Checked) + ";" + Environment.NewLine;
            text += "              NetStats=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox96.Checked) + ";" + Environment.NewLine;
            text += "              VonID=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox95.Checked) + ";" + Environment.NewLine;
            text += "              ExtendetInfoType=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox67.Checked) + ";" + Environment.NewLine;
            text += "             };" + Environment.NewLine;
            text += "   		  aiLevelPreset=" + (Var.fenetrePrincipale.comboBox_AILevelPresetVETERAN.SelectedIndex).ToString() + ";" + Environment.NewLine;
            text += "   		  skillAI=" + (Var.fenetrePrincipale.numericUpDown_SkillAIVETERAN.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionAI=" + (Var.fenetrePrincipale.numericUpDown_PrecisionAIVETERAN.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "       };" + Environment.NewLine;

            text += "      class mercenary" + Environment.NewLine;
            text += "       {" + Environment.NewLine;
            text += "       	class Flags" + Environment.NewLine;
            text += "             {" + Environment.NewLine;
            text += "              Armor=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox93.Checked) + ";" + Environment.NewLine;
            text += "              FriendlyTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox92.Checked) + ";" + Environment.NewLine;
            text += "              EnemyTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox91.Checked) + ";" + Environment.NewLine;
            text += "              MineTag=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox88.Checked) + ";" + Environment.NewLine;
            text += "              HUD=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox72.Checked) + ";" + Environment.NewLine;
            text += "              HUDPerm=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox90.Checked) + ";" + Environment.NewLine;
            text += "              HUDWp=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox85.Checked) + ";" + Environment.NewLine;
            text += "              HUDWpPerm=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox86.Checked) + ";" + Environment.NewLine;
            text += "              HUDGroupInfo=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox87.Checked) + ";" + Environment.NewLine;
            text += "              AutoSpot=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox84.Checked) + ";" + Environment.NewLine;
            text += "              Map=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox83.Checked) + ";" + Environment.NewLine;
            text += "              WeaponCursor=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox82.Checked) + ";" + Environment.NewLine;
            text += "              AutoGuideAT=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox81.Checked) + ";" + Environment.NewLine;
            text += "              ClockIndicator=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox80.Checked) + ";" + Environment.NewLine;
            text += "              3rdPersonView=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox79.Checked) + ";" + Environment.NewLine;
            text += "              UltraAI=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox78.Checked) + ";" + Environment.NewLine;
            text += "              CameraShake=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox77.Checked) + ";" + Environment.NewLine;
            text += "              UnlimitedSaves=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox76.Checked) + ";" + Environment.NewLine;
            text += "              DeathMessages=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox75.Checked) + ";" + Environment.NewLine;
            text += "              NetStats=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox74.Checked) + ";" + Environment.NewLine;
            text += "              VonID=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox73.Checked) + ";" + Environment.NewLine;
            text += "              ExtendetInfoType=" + Convert.ToInt32(Var.fenetrePrincipale.checkBox89.Checked) + ";" + Environment.NewLine;
            text += "             };" + Environment.NewLine;
            text += "   		  aiLevelPreset=" + (Var.fenetrePrincipale.comboBox_AILevelPresetMERCENARY.SelectedIndex).ToString() + ";" + Environment.NewLine;
            text += "   		  skillAI=" + (Var.fenetrePrincipale.numericUpDown_SkillAIMERCENARY.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "   		  precisionAI=" + (Var.fenetrePrincipale.numericUpDown_PrecisionAIMERCENARY.Value).ToString().Replace(",", ".") + ";" + Environment.NewLine;
            text += "       };" + Environment.NewLine;
            text += " };" + Environment.NewLine;
            System.IO.File.WriteAllText(repertoireDeTravail + @"\profile\Users\server\server.Arma3Profile", text);
        }

        }
}
