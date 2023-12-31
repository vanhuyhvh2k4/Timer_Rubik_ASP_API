﻿using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Timer_Rubik.WebApp.Interfaces.Utils;
using System.Text.RegularExpressions;

namespace Timer_Rubik.WebApp.Utilities
{
    public class EmailUtils : IEmailUtils
    {
        private readonly string host;
        private readonly int port;
        private readonly string username;
        private readonly string password;

        public EmailUtils(IConfiguration config)
        {
            host = config.GetSection("Mail_Host").Value!;
            port = int.Parse(config.GetSection("Mail_Port").Value!);
            username = config.GetSection("Mail_Username").Value!;
            password = config.GetSection("Mail_Password").Value!;
        }

        public bool EmailValid(string email)
        {
            string regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email.Trim(), regex);
        }

        public void SendEmail(string toAddress, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(username));
            email.To.Add(MailboxAddress.Parse(toAddress.Trim()));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(host, port, SecureSocketOptions.StartTls);
            smtp.Authenticate(username, password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
