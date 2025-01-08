using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Common.Mail
{
    public class MailSetting
    {
        public string Mail { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string Password { get; set; } = "";
        public string Host { get; set; } = "";
        public int Port { get; set; }
    }
}
