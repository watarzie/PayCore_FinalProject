using EmailService.Configurations;
using EmailService.Domain;
using EmailService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _eMailConfig;

        public EmailSender(EmailConfiguration eMailConfig)
        {
            _eMailConfig = eMailConfig;
        }

        public async Task SendEmailAsync(Message message)
        {
            await SendEmailMessage(message);

        }

        public MailMessage CreateEmail(Message message)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(message.To);
            mail.From = new MailAddress(_eMailConfig.From);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;
            return mail;
        }

        private async Task SendEmailMessage(Message message)
        {

            MailMessage mail = CreateEmail(message);

            
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_eMailConfig.From, _eMailConfig.Password);
            smtp.Port = _eMailConfig.Port;
            smtp.Host = _eMailConfig.SMTPServer;
            smtp.EnableSsl = true;



            using (smtp)
            {
                await smtp.SendMailAsync(mail);
            }


        }

    }

}
