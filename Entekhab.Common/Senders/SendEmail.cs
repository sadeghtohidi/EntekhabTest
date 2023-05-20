using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Rasad.Common.Senders
{
    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //mail.From = new MailAddress("fsfardadd@gmail.com", "فرداد");
            //mail.To.Add(to);
            //mail.Subject = subject;
            //mail.Body = body;
            //mail.IsBodyHtml = true;

            ////System.Net.Mail.Attachment attachment;
            //// attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            //// mail.Attachments.Add(attachment);

            //SmtpServer.Port = 587;

            //SmtpServer.Credentials = new System.Net.NetworkCredential("fsfardadd@gmail.com", "fardad1397@");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);

            try
            {
                const string fromEmail = "fardad@alohtech.ir";
                var message = new MailMessage
                {
                    From = new MailAddress(fromEmail, "فرداد"),
                    To = { to },
                    Subject = subject,
                    Body = body,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                };
                using (SmtpClient smtpClient = new SmtpClient("webmail.alohtech.ir"))
                {
                    smtpClient.Credentials = new NetworkCredential("fardad@alohtech.ir", "Fardad123456");
                    smtpClient.Port = 25;
                    smtpClient.EnableSsl = false;
                    smtpClient.Send(message);
                }
            }
            catch (Exception excep)
            {
                //ignore it or you can retry .
            }

        }

    }
}
