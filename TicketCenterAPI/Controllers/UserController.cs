
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
        [HttpGet]
        [Route("api/user/role")]
        public HttpResponseMessage GetUserRoleById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var user = context.sp_get_users_roles_by_id(id);

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

        [Route("api/user/register")]
        [HttpPost]
        public HttpResponseMessage RegisterUser([FromBody]TicketCenterAPI.Models.User user)
        {
            //insert into clients and users
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //by default ticket are open
                context.ins_client_registration(user.FirstName, user.LastName, user.Email, user.Password);

                var response = new HttpResponseMessage
                {
                    Content = new StringContent("Successfull saved"),
                    StatusCode = HttpStatusCode.OK
                };

                response.Content.Headers.Clear();
                response.Content.Headers.Add("Content-Type", "application/json");


                return Request.CreateResponse(HttpStatusCode.OK, "User registered succesfull");
            }
            

        }


        [HttpPost]
        public HttpResponseMessage AddEmployee(dynamic data)
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
                    bool inChat = data.InChat();

                    context.usp_tech_cat(categoryId,userId,inChat);

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
//update users roles
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
                    int roleId = data.RoleId;
                    string firstName = data.FirstName;
                    string lastName = data.LastName;

                    //update role
                    context.usp_role_user(id, roleId,firstName,lastName);


                    System.Diagnostics.Debug.WriteLine(roleId + " and " );

                    

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
