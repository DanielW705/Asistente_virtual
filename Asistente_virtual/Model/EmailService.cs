using Microsoft.Office.Interop.Outlook;
using System.Reflection;


namespace Asistente_virtual.Model
{
    public class EmailService : IDisposable
    {
        Microsoft.Office.Interop.Outlook.Application MailApp { get; }

        NameSpace NameSpace { get; }

        public MAPIFolder MAPIFolder { get; }

        public EmailService()
        {
            MailApp = new Microsoft.Office.Interop.Outlook.Application();

            NameSpace = MailApp.GetNamespace("mapi");

            NameSpace.Logon(Missing.Value, Missing.Value, false, true);

            MAPIFolder = NameSpace.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
        }
        public List<MailItem> GetMailItems()
        {
            List<MailItem> mails = new List<MailItem>();
            Items MailItems = MAPIFolder.Items;
            foreach (MailItem mail in MailItems)
                mails.Add(mail);
            return mails;
        }

        public void Dispose()
        {
            NameSpace.Logoff();
        }

    }
}
