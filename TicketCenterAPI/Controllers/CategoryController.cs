using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TicketCenterAPI.Controllers
{
    public class CategoryController : ApiController
    {

        [HttpGet]
        [ActionName("getallcategories")]
        public HttpResponseMessage GetAllCategories()
        {
            //using method calls dispose to dispose any resourses after the  call
            //context access the data model from the database
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;


                //    alternative to using store procedure
                var categories = context.sp_select_all_categories();
                  

                //write the query to search the database linq 
             //   var query = (from c in context.Categories select c).ToList();

                
                string result = "";

                if (categories != null)
                {
                    //TODO modify Json

                    //if we have a results get categories to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(categories);
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
