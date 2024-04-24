using Asistente_virtual.Model;
using Microsoft.Office.Interop.Outlook;
using System.Speech.Synthesis;

namespace Asistente_virtual.Views
{
    public partial class Ver_correos : Form
    {
        SpeechSynthesizer speecher;
        public Ver_correos(SpeechSynthesizer speecher)
        {
            this.speecher = speecher;
            InitializeComponent();
        }

        private void Ver_correos_Load(object sender, EventArgs e)
        {
            EmailService mail = new EmailService();
            List<MailItem> correos = mail.GetMailItems();
            for (int i = 0; i < correos.Count; i++)
            {
                txtbxMails.Text += $"Numero de Correo {i + 1}\r\nRemitente: {correos[i].SenderEmailAddress}\r\nAsunto: {correos[i].Subject}\r\n\r\n";
            }
            mail.MAPIFolder.Items.ItemAdd += Inbox_ItemAdd;
        }
        private void Inbox_ItemAdd(object Item)
        {
            if (Item is MailItem mailItem)
            {
                string message = $"Nuevo Correo\r\n Remitente: {mailItem.SenderEmailAddress}\r\nAsunto: {mailItem.Subject}";
                Invoke((MethodInvoker)(() => txtbxMails.Text += message));
                speecher.Speak(message);
            }
        }
    }
}
