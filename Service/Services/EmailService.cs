using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService()
        {
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("minhsangdiamondshop@gmail.com", "aaghgliopkmegwdi"),
                EnableSsl = true,
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("minhsangdiamondshop@gmail.com"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }

}
