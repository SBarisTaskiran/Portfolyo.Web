using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using App.Services.Abstract;
using App.Models.DTO.Mail;

namespace App.Services.Concrete
{
    public class EmailService(IServiceProvider serviceProvider)
       : AppServiceBase(serviceProvider)
           , IMailService
    {
        public async Task<Result> SendAsync(MailSendRequest request)
        {
            var validationResult = await ValidateModelAsync(request);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var host = Configuration["Email:Smtp:Host"];
            if (string.IsNullOrWhiteSpace(host))
            {
                return Result.Error("Host value is not valid in configuration.");
            }

            if (!int.TryParse(Configuration["Email:Smtp:Port"], out var port))
            {
                return Result.Error("Port value is not valid in configuration.");
            }

            var from = Configuration["Email:Smtp:Username"];
            if (string.IsNullOrWhiteSpace(from))
            {
                return Result.Error("From value is not valid in configuration.");
            }

            var password = Configuration["Email:Smtp:Password"];
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Error("Password value is not valid in configuration.");
            }

            if (!bool.TryParse(Configuration["Email:Smtp:EnableSsl"], out var enableSsl))
            {
                return Result.Error("EnableSsl value is not valid in configuration.");
            }

            using SmtpClient client = new(host, port)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = enableSsl
            };

            var email = new MailMessage
            {
                From = new MailAddress(from),
                Subject = request.Subject,
                Body = request.Body,
                IsBodyHtml = request.IsHtml,
            };

            foreach (var to in request.To)
            {
                email.To.Add(to);
            }

            if (request.Cc != null)
            {
                foreach (var cc in request.Cc)
                {
                    email.CC.Add(cc);
                }
            }

            if (request.Bcc != null)
            {
                foreach (var bcc in request.Bcc)
                {
                    email.Bcc.Add(bcc);
                }
            }

            await client.SendMailAsync(email);

            return Result.Success();
        }

      

    }
}
