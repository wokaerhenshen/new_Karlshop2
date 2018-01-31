using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace new_Karlshop.Data
{
    public class EmailHelper
    {
        public static void send(string fullname, string email, string phonenumber, string message)
        {


            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress("carolynho0422@gmail.com", "caro"));

            // From
            mailMsg.From = new MailAddress(email, "karl");

            // Subject and multipart/alternative Body
            mailMsg.Subject = "Customer Feedback";
            string Customer_Name = "Customer Name： "+ fullname;
            string phome_number = "Customer Number: " + phonenumber;
            string feedback = "Feedback: " + message;
            string html = @"<p>" +Customer_Name + "</p><br><p>"+ phome_number + "</p><br><p>"+ feedback + "</p>";

            //mailMsg.AlternateViews.Add(
            //        AlternateView.CreateAlternateViewFromString(Customer_Name,
            //                null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(html,
                    null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            SmtpClient smtpClient
            = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials
            = new System.Net.NetworkCredential("myaccount",
                                               "mypwd");
            smtpClient.Credentials = credentials;
            smtpClient.Send(mailMsg);
        }
    }
}
