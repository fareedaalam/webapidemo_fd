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
    public class CategorySubTopicController : ApiController
    {
        private readonly ICategorySubTopicInterface _categorysubtopicInterface;
        // Public constructor to initialize subject service instance
        public CategorySubTopicController(ICategorySubTopicInterface categorysubtopicInterface)
        {
            _categorysubtopicInterface = categorysubtopicInterface;
        }

        // GET api/categorysubtopic     
        public HttpResponseMessage Get()
        {
            var csts = _categorysubtopicInterface.GetAll();
            if (csts != null)
            {
                var cstEntities = csts as List<CategorySubTopicEntity> ?? csts.ToList();
                if (cstEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, cstEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Subtopic category  not found");
        }

        // GET api/categorysubtopic/5
        public HttpResponseMessage Get(Guid id)
        {
            var cst = _categorysubtopicInterface.GetById(id);
            if (cst != null)
                return Request.CreateResponse(HttpStatusCode.OK, cst);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No subtopic category found for this id");
        }

        public HttpResponseMessage Post([FromBody] CategorySubTopicEntity cstEntity)
        {
            try
            {
                FunctionResponse Resp = _categorysubtopicInterface.Create(cstEntity);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        public HttpResponseMessage Put( [FromBody] CategorySubTopicEntity cstEntity, Guid id)
        {
            try
            {
                FunctionResponse Resp = _categorysubtopicInterface.Update(id, cstEntity);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        // DELETE api/categorysubtopic/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _categorysubtopicInterface.Delete(id);
            return false;
        }

        [HttpPost]
        [Route("api/categorysubtopic/GetCategory")]
        public HttpResponseMessage GetCategory(CategorySubTopicEntity cstEntity)
        {
            try
            {
                FunctionResponse Resp = _categorysubtopicInterface.GetCategory(cstEntity);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<CategorySubTopicEntity> TranslationList = (IEnumerable<CategorySubTopicEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Error");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
                //throw;
            }
        }
    }
}
