
using System.Net;
using static Duende.IdentityServer.Models.IdentityResources;
using System;
using Document.Infrastructure;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Document.Repositories;
using Document.Services;
using Document.Models;
using System.Collections.Generic;

namespace Document.NewFolder1
{
    public class SendMailServices : ISendMailServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IDocumentService _documentService;
        private readonly INotifyRepository _notifyRepository;
        public SendMailServices(IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IDocumentService documentService, INotifyRepository notifyRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _documentService = documentService;
            _notifyRepository = notifyRepository;
        }
        public async Task<ResponseVM> SendEmail()
        {
            var test = new MimeMessage();
            IEnumerable<ViewDocument> documents = await _documentService.GetAllDocuments();
            DateTime todayDate = DateTime.Today;
            foreach (var document in documents)
            {
                var ExDate = document.ExpirationDate;
                TimeSpan diff1 = ExDate.Subtract(DateTime.Today);
                foreach (var notify in document.Notify)
                {
                    int notifyDate = notify.Days;
                    if (diff1.Days <= notify.Days && notify.Send == false)
                    {
                        await SendEmailAsync(notify.ContactModel.Email, notify.ContactModel.FirstName, document.Name, notify.Days, notify.ID);
                    }
                }
            }




            



            return new ResponseVM
            {
                message = "message send successfully"
            };
        }
        public string EMailTemplate(string template)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplate.html");
            string body = System.IO.File.ReadAllText(path);
            return body.ToString();
        }
        public async Task SendEmailAsync(string email, string contactName, string name, int days, Guid notifyID)
        {
            var _email = _configuration["EmailConfiguration:email"];
            var _epass = _configuration["EmailConfiguration:password"];

            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_email));
            //email.From.Add(MailboxAddress.Parse("document_reminder@mail.bg"));
            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject = $"Dear {contactName}, you have new expiring document";
            mail.Body = new TextPart(TextFormat.Plain) { Text = $"Dear {contactName}, document {name} expires after {days} days!" };

            
            var existingNotify = await _notifyRepository.GetByIdAsync(notifyID);
            existingNotify.Send = true;
            await _notifyRepository.UpdateAsync(existingNotify);

            //neshto s izchakvaneto
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_email, _epass);
            smtp.Send(mail);
            smtp.Disconnect(true);



        }
    }
}
