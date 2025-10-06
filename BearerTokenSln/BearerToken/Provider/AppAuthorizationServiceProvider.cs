using BearerToken.Repository;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BearerToken.Provider
{
    public class AppAuthorizationServiceProvider:OAuthAuthorizationServerProvider
    {
        public async override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserRepo repo = new UserRepo())
            {
                var user = repo.ValidateUser(context.UserName, context.Password);
                if(user==null)    
                {
                    context.SetError("Invalid_Grant", "UserName and password are Incorrect");
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                foreach (var item in user.Role.Split(','))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, item.Trim()));

                }
                context.Validated(identity);

            }
        }
        public async override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
    }
}