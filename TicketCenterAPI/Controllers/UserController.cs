
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Infrastructure;
using TicketCenterAPI.Models;
using System.Diagnostics;

namespace TicketCenterAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllUser()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //get all tickets
                var users = context.sp_select_all_users();

                string result = "";

                if (users != null)
                {
                    //convert list to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(users);
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

        [HttpPost]
        [ActionName("loggin")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> Loggin()
        {
            string requestBody;
            string cred;
            using(var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                try
                {
                     requestBody = await Request.Content.ReadAsStringAsync();
                     cred = Newtonsoft.Json.JsonConvert.DeserializeObject <string> (requestBody);

                    Trace.WriteLine(requestBody);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, cred);
        } 

       
        [HttpPut]
        [ActionName("updateuser")]
        public HttpResponseMessage UpdateUser()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {

                context.Configuration.ProxyCreationEnabled = false;

                //is the model with binding is incorrect
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                try
                {
                    //todo change name of SP admin used too
                    
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }
                var response = new HttpResponseMessage
                {
                    Content = new StringContent("Successfull saved"),
                    StatusCode = HttpStatusCode.OK
                };

                return Request.CreateResponse(HttpStatusCode.OK, "Update succesfull");
            }
        }

    }

}
