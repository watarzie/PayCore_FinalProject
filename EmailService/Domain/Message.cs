using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Domain
{
    public class Message
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }


        public Message(string to, string subject, string content)
        {
            To = to; Subject = subject; Body = content;
        }
    }
}
