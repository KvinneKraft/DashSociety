
// Author: Dashie
// Version: 1.0

using System;
using System.Net;
using System.Net.Mail;

namespace MailSpammer
{
    public class DashSMTP
    {
        public readonly static string dash_subject = ("Dash Society");

        public bool send_mail(string destination, string body, string sender_email, string sender_password, bool verbose_output)
        {
            try
            {
                using (SmtpClient smtp_client = new SmtpClient())
                {
                    using (MailMessage message = new MailMessage())
                    {
                        var credentials = new NetworkCredential(sender_email, sender_password);

                        smtp_client.UseDefaultCredentials = false;
                        smtp_client.Credentials = credentials;
                        smtp_client.EnableSsl = true;
                        smtp_client.Timeout = 5000;
                        smtp_client.Host = "smtp.gmail.com";
                        smtp_client.Port = 587;

                        MailAddress mail_address = new MailAddress(sender_email);

                        message.From = mail_address;
                        message.IsBodyHtml = true;

                        message.Subject = $"{dash_subject} #{new Random().Next(99999).ToString()}";
                        message.Body = $"{body}";

                        message.To.Add(destination);
                        smtp_client.Send(message);

                        return true;
                    };
                };
            }

            catch (Exception e)
            {
                if (verbose_output)
                {
                    Console.WriteLine(e.ToString());
                };

                return false;
            };
        }
    };
};