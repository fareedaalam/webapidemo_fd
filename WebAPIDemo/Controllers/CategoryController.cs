using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    [ApiAuthenticationFilter]
    public class CategoryController : ApiController
    {
        private readonly ICategoryInterface _categoryInterface;
        // Public constructor to initialize category service instance
        public CategoryController(ICategoryInterface categoryInterface)
        {
            _categoryInterface = categoryInterface;
        }

        // GET api/category    
        /// <summary>
        /// Get All Category 
        /// </summary>
        /// <returns>Json Result </returns>
        public HttpResponseMessage Get()
        {
            var category = _categoryInterface.GetAllCategory();
            if (category != null)
            {
                var categoryEntities = category as List<CategoryEntity> ?? category.ToList();
                if (categoryEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, categoryEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category not found");
        }
        // GET api/category/5
        public HttpResponseMessage Get(Guid id)
        {
            var category = _categoryInterface.GetCategoryById(id);
            if (category != null)
                return Request.CreateResponse(HttpStatusCode.OK, category);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No category found for this id");
        }

        // POST api/category

        public Guid Post([FromBody]CategoryEntity categoryEntity)
        {
            return _categoryInterface.CreateCategory(categoryEntity);
        }

        // PUT api/category/5
        public bool Put(Guid id, [FromBody]CategoryEntity categoryEntity)
        {
            if (id != null)
            {
                return _categoryInterface.UpdateCategory(id, categoryEntity);
            }
            return false;
        }

        // DELETE api/category/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _categoryInterface.DeleteCategory(id);
            return false;
        }
    }
}
