using DevHubWeb.Domains;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Service.Methods
{
    public class SendEmailService
    {
        private readonly IOptions<AppSettingsModel> _options;

        public SendEmailService(IOptions<AppSettingsModel> options)
        {
            this._options = options;
        }

        public async Task SendEmailAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Timeout = 900000;
                await client.ConnectAsync("smtp.gmail.com", 587, false).ConfigureAwait(false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_options.Value.SenderEmail, _options.Value.Password);

                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

            }
        }

        public async Task SendEmail(EmailParameters model)
        {
            var confirmedBy = "Admin";

            var htmlFilePath = "./email-html-templates/" + model.Template + ".html";
            var builder = new BodyBuilder
            {
                HtmlBody = File.ReadAllText(htmlFilePath)

                    .Replace("^Fullname^", (model.FullName))
                    .Replace("^DateTime^", model.DateTimeOfArrival)
                    .Replace("^Email^", model.Email)
                    .Replace("^Space^", model.AmenityName)
                    .Replace("^Message^", model.Message)
                    .Replace("^ContactNumber^", model.ContactNumber)                   
                    .Replace("^ReferenceNumber^", model.ReferenceNumber)
                    .Replace("^ConfirmedBy^", confirmedBy)
                    .Replace("^Link^", model.Link)
                    .Replace("^GuestCount^", model.GuestCount)
                    .Replace("^DatePeriod^", model.Period)
                    .Replace("^Duration^", model.Duration)
                     .Replace("^Rate^", "₱ " + model.Rate)
                    
            };
         
            var emailMessage = new MimeMessage
            {
                From = {
                    new MailboxAddress("Dev Partners",  _options.Value.SenderEmail)
                },
                To = {
                    new MailboxAddress("Dev Partners", model.IsAdmin ? model.Recipient : model.Email)
                },
                Subject = model.Subject,
                Body = builder.ToMessageBody()
            };

            await SendEmailAsync(emailMessage);
        }

        public EmailParameters BookLogForEmailParam(BookLogForEmailModel model, string template, string uri, bool isAdmin)
        {
            var emailParams = new EmailParameters {
                Subject = $"Dev Hub: New Booking Request!",
                FullName = model.FullName,
                Email = model.Email,
                AmenityName = model.AmenityName,
                Recipient = model.Recipient,
                Template = template,
                DateTimeOfArrival = model.DateTimeOfArrival.ToString(),
                IsFromDevhub = true,
                Message = !string.IsNullOrEmpty(model.Remarks) ? model.Remarks : "No Message",
                ContactNumber = string.Concat(model.ContactNumber),
                IsAdmin = isAdmin,
                Rate = model.RateValue.ToString(".00") + " - " + model.FrequencyDescription,
                GuestCount = model.Capacity,
                ReferenceNumber = model.BookingRefCode,
                Link = _options.Value.Protocol + uri + "/#!/Confirm?token=" + model.BookingRefCode + model.BookingID.ToString(),
                Period = model.DatePeriod,
                Duration = model.Duration
            };

            return emailParams;
        }
    }
}
