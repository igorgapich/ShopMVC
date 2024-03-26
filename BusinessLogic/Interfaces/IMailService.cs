using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IMailService
    {
        //send message to email user
        Task SendMailAsync(string subject, string body, string toSend, string? fromSend = null);
    }
}
