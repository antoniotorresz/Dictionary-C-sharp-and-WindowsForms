using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Traductor
{
    public partial class Form1 : Form
    {

        /*
         ANTONIO TORRES ITI 707
         UNIVERSIDAD TECNOLÓGICA DE LEÓN
             */
        private List<Palabra> palabras = new List<Palabra>();

        private List<string> contentTxtIng = new List<string>();
        private List<string> contentTxtEsp = new List<string>();

        //Los archivos se guardan en esta ruta: \Traductor\Traductor\bin
        private static string PATH_ESPANOL = @"../espanol.txt";
        private static string PATH_INGLES = @"../ingles.txt";

        public Form1()
        {
            InitializeComponent();
            fillEspList();
            fillIngList();

            cargarPalabras();
            label6.Text = "Palabras en el diccionario: " + palabras.Count;
        }

        private void cargarPalabras()
        {
            if (contentTxtEsp.Count > 0 && contentTxtIng.Count > 0) {
                if (!(contentTxtEsp.Count != contentTxtIng.Count))
                {
                    for (int i = 0; i < contentTxtEsp.Count; i++) {
                        string espanol = contentTxtEsp[i].ToString();
                        string ingles = contentTxtIng[i].ToString();

                        validarPalabras(espanol,ingles);
                    }
                }
                else {
                    MessageBox.Show("Palabras mal estructuradas, se limpiarán los archivos de texto");
                }
            }
        }

        private void fillIngList()
        {
            using (StreamReader reader = File.OpenText(PATH_INGLES))
                while (!reader.EndOfStream)
                {
                    string linea = reader.ReadLine();
                    contentTxtIng.Add(linea);
                }

            //MessageBox.Show("Palabras en inglés: " + contentTxtIng.Count);
        }

        private void fillEspList()
        {
            using (StreamReader reader = File.OpenText(PATH_ESPANOL))
                while (!reader.EndOfStream) {
                    string linea = reader.ReadLine();
                    contentTxtEsp.Add(linea);
                }

            //MessageBox.Show("Palabras en español: " + contentTxtEsp.Count);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string palabraEspaniol = textBox1.Text;
            string palabraIngles = textBox2.Text;
            validarPalabras(palabraEspaniol, palabraIngles);
        }

        private void validarPalabras(string palabraEspaniol, string palabraIngles) {
            if (!string.IsNullOrEmpty(palabraEspaniol) && !string.IsNullOrEmpty(palabraIngles))
            {
                if (!palabraEspaniol.Any(char.IsDigit) && !palabraIngles.Any(char.IsDigit))
                {
                    agregarPalabra(palabraEspaniol, palabraIngles);
                }
                else
                {
                    MessageBox.Show("No puedes introducir palabras con números, agrega una palabra con letras solamente");
                }
            }
            else
            {
                MessageBox.Show("Llena los 2 campos ");
            }
        }

        private void agregarPalabra(string espaniol, string ingles)
        {
            Palabra palabra = new Palabra();
            palabra.SigEspanol = espaniol;
            palabra.SigIngles = ingles;

            palabras.Add(palabra);

            label6.Text = "Palabras en el diccionario: " + palabras.Count;

            txtEspanol(espaniol);
            txtIngles(ingles);

            textBox1.Text = null;
            textBox2.Text = null;
        }

        private void txtIngles(string ingles)
        {
            string text = "";
            for (int i = 0; i < palabras.Count; i++)
            {
                text += palabras[i].SigIngles + "\n";
            }

            File.WriteAllText(PATH_INGLES, text);
            //textBox4.Text = (File.ReadAllText(PATH_INGLES));
            leerIngles();
        }

        private void leerIngles()
        {
            string ingContent = "";
            for (int i = 0; i < palabras.Count; i++) {
                ingContent += palabras[i].SigIngles + "\r\n";
            }

            textBox4.Text = ingContent;
        }

        private void txtEspanol(string espaniol)
        {
            string text = "";
            for (int i = 0; i < palabras.Count; i++) {
                text += palabras[i].SigEspanol +"\n" ;
            }
            
            File.WriteAllText(PATH_ESPANOL, text);
            //textBox3.Text = (File.ReadAllText(PATH_ESPANOL));
            leerEspaniol();
        }

        private void leerEspaniol()
        {
            string espContent = "";
            for (int i = 0; i < palabras.Count; i++)
            {
                espContent += palabras[i].SigEspanol + "\r\n";
            }

            textBox3.Text = espContent;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                if (radioButton1.Checked)
                {
                    buscarEsp();
                }

                if (radioButton2.Checked)
                {
                    buscarIng();
                }
            }
            else {
                MessageBox.Show("Seleccione un idioma para buscar");
            }

        }

        private void buscarIng()
        {
            string referencia = textBox5.Text;
            if (!string.IsNullOrEmpty(referencia))
            {
                Palabra item = palabras.FirstOrDefault(Palabra => Palabra.SigIngles == referencia);

                if (item != null)
                {
                    textBox6.Text = item.SigEspanol;
                }
                else {
                    //MessageBox.Show("No se pudo encontrar la palabra, agregar?");
                    DialogResult dialogResult = MessageBox.Show("¿Agregar la palabra al diccionario?", "Palabra no encontrada", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string palabraEsp = Convert.ToString(Microsoft.VisualBasic.Interaction.InputBox("Ingrese palabra en español"));
                        string palabraIng = Convert.ToString(Microsoft.VisualBasic.Interaction.InputBox("Ingrese palabra en inglés"));

                        validarPalabras(palabraEsp, palabraIng);
                    }
                }
            }
            else {
                MessageBox.Show("Ingrese la palabra a buscar");
            }
        }

        private void buscarEsp()
        {
            string referencia = textBox5.Text;
            if (!string.IsNullOrEmpty(referencia))
            {
                Palabra item = palabras.FirstOrDefault(Palabra => Palabra.SigEspanol == referencia);

                if (item != null)
                {
                    textBox6.Text = item.SigIngles;
                }
                else
                {
                    //MessageBox.Show("No se pudo encontrar la palabra, agregar?");
                    DialogResult dialogResult = MessageBox.Show("¿Agregar la palabra al diccionario?", "Palabra no encontrada", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes) {
                        string palabraEsp = Convert.ToString(Microsoft.VisualBasic.Interaction.InputBox("Ingrese palabra en español"));
                        string palabraIng = Convert.ToString(Microsoft.VisualBasic.Interaction.InputBox("Ingrese palabra en ingles"));

                        validarPalabras(palabraEsp, palabraIng);
                    }
                    else if (dialogResult == DialogResult.No) {

                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese la palabra a buscar");
            }
        }
    }
}
