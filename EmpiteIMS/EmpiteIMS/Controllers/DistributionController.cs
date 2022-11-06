using EmpiteIMS.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace EmpiteIMS.Controllers
{
    public class DistributionController : Controller
    {
        List<string> mails = new List<string>();

        private readonly IConfiguration _configuration;
        private readonly EmpiteIMSDBContext empiteIMSDBContext;
        public DistributionController(EmpiteIMSDBContext empiteIMSDBContext, IConfiguration configuration)
        {
            this.empiteIMSDBContext = empiteIMSDBContext;
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> SendMails()
        {
            var merchants = await empiteIMSDBContext.Merchants.ToListAsync();
            List<string> merchantsEmails = merchants.Select(x => x.MerchantEmail).ToList();
            //return (IActionResult)merchantsEmails;

            var fromEmail = "admin@gmail.com";
            var mailTitle = "Inventory Summary";
            var mailSubject = "Empite";


            var inventoryDetails = await empiteIMSDBContext.Inventory.ToListAsync();
            var mailBody = inventoryDetails;

            foreach (var merchantEmail in merchantsEmails)
            {
                MailMessage message = new MailMessage(new MailAddress(fromEmail, mailTitle), new MailAddress(merchantEmail));

                message.Subject = mailSubject;
                message.Body = mailBody.ToString();

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.office365.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
                credential.UserName = fromEmail;
                credential.Password = _configuration.GetValue<string>("Passwords:MailPassword");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;

                smtp.Send(message);
            }
            return View();
        }

    }
}
