using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicketCenterAPI.Controllers
{
    public class LoginController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage GetloginUser([FromBody]TicketCenterAPI.Models.User user)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //get all tickets
                var userlogin = context.sp_get_login(user.Email, user.Password);

                string result = "";

                if (userlogin != null)
                {
                    //convert list to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(userlogin);
                }
                else
                {
                    //user not found
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Credentials");

                }

                var response = new HttpResponseMessage
                {
                    Content = new StringContent(result),
                    StatusCode = HttpStatusCode.OK
                };

                response.Content.Headers.Clear();
                response.Content.Headers.Add("Content-Type", "application/json");


                return response;
            }


        }


    }
}
