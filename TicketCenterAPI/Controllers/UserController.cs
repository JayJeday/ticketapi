
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
        public IHttpActionResult GetAllUser()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //get all tickets
                var users = context.sp_select_all_users();

                if (users == null)
                {
                    //convert list to json
                    return NotFound();            
                }

                string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(users);
                
                JArray jsonObjects = JArray.Parse(jsonArrayString);

                return Ok(jsonObjects);
            }


        }

        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var user = context.sp_get_user_by_id(id);

                if (user == null)
                {
                    return NotFound();

                }

             //convert list to json
             string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(user);

             JArray jsonArray = JArray.Parse(jsonArrayString);

             return Ok(jsonArray);

            }
        }

        [HttpGet]
        [Route("api/user/role")]
        public IHttpActionResult GetUserRoleById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var user = context.sp_get_users_roles_by_id(id);

                if (user == null)
                {
                    return NotFound();
                }

                //convert list to json
                string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(user);

                JArray jsonObjects = JArray.Parse(jsonArrayString);
                return Ok(jsonObjects);
            }
        }

        [Route("api/user/register")]
        [HttpPost]
        public IHttpActionResult RegisterUser([FromBody]TicketCenterAPI.Models.User user)
        {
            //insert into clients and users
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                     context.ins_client_registration(user.FirstName, user.LastName, user.Email, user.Password);
                }
                catch 
                {
                    return InternalServerError();
                }
                //by default ticket are open

                return Ok("User registered succesfull");
            }
            
        }


        [HttpPost]
        public IHttpActionResult AddEmployee(dynamic data)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string firstname = data.FirstName;
                string lastname = data.LastName;
                string email = data.Email;
                int roleId = data.RoleId;

                try
                {

                    if (data.CategoryId == null)
                    {
                        context.ins_user(firstname, lastname, email, roleId, null);

                        return InternalServerError();
                    }
                    else
                    {
                        int categoryId = data.CategoryId;
                        context.ins_user(firstname, lastname, email, roleId, categoryId);
                    }
                }

                catch
                {
                    return InternalServerError();
                }
                    
               return Ok("User succesfull created");
               
            }

        }

        [Route("api/user/techs")]
        [HttpGet]
        public IHttpActionResult GetAllTechs()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var techs = context.sp_get_all_technician();


                if (techs == null)
                {
                    return NotFound();

                }

                //convert list to json
               string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(techs);

                JArray jsonArray = JArray.Parse(jsonArrayString);

                return Ok(jsonArray);
            }
        }


        [Route("api/user/tech/cat")]
        [HttpPut]
        public IHttpActionResult UpdateTechCat(dynamic data)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {

                    //alternative => req.Content.ReadAsStringAsync().Result;

                    //object then unbox to int
                    int userId = data.UserId;
                    
                    if(data.CategoryId == null)
                    {

                        bool inChat = data.InChat;
                        context.usp_tech_cat(null,userId,inChat);

                    }

                    else
                    {

                        int categoryId = data.CategoryId;
                        context.usp_tech_cat(categoryId, userId, null);

                    }

                 //   System.Diagnostics.Debug.WriteLine(roleId + " and " + categoryId);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    return NotFound();
                }

                return Ok("Update succesfull");
            }
        }

        //update users roles
        [HttpPut]
        public IHttpActionResult UpdateUser(dynamic data)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {

                context.Configuration.ProxyCreationEnabled = false;

                //is the model with binding is incorrect
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
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
                    return NotFound();
                }

                return Ok("Update succesfull");   
            }
        }

    }

}
