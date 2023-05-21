using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IMailService
    {
        Task SendMailAsync(string subject, string body, string to, string from = null);
    }
}