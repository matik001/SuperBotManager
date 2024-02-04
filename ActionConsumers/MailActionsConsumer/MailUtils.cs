using MailKit.Net.Imap;
using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Search;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace MailActionsConsumer
{
    class MailInfo
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    class MailUtils
    {
        public static MimeMessage? ReadOne(ReadMailActionInput info)
        {
            using(var client = new ImapClient())
            {
                client.Connect(info.Host, int.Parse(info.Port), info.SSL);

                client.Authenticate(info.Login, info.Password);

                var inbox = client.GetFolder(info.MailBox);
                inbox.Open(FolderAccess.ReadWrite);
                SearchQuery query;
                if(string.IsNullOrEmpty(info.SearchPhrase))
                    query = SearchQuery.NotSeen;
                else
                    query = SearchQuery.NotSeen.And(SearchQuery.SubjectContains(info.SearchPhrase));

                var uids = inbox.Search(query);
                if(uids.Count == 0)
                {
                    client.Disconnect(true);
                    return null;
                }
                var id = uids.First();
                var message = inbox.GetMessage(id);
                inbox.AddFlags(id, MessageFlags.Seen, true);

                client.Disconnect(true);

                return message;
            }
        }

  
        public static void Send(MailSendActionInput info)
        {
            using(var client = new SmtpClient())
            {
                client.Connect(info.Host, int.Parse(info.Port), info.SSL);
                client.Authenticate(info.Login, info.Password);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(info.Login, info.Login));
                message.To.Add(new MailboxAddress("", info.To));
                message.Subject = info.Subject;
                message.Body = new TextPart("plain") { Text = info.Body};
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
