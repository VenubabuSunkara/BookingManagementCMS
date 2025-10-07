using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Models
{
    public record SmtpSettings(string Host, int Port, string SenderName, string SenderEmail, string Username, string Password, bool EnableSsl);
}
