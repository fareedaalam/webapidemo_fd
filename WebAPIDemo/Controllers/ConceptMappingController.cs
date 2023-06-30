using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    [ApiAuthenticationFilter]
    public class ConceptMappingController : ApiController
    {
        private readonly IConceptMappingInterface _ConceptMappingInterface;
        public ConceptMappingController(IConceptMappingInterface ConceptMappingInterface)
        {
            _ConceptMappingInterface = ConceptMappingInterface;

        }
        // GET: api/ConceptMapping
        public HttpResponseMessage Get()
        {
            try
            {

                FunctionResponse Resp = _ConceptMappingInterface.GetAll();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    //IEnumerable<ConceptMappingEntity> TranslationList = (IEnumerable<ConceptMappingEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data[0]);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: api/ConceptMapping/5
        public HttpResponseMessage Get(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _ConceptMappingInterface.GetConceptsById(Id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    //IEnumerable<ConceptMappingEntity> TranslationList = (IEnumerable<ConceptMappingEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data[0]);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }

            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: api/ConceptMapping
        public HttpResponseMessage Post(ConceptMappingEntity conceptMappingEntity)
        {
            try
            {
                FunctionResponse Resp = _ConceptMappingInterface.Create(conceptMappingEntity);
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

        // PUT: api/ConceptMapping/5
        public HttpResponseMessage Put(Guid id, ConceptMappingEntity conceptMappingEntity)
        {
            try
            {
                FunctionResponse Resp = _ConceptMappingInterface.Update(id, conceptMappingEntity);
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

        // DELETE: api/ConceptMapping/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                FunctionResponse Resp = _ConceptMappingInterface.Delete(id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        /// <summary>
        /// Get Question Pattern by purticular Id
        /// </summary>
        /// <param name="entity">
        /// {"TopicId": "","CategorySubTopicId": "", "LevelId": "","BoardId": "","SubjectId": "","StandardId": "" }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/ConceptMapping/GetConcepts")]
        public HttpResponseMessage GetConcepts(ConceptMappingEntity entity)
        {
            try
            {
                FunctionResponse Resp = _ConceptMappingInterface.GetConcepts(entity);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<ConceptMappingEntity> TranslationList = (IEnumerable<ConceptMappingEntity>)Resp.Data[0];
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
