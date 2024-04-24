namespace Asistente_virtual
{
    partial class Form_asistente
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtbx_prueba = new TextBox();
            SuspendLayout();
            // 
            // txtbx_prueba
            // 
            txtbx_prueba.Dock = DockStyle.Fill;
            txtbx_prueba.Location = new Point(0, 0);
            txtbx_prueba.Multiline = true;
            txtbx_prueba.Name = "txtbx_prueba";
            txtbx_prueba.Size = new Size(800, 450);
            txtbx_prueba.TabIndex = 0;
            // 
            // Form_asistente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtbx_prueba);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form_asistente";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Asistente";
            FormClosing += Form_asistente_FormClosing;
            Load += Form_asistente_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TextBox txtbx_prueba;
    }
}
