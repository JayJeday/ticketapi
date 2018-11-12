using Newtonsoft.Json.Linq;
using System.Web.Http;



namespace TicketCenterAPI.Controllers
{
    public class RoleController : ApiController
    {

        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;
                context.Configuration.LazyLoadingEnabled = false;

                var roles = context.sp_select_all_roles();

                if(roles == null)
                {
                    return NotFound();
                }

                //if we have a results get roles to json string 
                string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(roles);

                //parse string json array to json objects
                JArray jsonArray = JArray.Parse(jsonArrayString);

                /*             
                 *             get specific object
                dynamic data = JObject.Parse(jsonArray[0].ToString());
                */
                
                return Ok(jsonArray);
            }
        }


    }
}
