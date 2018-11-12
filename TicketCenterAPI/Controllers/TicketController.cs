using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace TicketCenterAPI.Controllers
{
    public class TicketController : ApiController
    {
        //get all  tickets
         [HttpGet]
        public IHttpActionResult GetAllTickets(int pageIndex, int pageSize)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //get all tickets
                var tickets = context.sp_select_all_tickets(pageIndex,pageSize);

                if (tickets == null)
                {
                    return NotFound();
                }

                //convert list to json
                string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(tickets);

                JArray jsonArray = JArray.Parse(jsonArrayString);

                return Ok(jsonArray);

            }
        }
        

        // Insert Employee  
        // POST api/Tickets
        [HttpPost]
        public IHttpActionResult AddTicket([FromBody]TicketCenterAPI.Models.Ticket ticket)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }

                context.Configuration.ProxyCreationEnabled = false;

                try
                {
                //by default ticket are open
                context.ins_tickets(ticket.Description, "", ticket.CategoryId, 1,ticket.ClientId);
                }
                catch
                {
                    return InternalServerError();
                }

                return Ok("Ticket succesfull created");

            }
        }


        [HttpPut]
        [ActionName("updateticket")]
        public IHttpActionResult UpdateTicket([FromBody]TicketCenterAPI.Models.Ticket ticket)
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
                    //todo change name of SP admin used too
                    // context.usp_ticket(ticket.TicketId,ticket.TechId,ticket.StatusId, ticket.Comment);
                    context.usp_ticket(ticket.TicketId, ticket.TechId, ticket.StatusId, ticket.Comment);
                }
                catch
                {
                    return NotFound();
                }

                return Ok("Update succesfull");
            }
        }

         
        
        [HttpGet]
        public IHttpActionResult GetTicketById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var ticket = context.sp_get_ticket_by_id(id);

                if (ticket == null)
                {
                    //convert list to json
                    return NotFound();              
                }

               string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(ticket);

                JArray jsonArray = JArray.Parse(jsonArrayString);

                return Ok(jsonArray);
            }
        }

        //Get the ticket by client id to get the ticket
        [HttpGet]
        [Route("api/user/ticket/client")]
        public IHttpActionResult GetTicketByClientId(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var ticket = context.sp_get_chat_ticket_by_client_id(id);

                if (ticket == null)
                {
                    return NotFound();
                }
                    //convert list to json
                  string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(ticket);

                JArray jsonArray = JArray.Parse(jsonArrayString);

                return Ok(jsonArray);
            }
        }



        [Authorize] // Require authenticated requests.
        [HttpGet]
        [Route("api/user/tickets/pagi")]
        public IHttpActionResult GetTicketByIdPagi(int pageIndex,int pageSize,int userId)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var ticket = context.sp_get_user_ticket_by_id_pagi(pageIndex,pageSize,userId);


                if (ticket == null)
                {
                    return NotFound();   
                }

               string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(ticket);

                JArray jsonArray = JArray.Parse(jsonArrayString);

                return Ok(jsonArray);

            }
        }
    } 
}
