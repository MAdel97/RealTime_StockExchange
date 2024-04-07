using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using AutoMapper.Configuration;

namespace AcademicCoursesCRUD.Common
{
    public class Email
    {
        private readonly IConfiguration _config;
        public Email(IConfiguration config)
        {
            _config = config;
        }

        public static async Task<bool> SendMail(string fromemail, string frompassword, string toemail, string emailsubject, string emailbody)
        {
            
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(fromemail);
            mail.To.Add(toemail);
            mail.Subject = emailsubject;
            mail.Body = emailbody;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(fromemail, frompassword);
            SmtpServer.EnableSsl = true;
            try
            {
                await SmtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
