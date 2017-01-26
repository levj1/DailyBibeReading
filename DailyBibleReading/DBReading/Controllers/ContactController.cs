using DBReading.Models;
using DBReading.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Text;

namespace DBReading.Controllers
{
    public class ContactController : Controller
    {
        // GET: Conact
        public ActionResult ContactUs()
        {
            var contact = new Contact();
            return View("ContactUs", contact);
        }


        [HttpPost]
        public ActionResult ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                ContactViewModel vm = new ContactViewModel
                {
                    Contact = contact
                };

                // Send email
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    msg.From = new MailAddress(contact.Email.ToString());
                    StringBuilder sb = new StringBuilder();
                    msg.To.Add("bsn.progreat@gmail.com");
                    msg.Subject = "Contact us";
                    msg.IsBodyHtml = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new System.Net.NetworkCredential("**", "**");
                    sb.Append("First name: " + contact.FirstName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Last name: " + contact.LastName);
                    sb.Append(Environment.NewLine);
                    sb.Append("Email: " + contact.Email);
                    sb.Append(Environment.NewLine);
                    sb.Append("Comments: " + contact.Comment);
                    msg.Body = sb.ToString();
                    smtp.Send(msg);
                    msg.Dispose();
                    return View("Success", vm);
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
            return View(contact);
        }
    }
}