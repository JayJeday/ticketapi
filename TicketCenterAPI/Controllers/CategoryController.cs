using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;


                //    alternative to using store procedure
                var categories = context.sp_select_all_categories();
                  

                //write the query to search the database linq 
             //   var query = (from c in context.Categories select c).ToList();

                
                string result = "";

                if (categories != null)
                {
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

        //add status
        [HttpPost]
        [ActionName("addcategory")]
        public HttpResponseMessage AddCategory([FromBody]TicketCenterAPI.Models.Category category)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                //by default ticket are open
                context.ins_category(category.CategoryDesc);

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
        [ActionName("updatecategory")]
        public HttpResponseMessage UpdateCategory([FromBody]TicketCenterAPI.Models.Category category)
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
                    context.usp_category(category.CategoryId, category.CategoryDesc);
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
        public HttpResponseMessage GetCategoryById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var category = context.sp_get_category_by_id(id); 

                string result = "";

                if (category != null)
                {
                    //convert list to json
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(category);

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

    }

  
}
