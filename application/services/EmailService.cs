using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using campusLove.domain.ports;

namespace campusLove.application.services
{
    public class EmailService
    {
        
         private readonly IEmailRepository _emailService;

        public EmailService(IEmailRepository emailService)
        {
            _emailService = emailService;
        }

        public async Task Execute(string to, string subject, string body)
        {
            await _emailService.SendEmailAsync(to, subject, body);
        }
    }
}