using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class MailCC
    {
        public string ToMail { get; set; }
        public List<string> CCMails { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
    }
}