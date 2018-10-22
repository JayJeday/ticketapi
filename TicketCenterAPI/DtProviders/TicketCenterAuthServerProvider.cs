using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Cors;


namespace TicketCenterAPI.DtProviders
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TicketCenterAuthServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        // validate the username and password 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (var db = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                if (db != null)
                {
                    var user = db.Users.ToList();
                    if (user != null)
                    {
                       

                        if (!string.IsNullOrEmpty(user.Where(u => u.Email == context.UserName && u.Password == context.Password).FirstOrDefault().Email))
                        {
                           identity.AddClaim(new Claim("Age", "16"));

                            //Example of how to validate a claim
                           var props = new AuthenticationProperties(new Dictionary<string, string>
                            {
                                {
                                   "userdisplayname", context.UserName
                                },
                               {
                                     "role", "admin"
                               }
                             });


                           var ticket = new AuthenticationTicket(identity, props);

                           context.Validated(identity);
                            
                        }
                        else
                        {
                            context.SetError("invalid_grant", "Provided username and password is incorrect");
                            context.Rejected();
                        }
                    }
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    context.Rejected();
                }
                return;
            }



        }
    }

}
