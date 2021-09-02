using System.Collections.Generic;

namespace Shared.Entities
{
    public class EmailMessage
    {
        public string SenderEmail { get; set; }
        public IEnumerable<string> ReceiverEmails { get; set; }
        public string Subject { get; set; }
        public string PlainText { get; set; }
        public string HtmlContent { get; set; }
    }
}
