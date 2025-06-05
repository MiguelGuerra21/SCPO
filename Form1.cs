using EGIS.Controls;
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
        private void CloseDialog_Button(object sender, EventArgs e)
        {
            this.Close();
        }


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
