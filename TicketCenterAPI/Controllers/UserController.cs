
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public HttpResponseMessage AddUser(dynamic data)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;


                string firstname = data.FirstName;
                string lastname = data.LastName;
                string email = data.Email;
                int roleId = data.RoleId;
                
                if(data.CategoryId == null)
                {
                context.ins_user(firstname,lastname,email,roleId,null);
                }
                else
                {
                    int categoryId = data.CategoryId;
                    context.ins_user(firstname, lastname, email, roleId, categoryId);
                }
                //by default ticket are open
                

                var response = new HttpResponseMessage
                {
                    Content = new StringContent("Successfull saved"),
                    StatusCode = HttpStatusCode.OK
                };

                response.Content.Headers.Clear();
                response.Content.Headers.Add("Content-Type", "application/json");


                return Request.CreateResponse(HttpStatusCode.OK, "User succesfull created");
            }
        }

        [Route("api/user/techs")]
        [Authorize]
        public HttpResponseMessage GetAllTechs()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var techs = context.sp_get_all_technician();

                string result = "";

                if (techs != null)
                {
                    //convert list to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(techs);

                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Result Unavailable");
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

        [Route("api/user/tech/cat")]
        [HttpPut]
        [Authorize]
        public HttpResponseMessage UpdateTechCat(dynamic data)
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
                    int userId = data.UserId;
                    int categoryId = data.CategoryId;

                    context.usp_tech_cat(categoryId,userId);

                 //   System.Diagnostics.Debug.WriteLine(roleId + " and " + categoryId);



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

        [HttpPut]
        [Authorize]
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
