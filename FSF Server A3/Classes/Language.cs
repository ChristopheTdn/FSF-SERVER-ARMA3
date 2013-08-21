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
       

        static public void ChangeLangage(string langue)
        {
            SauvegardeLangage(langue);
        }
        static private void SauvegardeLangage(string langue)
        {
            Core.SetKeyValue(@"Software\Clan FSF\FSF Server A3\", "langage", langue);
        }
        
    }
}
