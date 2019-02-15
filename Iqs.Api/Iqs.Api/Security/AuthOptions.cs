using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iqs.Api.Security
{
    public class AuthOptions
    {
        public const string ISSUER = "ItransitionQualificationSystem";
        public const string AUDIENCE = "http://localhost";
        public const int LIFETIME = 1;
        const string KEY = "secretkey141private";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
