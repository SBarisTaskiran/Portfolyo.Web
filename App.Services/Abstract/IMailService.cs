using App.Models.DTO.Mail;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Abstract
{
    public interface IMailService
    {
        Task<Result> SendAsync(MailSendRequest request);
    }
}
