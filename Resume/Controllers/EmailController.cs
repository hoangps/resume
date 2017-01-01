using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Resume.Models.Email;

namespace Resume.Controllers
{
    public class EmailController : Controller
    {
        [HttpPost]
        public JsonResult Send(EmailViewModel email)
        {
            if (email == null) return Json("Invalid form data");

            var fromAddress = new MailAddress("dajkavn@gmail.com", "resume.hoangps.com");
            var toAddress = new MailAddress("hoangps18689@gmail.com");
            const string fromPassword = "hoangps@123!@#";
            var subject = "New message from " + email.Name + " (" + email.Email + ")" + " via resume.hoangps.com";
            var body = email.Message;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var mail = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {
                    smtp.Send(mail);
                    return Json("success");
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
        }
    }
}