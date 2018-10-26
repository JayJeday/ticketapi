using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Infrastructure;

namespace TicketCenterAPI.Controllers
{
    public class TicketController : ApiController
    {

        //get all  tickets
    
         [HttpGet]
        public HttpResponseMessage GetAllTickets(int pageIndex, int pageSize)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //get all tickets
                var tickets = context.sp_select_all_tickets(pageIndex,pageSize);

                string result = "";

                if (tickets != null)
                {
                    //convert list to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(tickets);
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
        

        // Insert Employee  
        // POST api/Tickets
        [HttpPost]
        public HttpResponseMessage AddTicket([FromBody]TicketCenterAPI.Models.Ticket ticket)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //by default ticket are open
                context.ins_tickets(ticket.Description, "", ticket.CategoryId, 1,ticket.ClientId);

                var response = new HttpResponseMessage
                {
                    Content = new StringContent("Successfull saved"),
                    StatusCode = HttpStatusCode.OK
                };

                response.Content.Headers.Clear();
                response.Content.Headers.Add("Content-Type", "application/json");


                return Request.CreateResponse(HttpStatusCode.OK, "Ticket succesfull created");
            }
        }


        [HttpPut]
        [ActionName("updateticket")]
        public HttpResponseMessage UpdateTicket([FromBody]TicketCenterAPI.Models.Ticket ticket)
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
                    // context.usp_ticket(ticket.TicketId,ticket.TechId,ticket.StatusId, ticket.Comment);
                    context.usp_ticket(ticket.TicketId, ticket.TechId, ticket.StatusId, ticket.Comment);
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

         
        
        [HttpGet]
        public HttpResponseMessage GetTicketById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var ticket = context.sp_get_ticket_by_id(id);
                
                string result = "";

                if (ticket != null)
                {
                    //convert list to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(ticket);
                    
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
        [Route("api/user/tickets/pagi")]
        public HttpResponseMessage GetTicketByIdPagi(int pageIndex,int pageSize,int userId)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var ticket = context.sp_get_user_ticket_by_id_pagi(pageIndex,pageSize,userId);

                string result = "";

                if (ticket != null)
                {
                    //convert list to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(ticket);

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
