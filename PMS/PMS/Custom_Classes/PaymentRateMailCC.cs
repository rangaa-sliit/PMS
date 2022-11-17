using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class PaymentRateMailCC
    {
        public int FacultyId { get; set; }
        public string ToUsername { get; set; }
        public string ToMail { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
    }
}