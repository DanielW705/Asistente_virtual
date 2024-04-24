using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using static Google.Apis.Requests.BatchRequest;
using Message = Google.Apis.Gmail.v1.Data.Message;


namespace Asistente_virtual.Model
{
    public class GmailServices
    {
        private string[] Scopes = { GmailService.Scope.GmailReadonly };

        public DateTime lasProcessedTime = DateTime.MinValue ;

        private UserCredential credential { get; }

        public GmailService service { get; }

        public Google.Apis.Gmail.v1.UsersResource.MessagesResource.ListRequest listRequest { get; set; }

        public ListMessagesResponse response { get; set; }

        private WatchRequest watchRequest { get; }

        private UsersResource.WatchRequest watch { get; set; }

        private WatchResponse watchResponse { get; set; }


        public GmailServices()
        {
            using (FileStream stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                CancellationToken.None,
                    new FileDataStore("token.json", true)).Result;
            }
            service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Gmail API .NET QUickstart"
            });

            listRequest = service.Users.Messages.List("me");
            listRequest.LabelIds = "INBOX";
            listRequest.IncludeSpamTrash = false;

            watchRequest = new WatchRequest()
            {
                TopicName = "projects/gmail-auth-421220/topics/gmail-auth", // Cambia estos valores por los tuyos
                LabelIds = new List<string>() { "INBOX" },
                LabelFilterAction = "INCLUDE"
            };
            watch = service.Users.Watch(watchRequest, "me");
            watchResponse = watch.Execute();
        }


        public IList<Message> GetAllInboxMessages()
        {
            IList<Message> messages = new List<Message>();
            response = listRequest.Execute();
            foreach (Message message in response.Messages)
                messages.Add(service.Users.Messages.Get("me", message.Id).Execute());
            return messages;
        }
    }
}
