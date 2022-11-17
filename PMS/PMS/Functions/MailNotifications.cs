using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace PMS.Functions
{
    public class MailNotifications
    {
        public void SendEmailToSinglePerson(string toMail, List<string> ccMails, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //mail.To.Add(toMails);
                mail.To.Add(toMail);

                foreach (string ccMail in ccMails)
                {
                    mail.CC.Add(ccMail);
                }

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}