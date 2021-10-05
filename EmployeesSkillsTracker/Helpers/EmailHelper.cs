using EASendMail;
using EmployeesSkillsTracker.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesSkillsTracker.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        public EmailHelper() { }

        public void sendEmail(string subject, string to, string body)
        {
            try
            {
                SmtpMail smtpMail = new SmtpMail("TryIt");

                // Your email address
                smtpMail.From = "ranaa_saber@outlook.com";

                // Set recipient email address
                smtpMail.To = to;

                // Set email subject
                smtpMail.Subject = subject;

                // Set email body
                smtpMail.HtmlBody = body;

                // Hotmail/Outlook SMTP server address
                SmtpServer smtpServer = new SmtpServer("smtp.live.com");

                // If your account is office 365, please change to Office 365 SMTP server
                // SmtpServer oServer = new SmtpServer("smtp.office365.com");

                // User authentication should use your
                // email address as the user name.
                smtpServer.User = "userEmail";
                smtpServer.Password = "password";

                // use 587 TLS port
                smtpServer.Port = 587;

                // detect SSL/TLS connection automatically
                smtpServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.SendMail(smtpServer, smtpMail);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
        }
    }       
}
