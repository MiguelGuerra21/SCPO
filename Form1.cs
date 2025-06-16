using EGIS.Controls;
using Example1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDCO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public string[] ItemsListBox
        {
            get
            {
                return listBox1.Items.Cast<string>().ToArray();
            }
            set
            {
                if (value != null)
                {
                    this.listBox1.Items.AddRange(value);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ModifyDialog_Button(object sender, EventArgs e)
        {
            // Guardar todos los ítems de ListBox1 en un array
            string[] datos = listBox1.Items.Cast<string>().ToArray();
            FormModificarCampos dialog = new FormModificarCampos(datos);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                // Paso 3: Obtener datos modificados y actualizar el listBox original
                listBox1.Items.Clear();
                listBox1.Items.AddRange(dialog.DatosModificados);
            }
        }

        public string[] datosModificados { get; set; }
        public int indiceModificado { get; set; }

        private void SaveDialog_Button(object sender, EventArgs e)
        {
            //Guardar los datos del ListBox1 en un array
            string[] datos = listBox1.Items.Cast<string>().ToArray();
            this.indiceModificado = listBox1.SelectedIndex;
            

            if (indiceModificado != -1)
            {
                string nuevoValor = listBox1.Text.Trim();
                listBox1.Items[indiceModificado] = nuevoValor;
            }
            this.Refresh();
            this.DialogResult = DialogResult.OK;
            this.Close();
            //MessageBox.Show("Changes saved successfully!");
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CloseDialog_Button(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
    }
}
