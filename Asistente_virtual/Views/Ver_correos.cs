using Asistente_virtual.Model;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System.Speech.Synthesis;
using static Google.Apis.Requests.BatchRequest;
using Message = Google.Apis.Gmail.v1.Data.Message;


namespace Asistente_virtual.Views
{
    public partial class Ver_correos : Form
    {
        private SpeechSynthesizer speecher;

        private GmailServices gmailServices = new GmailServices();
        public Ver_correos(SpeechSynthesizer speecher)
        {
            this.speecher = speecher;
            InitializeComponent();
            PushNotification.Start();
        }


        private void Ver_correos_Load(object sender, EventArgs e)
        {
            IList<Message> messages = gmailServices.GetAllInboxMessages();
            foreach (Message message in messages)
            {
                txtbxMails.Text += $"Fecha de recibido: {DateTime.FromFileTime((long)message.InternalDate)}\r\nRemitente: {message.Payload.Headers.FirstOrDefault(h => h.Name == "From").Value}\r\nAsunto: {message.Payload.Headers.FirstOrDefault(h => h.Name == "Subject").Value}\r\n";
            }
        }



        private void PushNotification_Tick(object sender, EventArgs e)
        {
            gmailServices.listRequest = gmailServices.service.Users.Messages.List("me");
            gmailServices.listRequest.Q = "label:inbox";
            gmailServices.response = gmailServices.listRequest.Execute();
            if (gmailServices.response.Messages != null && gmailServices.response.Messages.Count > 0)
            {
                speecher.Speak("Nuevo correo");
                txtbxMails.Text = string.Empty;
                foreach (Message message in gmailServices.response.Messages)
                {
                    Message msg = gmailServices.service.Users.Messages.Get("me", message.Id).Execute();
                    txtbxMails.Text += $"Fecha de recibido: {DateTime.FromFileTime((long)msg.InternalDate)}\r\nRemitente: {msg.Payload.Headers.FirstOrDefault(h => h.Name == "From").Value}\r\nAsunto: {msg.Payload.Headers.FirstOrDefault(h => h.Name == "Subject").Value}\r\n";

                }
            }
        }
    }
}
