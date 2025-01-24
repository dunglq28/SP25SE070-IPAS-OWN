using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_BussinessObject.GoogleUser
{
    public class GoogleTokenInfo
    {
        public string Issuer { get; set; } // "iss"
        public string Audience { get; set; } // "aud"
        public string Subject { get; set; } // "sub" (User ID)
        public string Email { get; set; } // "email"
        public bool EmailVerified { get; set; } // "email_verified"
        public string Name { get; set; } // "name"
        public string Picture { get; set; } // "picture"
        public string GivenName { get; set; } // "given_name"
        public string FamilyName { get; set; } // "family_name"
        public long IssuedAt { get; set; } // "iat" (timestamp)
        public long Expiration { get; set; } // "exp" (timestamp)
    }
}
