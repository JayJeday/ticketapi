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
                    
                    var usersWithRoles = db.UserHasRoles.ToList();
                    var user = db.Users.ToList();
                    if (user != null)
                    {
                       
                        if (!string.IsNullOrEmpty(user.Where(u => u.Email == context.UserName && u.Password == context.Password).FirstOrDefault().Email))
                        {

                            //find user fix this
                             User loginUser = user.Where(u => u.Email == context.UserName && u.Password == context.Password).FirstOrDefault();

                            //add intities
                            identity.AddClaim(new Claim("username", context.UserName));
                            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

                            var clients = db.Clients.ToList();

                            Client client = clients.Where(x => x.UserId  == loginUser.id).FirstOrDefault();

                            Employee emp = db.Employees.Where(e => e.UserId == loginUser.id).FirstOrDefault();


                            string empId;

                            if (emp == null)
                            {
                                empId = "";
                            }else
                            {
                                empId = emp.Id + "";
                            }

                            string clientId;
                          
                            //************ if user is not a client  ***************** 
                            if (client == null)
                            {
                                clientId = "";
                            }
                            else
                            {
                                //give me the client
                                clientId = client.Id + "";
                            }


                            string roleId;

                            //get user role
                            UserHasRole userRole = usersWithRoles.Where(u => u.UserId == loginUser.id).FirstOrDefault();

                            //********** if user is a client role id is empty
                            if(userRole == null)
                            {
                                roleId = "";
                            }else
                            {
                                roleId = userRole.RoleId + "";
                            }         

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
                                    "ClientId",clientId
                                },
                                {
                                    "aRoleId", roleId 
                                },
                                {
                                    "aEmpId", empId
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


        //token end point override  
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
