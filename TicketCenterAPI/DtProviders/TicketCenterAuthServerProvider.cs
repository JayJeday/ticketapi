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
using TicketCenterAPI.Models;

namespace TicketCenterAPI.DtProviders
{

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
       //     context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (var db = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                if (db != null)
                {
                    var user = db.Users.ToList();
                    if (user != null)
                    {
                       
                        if (!string.IsNullOrEmpty(user.Where(u => u.Email == context.UserName && u.Password == context.Password).FirstOrDefault().Email))
                        {

                            //find user
                             User loginUser = user.Where(u => u.Email == context.UserName && u.Password == context.Password).First();

                            //add intities
                            identity.AddClaim(new Claim("username", context.UserName));
                            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));




                            var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                         {
                             "Email", context.UserName
                         },
                         {
                             "aId", loginUser.id + ""
                         },

                          {
                                "FirstName", loginUser.FirstName
                          },
                                {
                                    "LastName", loginUser.LastName
                                },
                                {
                                    "aRoleId", loginUser.RolesId + ""
                                }
                    });

                            var ticket = new AuthenticationTicket(identity, props);
                            context.Validated(ticket);


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


        //token end point 
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

    }

}
