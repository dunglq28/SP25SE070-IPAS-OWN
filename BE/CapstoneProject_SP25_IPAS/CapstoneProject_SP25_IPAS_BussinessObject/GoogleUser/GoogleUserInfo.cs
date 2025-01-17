using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapstoneProject_SP25_IPAS_BussinessObject.GoogleUserInfo
{
    public class GoogleUserInfo
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public Date? Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
