using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithCoreDemo.Models.WebApi
{
    public class LoginModel : SuccessViewModel
    {
        public new JwtTokenViewModel Content { get; set; }
    }

    public class JwtTokenViewModel
    {
        public string Id { get; set; }
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
