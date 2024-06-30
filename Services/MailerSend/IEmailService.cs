using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniDrive.Services.MailerSend
{    
    public interface IEmailService
    {
        Task<string> SendWelcomeEmail( string Email, string templatePath, Dictionary<string, string> placeholders);
    }
}