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
    public class CategoryController : ApiController
    {

        [HttpGet]
        [Route("api/category/all")]
        public IHttpActionResult GetAllCategories()
        {
            //using method calls dispose to dispose any resourses after the  call
            //context access the data model from the database
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;


                //    alternative to using store procedure
                var categories = context.sp_select_all_categories();
                  

                if(categories == null)
                {
                    return NotFound();
                }

                //write the query to search the database linq 
                //   var query = (from c in context.Categories select c).ToList();


                //if we have a results get categories to json
                string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(categories);

                JArray jsonArray = JArray.Parse(jsonArrayString);

                return Ok(jsonArray);
            }
              
        }

        //add status
        [HttpPost]
        [ActionName("addcategory")]
        public IHttpActionResult AddCategory([FromBody]TicketCenterAPI.Models.Category category)
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
                    context.ins_category(category.CategoryDesc);
                }
                catch
                {
                    return InternalServerError();
                }
      
                return Ok("Saved succesfull");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                try
                {
                    context.sp_delete_category_by_id(id);
                }
                catch
                {
                    return InternalServerError();
                }

                return Ok("Delete succesfull");

            }
        }

        [HttpPut]
        [ActionName("updatecategory")]
        public IHttpActionResult UpdateCategory([FromBody]TicketCenterAPI.Models.Category category)
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
                    context.usp_category(category.CategoryId, category.CategoryDesc);
                }
                catch
                {
                    return NotFound();
                }

                return Ok("Update succesfull");
            }
        }


        [HttpGet]
        [Route("api/category/summary")]
        public IHttpActionResult GetCategorySumary()
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var category=context.sp_summary_category();


                if (category == null)
                {
                    return NotFound();

                }

                //convert list to json
                string jsonArrayString = Newtonsoft.Json.JsonConvert.SerializeObject(category);

                JArray jsonObjects = JArray.Parse(jsonArrayString);

                return Ok(jsonObjects);
            }
        }



        [HttpGet]
        public IHttpActionResult GetCategoryById(int id)
        {
            using (var context = new TicketCenterAPI.Models.ticketcenterdbEntities1())
            {
                context.Configuration.ProxyCreationEnabled = false;

                var category = context.sp_get_category_by_id(id); 

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

    }

  
}
