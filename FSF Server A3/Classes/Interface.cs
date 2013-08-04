using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSF_Server_A3.Classes;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace FSF_Server_A3.Classes
{

    //  Classe Combo BoxItem

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    class Interface
    {
        

        static public void dessineInterface(string profil)
        {
            // Dessin Interface Constant
            Var.fenetrePrincipale.label31.Text = AfficheVersionProgramme();
            // Genere Tabs 
            genereTab(profil);
            // Bouton Lancer Serveur
            if (!System.IO.File.Exists(Var.fenetrePrincipale.textBox18.Text + @"\arma3server.exe"))
            {
                Var.fenetrePrincipale.button16.Enabled = false;
            }
            else
            {
                Var.fenetrePrincipale.button16.Enabled = true;
            }
            // Bouton Synchro WINSCP
            if (File.Exists(Var.RepertoireApplication+@"\winscp.exe"))
            {
                Var.fenetrePrincipale.button26.Visible = false;
                Var.fenetrePrincipale.progressBar1.Visible = false;
                Var.fenetrePrincipale.button25.Enabled = true; 

            }
            else
            {
                Var.fenetrePrincipale.button26.Visible = true;
                Var.fenetrePrincipale.progressBar1.Visible = true;
                Var.fenetrePrincipale.button25.Enabled = false; 

            }


        }
        static private string AfficheVersionProgramme()
        {
            string versionProg = "";
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Version version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                versionProg = "v. " + version.Major + "." + version.Minor + "." + version.Build + " (Rev. " + version.Revision + ")";
            }
            return versionProg;

        }

        // TABS

        // GESTION TAB MODS 
        static public void genereTab(string profil)
        {
            effaceTousItemsOnglets();
            GestionProfil.ChargeProfilServer(profil);
            
            // @FSF 
            ListeTab(Var.fenetrePrincipale.checkedListBox7, "@TEMPLATE", profil);
            ListeTab(Var.fenetrePrincipale.checkedListBox1, "@ISLANDS", profil);
            ListeTab(Var.fenetrePrincipale.checkedListBox2, "@UNITS", profil);
            ListeTab(Var.fenetrePrincipale.checkedListBox3, "@MATERIEL", profil);
            ListeTab(Var.fenetrePrincipale.checkedListBox6, "@CLIENT", profil);
            ListeTab(Var.fenetrePrincipale.checkedListBox4, "@TEST", profil);
            // @Autre
            ListeTab(Var.fenetrePrincipale.checkedListBox5, "", profil);
            //genereTabMods();
            genereTabParam(profil);
            TabPriority.genereTabPriorite(profil);
            TabMissions.genereTabMissions(profil);
        }
        static public void effaceTousItemsOnglets()
        {
            Var.fenetrePrincipale.comboBox2.Items.Clear();
            Var.fenetrePrincipale.comboBox2.Items.Add("");
            Var.fenetrePrincipale.radioButton20.Enabled = false;
            Var.fenetrePrincipale.radioButton20.Checked = false;
            Var.fenetrePrincipale.radioButton21.Enabled = false;
            Var.fenetrePrincipale.radioButton21.Checked = false;
            Var.fenetrePrincipale.pictureBox1.Image = FSF_Server_A3.Properties.Resources.logo_fsf;
            Var.fenetrePrincipale.checkedListBox7.Items.Clear();
            Var.fenetrePrincipale.checkedListBox1.Items.Clear();
            Var.fenetrePrincipale.checkedListBox2.Items.Clear();
            Var.fenetrePrincipale.checkedListBox3.Items.Clear();
            Var.fenetrePrincipale.checkedListBox6.Items.Clear();
            Var.fenetrePrincipale.checkedListBox4.Items.Clear();
            Var.fenetrePrincipale.checkedListBox5.Items.Clear();
        }
        static public List<string> GenereListeFSF(string repertoireSource)
        {
            List<string> listeRepertoire = new List<string>();
            if (!System.IO.Directory.Exists(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\" + repertoireSource))
            {
                return listeRepertoire;
            }



            string[] tableauRepertoire = Directory.GetDirectories(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\" + repertoireSource + @"\", "Addons*", SearchOption.AllDirectories);

            foreach (var ligne in tableauRepertoire)
            {
                // Genere les Tab Specifiques pour les tenues FSF
                if ((ligne.IndexOf(@"\@FSF\@TEMPLATE\@FSFSkin_") != -1) && (ligne.IndexOf(@"\@FSF\@TEMPLATE\@FSFUnits_Cfg") == -1))
                {
                    string addons = ligne.Replace(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\@TEMPLATE\@FSFSkin_", "");
                    Var.fenetrePrincipale.comboBox2.Items.Add(addons.Replace(@"\addons", ""));
                }
                else
                    if (ligne.IndexOf(@"\@FSF\@TEMPLATE\@FSFUnit_Helmets") != -1)
                    {
                        if ((ligne.IndexOf(@"@FSF\@TEMPLATE\@FSFUnit_HelmetsST\") != -1)) { Var.fenetrePrincipale.radioButton20.Enabled = true; }
                        if ((ligne.IndexOf(@"@FSF\@TEMPLATE\@FSFUnit_HelmetsXT\") != -1)) { Var.fenetrePrincipale.radioButton21.Enabled = true; }
                    }
                    else
                    {
                        string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                        listeRepertoire.Add(menuRepertoire.Replace(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\" + repertoireSource + @"\", ""));
                    }
            }
            return listeRepertoire;

        }
        static public List<string> GenereListeAUTRE(string repertoireSource)
        {

            List<string> listeRepertoire = new List<string>();
            try
            {
                string[] tableauRepertoire = Directory.GetDirectories(Var.fenetrePrincipale.textBox18.Text, "Addons*", SearchOption.AllDirectories);

                foreach (var ligne in tableauRepertoire)
                {
                    string menuRepertoire = System.IO.Directory.GetParent(ligne).ToString();
                    string nomAAjouter = menuRepertoire;
                    if ((nomAAjouter.IndexOf(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\@ISLANDS\") == -1)
                        && (nomAAjouter.IndexOf(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\@UNITS\") == -1)
                        && (nomAAjouter.IndexOf(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\@MATERIEL\") == -1)
                        && (nomAAjouter.IndexOf(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\@TEMPLATE\") == -1)
                        && (nomAAjouter.IndexOf(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\@CLIENT\") == -1)
                        && (nomAAjouter.IndexOf(Var.fenetrePrincipale.textBox18.Text + @"\@FSF\@TEST\") == -1)
                        && (nomAAjouter.IndexOf(".pack") == -1)
                        && (nomAAjouter.IndexOf(".rsync") == -1)
                        )
                    {

                        if ((menuRepertoire) != Var.fenetrePrincipale.textBox18.Text)
                        {
                            listeRepertoire.Add(menuRepertoire.Replace(Var.fenetrePrincipale.textBox18.Text + @"\", ""));
                        }

                    }
                }
            }
            catch
            {
            }
            return listeRepertoire;
        }
        static public void ListeTab(CheckedListBox Tab, string nomRep, string nomProfil)
        {
            List<string> tableauValeur;
            if (nomRep != "")
            {
                tableauValeur = GenereListeFSF(nomRep);
            }
            else
            {
                tableauValeur = GenereListeAUTRE(nomRep);
            }


            string tagNameXML;
            string filtreRepertoire;
            switch (nomRep)
            {
                case "":
                    tagNameXML = "AUTRES_MODS";
                    filtreRepertoire = "";
                    break;
                case "@TEMPLATE":
                    tagNameXML = "TEMPLATE";
                    filtreRepertoire = @"@FSF\@TEMPLATE\";
                    break;
                case "@ISLANDS":
                    tagNameXML = "ISLANDS";
                    filtreRepertoire = @"@FSF\@ISLANDS\";
                    break;
                case "@UNITS":
                    tagNameXML = "UNITS";
                    filtreRepertoire = @"@FSF\@UNITS\";
                    break;
                case "@MATERIEL":
                    tagNameXML = "MATERIEL";
                    filtreRepertoire = @"@FSF\@MATERIEL\";
                    break;
                case "@CLIENT":
                    tagNameXML = "CLIENT";
                    filtreRepertoire = @"@FSF\@CLIENT\";
                    break;
                case "@TEST":
                    tagNameXML = "TEST";
                    filtreRepertoire = @"@FSF\@TEST\";
                    break;
                default:
                    tagNameXML = "AUTRES_MODS";
                    filtreRepertoire = "";
                    break;
            }
            XmlDocument fichierProfilXML = new XmlDocument();
            if (nomProfil == "") { nomProfil = "defaut"; };
            fichierProfilXML.Load(Var.RepertoireDeSauvegarde + nomProfil + ".profil.xml");


            foreach (var ligne in tableauValeur)
            {
                bool elementsProfilChecked = false;
                // Read the XmlDocument (Directory Node)
                XmlNodeList elemList = fichierProfilXML.GetElementsByTagName(tagNameXML);
                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlNodeList eltList = elemList[i].ChildNodes;
                    for (int j = 0; j < eltList.Count; j++)
                    {
                        string repertoireAChercher = eltList[j].InnerXml;
                        if (repertoireAChercher.IndexOf(@"@FSF\@TEMPLATE\@FSFSkin_") != -1)
                        {
                            int indexApparence = 0;
                            foreach (string apparencePossible in Var.fenetrePrincipale.comboBox2.Items)
                            {
                                if (@"@FSF\@TEMPLATE\@FSFSkin_" + apparencePossible == repertoireAChercher)
                                {
                                    Var.fenetrePrincipale.comboBox2.SelectedIndex = indexApparence;
                                }
                                indexApparence++;
                            }

                        }
                        if (repertoireAChercher == @"@FSF\@TEMPLATE\@FSFUnit_HelmetsST") { Var.fenetrePrincipale.radioButton20.Checked = true; }
                        if (repertoireAChercher == @"@FSF\@TEMPLATE\@FSFUnit_HelmetsXT") { Var.fenetrePrincipale.radioButton21.Checked = true; }

                        if (filtreRepertoire + ligne == repertoireAChercher)
                        {
                            elementsProfilChecked = true;
                        }

                    }
                    Tab.Items.Add(ligne, elementsProfilChecked);
                }

            }



        }
        static public void SelectionneTousTAB(CheckedListBox tab)
        {
            for (int x = 0; x <= tab.Items.Count - 1; x++)
            {
                tab.SetItemChecked(x, true);
            }
        }
        static public void InverseTousTAB(CheckedListBox tab)
        {
            for (int x = 0; x <= tab.Items.Count - 1; x++)
            {
                if (tab.GetItemChecked(x))
                {
                    tab.SetItemChecked(x, false);
                }
                else
                {
                    tab.SetItemChecked(x, true);
                }
            }
        }

        // GESTION TABS PARAM

        static public void effaceTousparamsOnglet()
        {
            Var.fenetrePrincipale.checkBox1.Checked = false;
            Var.fenetrePrincipale.checkBox2.Checked = false;
            Var.fenetrePrincipale.checkBox3.Checked = false;
            Var.fenetrePrincipale.checkBox4.Checked = false;
            Var.fenetrePrincipale.checkBox5.Checked = false;
            Var.fenetrePrincipale.checkBox6.Checked = false;
            Var.fenetrePrincipale.checkBox7.Checked = false;
            Var.fenetrePrincipale.checkBox8.Checked = false;
            Var.fenetrePrincipale.checkBox9.Checked = false;
            Var.fenetrePrincipale.checkBox10.Checked = false;
            Var.fenetrePrincipale.checkBox19.Checked = false;
            Var.fenetrePrincipale.checkBox22.Checked = false;
            Var.fenetrePrincipale.checkBox23.Checked = false;
            Var.fenetrePrincipale.checkBox21.Checked = false;
            Var.fenetrePrincipale.checkBox24.Checked = false;

        }
        static public void genereTabParam(string profil)
        {
            effaceTousparamsOnglet();
            if (profil == "") return;
            XmlTextReader fichierProfilXML = new XmlTextReader(Var.RepertoireDeSauvegarde + profil + ".profil.xml");
            while (fichierProfilXML.Read())
            {

                fichierProfilXML.ReadToFollowing("winXP");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox9.Checked = true; }
                fichierProfilXML.ReadToFollowing("showScriptErrors");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox5.Checked = true; }
                fichierProfilXML.ReadToFollowing("worldEmpty");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox4.Checked = true; }
                fichierProfilXML.ReadToFollowing("noPause");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox2.Checked = true; }
                fichierProfilXML.ReadToFollowing("nosplash");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox1.Checked = true; }
                fichierProfilXML.ReadToFollowing("window");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox3.Checked = true; }
                fichierProfilXML.ReadToFollowing("maxMem");
                string maxmem = fichierProfilXML.ReadString();
                if (maxmem != "") { Var.fenetrePrincipale.checkBox6.Checked = true; Var.fenetrePrincipale.trackBar1.Value = int.Parse(maxmem); Var.fenetrePrincipale.textBox5.Text = maxmem; }
                fichierProfilXML.ReadToFollowing("cpuCount");
                string cpucount = fichierProfilXML.ReadString();
                if (cpucount != "") { Var.fenetrePrincipale.checkBox7.Checked = true; Var.fenetrePrincipale.trackBar2.Value = int.Parse(cpucount); Var.fenetrePrincipale.textBox6.Text = cpucount; }
                fichierProfilXML.ReadToFollowing("noCB");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox8.Checked = true; }
                fichierProfilXML.ReadToFollowing("minimize");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox19.Checked = true; }
                fichierProfilXML.ReadToFollowing("noFilePatching");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox23.Checked = true; }
                fichierProfilXML.ReadToFollowing("VideomaxMem");
                string Videomaxmem = fichierProfilXML.ReadString();
                if (Videomaxmem != "") { Var.fenetrePrincipale.checkBox22.Checked = true; Var.fenetrePrincipale.trackBar3.Value = int.Parse(Videomaxmem); Var.fenetrePrincipale.textBox20.Text = Videomaxmem; }
                fichierProfilXML.ReadToFollowing("threadMax");
                string threadMax = fichierProfilXML.ReadString();
                if (threadMax != "") { Var.fenetrePrincipale.checkBox21.Checked = true; Var.fenetrePrincipale.comboBox3.SelectedIndex = int.Parse(threadMax); }
                fichierProfilXML.ReadToFollowing("adminmode");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox24.Checked = true; }
                fichierProfilXML.ReadToFollowing("nologs");
                if (fichierProfilXML.ReadString() == "true") { Var.fenetrePrincipale.checkBox10.Checked = true; }

            }
            fichierProfilXML.Close();
        }
        static public void AjouteComboNomProfil(int index, string nomProfil)
        {
            ComboboxItem item = new ComboboxItem();
            string textAffiche = nomProfil;
            if (nomProfil == "defaut")
            {
                string langue = Core.GetKeyValue(@"Software\Clan FSF\FSF Server A3\", "langage");
                switch (langue)
                {
                    case "en-US":
                        textAffiche = "default";
                        break;
                    case "ru-RU":
                        textAffiche = "умолчание";
                        break;
                    case "de-DE":
                        textAffiche = "Vorgabe";
                        break;
                    case "el-GR":
                        textAffiche = "Προεπιλογή";
                        break;
                    default:
                        textAffiche = "defaut";
                        break;
                }

            }

            item.Text = textAffiche;
            item.Value = nomProfil;
            Var.fenetrePrincipale.comboBox4.Items.Add(item);

        }
        static public void AjouteListeBoxNomProfil(int index, string nomProfil)
        {
            ComboboxItem item = new ComboboxItem();
            string textAffiche = nomProfil;
            if (nomProfil == "defaut")
            {
                string langue = Core.GetKeyValue(@"Software\Clan FSF\FSF Server A3\", "langage");
                switch (langue)
                {
                    case "en-US":
                        textAffiche = "default";
                        break;
                    case "ru-RU":
                        textAffiche = "умолчание";
                        break;
                    case "de-DE":
                        textAffiche = "Vorgabe";
                        break;
                    case "el-GR":
                        textAffiche = "Προεπιλογή";
                        break;
                    default:
                        textAffiche = "defaut";
                        break;
                }

            }

            item.Text = textAffiche;
            item.Value = nomProfil;
            Var.fenetrePrincipale.listBox1.Items.Add(item);
        }





    }
}
