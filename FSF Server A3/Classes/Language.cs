using Infralution.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSF_Server_A3.Classes
{
    class Language
    {

        static public void DetermineLanguage()
        {
            try
            {
                if (Core.GetKeyValue(@"Software\Clan FSF\FSF Server A3\", "langage") != "")
                {
                    CultureManager.ApplicationUICulture = new CultureInfo(Core.GetKeyValue(@"Software\Clan FSF\FSF Server A3\", "langage"));
                }
            }
            catch
            {
            }


        }
        static public void ChangeLangage(string langue)
        {
            SauvegardeLangage(langue);
        }
        static private void SauvegardeLangage(string langue)
        {
            Core.SetKeyValue(@"Software\Clan FSF\FSF Server A3\", "langage", langue);
        }
       static public void CheckButtonLanguageOption()
        {
            try
            {
                switch (Core.GetKeyValue(@"Software\Clan FSF\FSF Server A3\", "langage"))
                {
                    case "fr-fr":
                    Var.fenetrePrincipale.radioButton1.Checked = true;
                    break;
                    case "en-us":
                    Var.fenetrePrincipale.radioButton2.Checked = true;
                    break;
                }            
            }
            catch
            {
            }
        }
        
    }
}
