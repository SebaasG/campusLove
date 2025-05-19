using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace campusLove.domain.ports
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}