using Microsoft.Extensions.Options;
using ReportTracker.Models;
using System;
using System.Net;
using System.Net.Mail;

namespace ReportTracker.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly AppSettings appSettings;
        public EmailSender(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }
        private bool SendMail(string toAddress, string htmlString, string subject)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(appSettings.FromAddress);
                message.To.Add(new MailAddress(toAddress));
                message.Subject = appSettings.Subject + subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = appSettings.Port;
                smtp.Host = appSettings.Host; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(appSettings.UserId, appSettings.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool SendConformationEmail(string toAddress, string htmlBody)
        {
            return SendMail(toAddress, htmlBody, "Email Confirmation");
        }
    }
}
