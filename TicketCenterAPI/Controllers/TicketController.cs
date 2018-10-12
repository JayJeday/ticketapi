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
        [ActionName("getalltickets")]
        public HttpResponseMessage GetAllTickets()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //get all tickets
                var tickets = context.sp_select_all_tickets();

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
        [ActionName("addticket")]
        public HttpResponseMessage AddTicket([FromBody]TicketCenterAPI.Models.Ticket ticket)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //by default ticket are open
                context.ins_tickets(ticket.Description, "", ticket.CategoryId, 1);

                var response = new HttpResponseMessage
                {
                    Content = new StringContent("Successfull saved"),
                    StatusCode = HttpStatusCode.OK
                };

                response.Content.Headers.Clear();
                response.Content.Headers.Add("Content-Type", "application/json");


                return response;
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
                     context.usp_techician_ticket(ticket.TicketId,ticket.StatusId,ticket.Comment);
                }
                catch(DbUpdateConcurrencyException ex)
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
