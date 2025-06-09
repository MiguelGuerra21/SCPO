using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example1
{
    public partial class FormModificarCampos : Form
    {
        public FormModificarCampos(string[] data)
        {
            InitializeComponent();

            if (data != null &&data.Length > 0)
            {

                listBox1.Items.AddRange(data);
            }
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el texto seleccionado
            string selectedItem = listBox1.SelectedItem?.ToString();

            // Verifica si es el campo que queremos editar
            if (!string.IsNullOrEmpty(selectedItem))
            {
                textBoxEdit.Text = selectedItem.ToString();
            }
            else
            {
                textBoxEdit.Text = "Selecciona un campo de la lista para editarlo";
            }
        }

        // Opción para devolver datos modificados
        public string[] DatosModificados
        {
            get
            {
                return listBox1.Items.Cast<string>().ToArray();
            }
        }
        public string ValorEditado { get; private set; }
        public int IndiceEditado { get; private set; }

        private void SaveDialog_Button(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            if (index != -1)
            {
                ValorEditado = textBoxEdit.Text.Trim();
                IndiceEditado = index;

                // Puedes actualizar visualmente en el listBox interno si quieres
                listBox1.Items[index] = ValorEditado;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void CloseDialog_Button(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
