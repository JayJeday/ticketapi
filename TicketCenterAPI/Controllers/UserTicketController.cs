using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicketCenterAPI.Controllers
{
    public class UserTicketController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage GetUserTicketById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var userTicket = context;

                string result = "";

                if (userTicket != null)
                {
                    //convert list to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(userTicket);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something went wrong");
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
