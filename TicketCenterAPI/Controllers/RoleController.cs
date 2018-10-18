using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicketCenterAPI.Controllers
{
    public class RoleController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage GetRoles()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var roles = context.sp_select_all_roles();

                string result = "";

                if (roles != null)
                {
                    //if we have a results get categories to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(roles);
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
