
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Infrastructure;
using TicketCenterAPI.Models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

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

        [HttpGet]
        public HttpResponseMessage GetUserById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var user = context.sp_get_user_by_id(id);

                string result = "";

                if (user != null)
                {
                    //convert list to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(user);

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
        public HttpResponseMessage UpdateUser(dynamic data)
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

                    //alternative => req.Content.ReadAsStringAsync().Result;

                    //object then unbox to int
                    int id = data.id;
                    object roleId = data.RoleId;
                    object categoryId = data.CategoryId;

                    //if roleId is not null => role was selected to update
                    if(data.RoleId != null)
                    {
                        int i = data.RoleId;
                      context.usp_user(id,null,i);
                    }
                    else if (categoryId != null)
                    {
                        //category was updated pass null to role
                        int i = data.CategoryId;
                        context.usp_user(id,i,null);
                    }
                   


                    System.Diagnostics.Debug.WriteLine(roleId + " and " + categoryId);

                    

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
