using System.Windows.Forms;

namespace Example1
{
    partial class FormModificarCampos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxLabel = new System.Windows.Forms.Label();
            this.textBoxEdit = new System.Windows.Forms.TextBox();
            this.textBoxErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(16, 36);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(290, 404);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Modificar propiedades";
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.Location = new System.Drawing.Point(386, 141);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(86, 26);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Guardar";
            this.buttonSave.Click += new System.EventHandler(this.SaveDialog_Button);
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.Location = new System.Drawing.Point(509, 141);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(86, 26);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Cancelar";
            this.buttonClose.Click += new System.EventHandler(this.CloseDialog_Button);
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.AutoSize = true;
            this.textBoxLabel.Location = new System.Drawing.Point(13, 16);
            this.textBoxLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.Size = new System.Drawing.Size(90, 16);
            this.textBoxLabel.TabIndex = 1;
            this.textBoxLabel.Text = "Modificando : ";
            // 
            // textBoxEdit
            // 
            this.textBoxEdit.Location = new System.Drawing.Point(313, 74);
            this.textBoxEdit.Name = "textBoxEdit";
            this.textBoxEdit.Size = new System.Drawing.Size(357, 22);
            this.textBoxEdit.TabIndex = 2;
            // 
            // textBoxErrorLabel
            // 
            this.textBoxErrorLabel.AutoSize = true;
            this.textBoxErrorLabel.Location = new System.Drawing.Point(383, 55);
            this.textBoxErrorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.textBoxErrorLabel.Name = "textBoxErrorLabel";
            this.textBoxErrorLabel.Size = new System.Drawing.Size(212, 16);
            this.textBoxErrorLabel.TabIndex = 1;
            this.textBoxErrorLabel.Text = "Información sobre el campo actual";
            // 
            // FormModificarCampos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 472);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBoxLabel);
            this.Controls.Add(this.textBoxEdit);
            this.Controls.Add(this.textBoxErrorLabel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormModificarCampos";
            this.Text = "Modificando propiedades";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label textBoxLabel;
        private System.Windows.Forms.TextBox textBoxEdit;
        private System.Windows.Forms.Label textBoxErrorLabel;
    }
}