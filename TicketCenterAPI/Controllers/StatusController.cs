using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicketCenterAPI.Controllers
{
    public class StatusController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllStatus()
        {
            //using method calls dispose to dispose any resourses after the  call
            //context access the data model from the database
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //    alternative to using store procedure
                var status = context.sp_select_all_status();

                //write the query to search the database linq 
                //   var query = (from c in context.Categories select c).ToList();

                if (status == null)
                {
                    return NotFound();
                   
                }


               string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(status);

                JArray jsonArray = JArray.Parse(jsonArrayString);

                return Ok(jsonArray);
            }

        }


        [HttpDelete]
        public IHttpActionResult DeleteStatus(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {

                    context.Configuration.ProxyCreationEnabled = false;
                try
                {
                    context.sp_delete_status_by_id(id);
                }
                catch
                {
                    return InternalServerError();
                }
               
                return Ok("Delete succesfull");

            }
        }



        //add status
        [HttpPost]
        [ActionName("addstatus")]
        public IHttpActionResult AddStatus([FromBody]TicketCenterAPI.Models.Status status)
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
                    //by default ticket are open
                    context.ins_status(status.StatusDesc);
                }
                catch
                {
                    return InternalServerError();
                }
              
                //TODO delete response
                return Ok("Succesfull created");
            }
        }

        [HttpGet]
        [Route("api/status/summary")]
        public IHttpActionResult GetStatusSumary()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var category = context.sp_summary_status();

                if (category == null)
                {
                    return NotFound();
                }
               //convert list to json
                  string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(category);

                JArray jsonArray = JArray.Parse(jsonArrayString);

                return Ok(jsonArray);
            }
        }




        [HttpPut]
        [ActionName("updatestatus")]
        public IHttpActionResult UpdateStatus([FromBody]TicketCenterAPI.Models.Status status)
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
                    context.usp_status(status.StatusId, status.StatusDesc);
                }
                catch
                {
                    return NotFound();
                }

                return Ok("Update succesfull");
            }
        }



        [HttpGet]
        public IHttpActionResult GetStatusById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var status = context.sp_get_status_by_id(id); 

                if (status == null)
                {
                    return NotFound();
                }

                //convert list to json
               string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(status);

                JArray jsonArray = JArray.Parse(jsonArrayString);

                return Ok(jsonArray);
            }
        }
    }

}

