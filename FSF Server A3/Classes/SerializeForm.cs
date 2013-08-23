using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSF_Server_A3.Classes
{
    class SerializeForm
    {
        public void serialize(Control controlToSerialize, string filename)
        {
            //On créé la table des données
            ArrayList data = new ArrayList();

            //Lecture des controles du control initial
            readControls(controlToSerialize, data);

            saveToFile(filename, data);
        }

        public void readControls(Control groupControl, ArrayList data)
        {
            //On récupère tous les controls du tabpage
            foreach (Control control in groupControl.Controls)
            {
                SimplifiedControl sc = null;
                if (control.GetType() == typeof(TextBox))
                    sc = new SimplifiedControl(control.Name, ((TextBox)control).Text, control.GetType());
                else if (control.GetType() == typeof(CheckBox))
                    sc = new SimplifiedControl(control.Name, ((CheckBox)control).Checked, control.GetType());
                else if (control.GetType() == typeof(RadioButton))
                    sc = new SimplifiedControl(control.Name, ((RadioButton)control).Checked, ((RadioButton)control).Text, control.GetType());
                else if (control.GetType() == typeof(NumericUpDown))
                    sc = new SimplifiedControl(control.Name, ((NumericUpDown)control).Value, control.GetType());
                else if (control.GetType() == typeof(ComboBox))
                    sc = new SimplifiedControl(control.Name, ((ComboBox)control).SelectedIndex, control.GetType());

                else if (control.GetType() == typeof(GroupBox))
                    readControls(control, data);
                else if (control.GetType() == typeof(TabPage))
                    readControls(control, data);
                else if (control.GetType() == typeof(TabControl))
                    readControls(control, data);



                if (sc != null) data.Add(sc);
            }
        }

        public void unSerialize(Form form, string filename)
        {
            if (!System.IO.File.Exists(filename))
            {
                // n'existe pas
                return;
            };
            //On récupère la table des données
            ArrayList data = readFromFile(filename);
            try
            {
                //On met à jour tous les controls du tabpage
                foreach (SimplifiedControl simplifiedControl in data)
                {
                    //On recherche le control depuis la From
                    Control c = form.Controls.Find(simplifiedControl.getName(), true)[0];

                    if (simplifiedControl.getType() == typeof(TextBox))
                        ((TextBox)c).Text = (string)simplifiedControl.getValue1();

                    else if (simplifiedControl.getType() == typeof(CheckBox))
                    {
                        ((CheckBox)c).Checked = (bool)simplifiedControl.getValue1();
                    }
                    else if (simplifiedControl.getType() == typeof(RadioButton))
                    {
                        ((RadioButton)c).Checked = (bool)simplifiedControl.getValue1();
                        //((RadioButton)c).Text = (string)simplifiedControl.getValue2();
                    }
                    else if (simplifiedControl.getType() == typeof(NumericUpDown))
                    {
                        ((NumericUpDown)c).Value = (decimal)simplifiedControl.getValue1();
                    }
                    else if (simplifiedControl.getType() == typeof(ComboBox))
                    {
                        ((ComboBox)c).SelectedIndex = (int)simplifiedControl.getValue1();
                    }
                }
            }
            catch
            {
            }

        }

        private void saveToFile(string filename, ArrayList data)
        {
            BinaryFormatter BinFormatter = new BinaryFormatter();
            FileStream FS = new FileStream(filename, FileMode.Create);
            BinFormatter.Serialize(FS, data);
            FS.Close();
        }

        private ArrayList readFromFile(string filename)
        {

            ArrayList data = null;
            BinaryFormatter BinFormatter = new BinaryFormatter();
            FileStream FS = new FileStream(filename, FileMode.Open);
            data = (ArrayList)BinFormatter.Deserialize(FS);
            FS.Close();
            return data;
        }

        //Le container pour sauvgerader les infos de chaque control
        [Serializable]
        private class SimplifiedControl
        {
            private string name;
            private object value1;
            private object value2;
            private Type type;

            public SimplifiedControl(string name, object value1, Type type)
            {
                this.name = name;
                this.value1 = value1;
                this.type = type;
            }

            public SimplifiedControl(string name, object value1, object value2, Type type)
            {
                this.name = name;
                this.value1 = value1;
                this.value2 = value2;
                this.type = type;
            }

            public Type getType()
            {
                return type;
            }

            public string getName()
            {
                return name;
            }

            public object getValue1()
            {
                return value1;
            }

            public object getValue2()
            {
                return value2;
            }
        }
    }
}

