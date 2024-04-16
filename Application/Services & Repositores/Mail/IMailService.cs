using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services___Repositores.Mail
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
