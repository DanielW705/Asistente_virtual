namespace Asistente_virtual.Views
{
    partial class Ver_correos
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
            components = new System.ComponentModel.Container();
            txtbxMails = new TextBox();
            PushNotification = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // txtbxMails
            // 
            txtbxMails.Dock = DockStyle.Fill;
            txtbxMails.Location = new Point(0, 0);
            txtbxMails.Multiline = true;
            txtbxMails.Name = "txtbxMails";
            txtbxMails.ScrollBars = ScrollBars.Vertical;
            txtbxMails.Size = new Size(800, 450);
            txtbxMails.TabIndex = 0;
            // 
            // PushNotification
            // 
            PushNotification.Interval = 60000;
            PushNotification.Tick += PushNotification_Tick;
            // 
            // Ver_correos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtbxMails);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Ver_correos";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Correos Electronicos";
            Load += Ver_correos_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtbxMails;
        private System.Windows.Forms.Timer PushNotification;
    }
}