using AngularWithCoreDemo.Models.WebApi;
using AngularWithCoreDemo.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AngularWithCoreDemo.Helpers
{
    public class Tokens
    {
        public static async Task<JwtTokenViewModel> GenerateJwt(
            ClaimsIdentity identity,
            IJwtFactory jwtFactory,
            JwtIssuerOptions jwtOptions
        )
        {
            JwtTokenViewModel response = new JwtTokenViewModel
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                AuthToken = await jwtFactory.GenerateEncodedToken(identity.Claims.Single(c => c.Type == "id").Value, identity),
                ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds,
                RefreshToken = Guid.NewGuid().ToString()
            };

            return response;
        }
    }
}
