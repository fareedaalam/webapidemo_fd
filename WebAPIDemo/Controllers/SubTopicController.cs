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
    public class SubTopicController : ApiController
    {
        private readonly ISubTopicInterface _subTopicInterface;
        // Public constructor to initialize subtopic service instance
        public SubTopicController(ISubTopicInterface subTopicInterface)
        {
            _subTopicInterface = subTopicInterface;
        }

        // GET api/subtopic       
        public HttpResponseMessage Get()
        {
            try
            {
                var subTopic = _subTopicInterface.GetAllSubTopic();
                if (subTopic != null)
                {
                    var subTopicEntities = subTopic as List<SubTopicEntity> ?? subTopic.ToList();
                    if (subTopicEntities.Any())
                        return Request.CreateResponse(HttpStatusCode.OK, subTopicEntities);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Sub topic not found");

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.ToString());

            }

        }
        // GET api/subtopic/5
        public HttpResponseMessage Get(Guid id)
        {
            var subTopic = _subTopicInterface.GetSubTopicById(id);
            if (subTopic != null)
                return Request.CreateResponse(HttpStatusCode.OK, subTopic);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No sub topic found for this id");
        }

        // POST api/subtopic

        //public Guid Post([FromBody]SubTopicEntity subTopicEntity)
        //{
        //    return _subTopicInterface.CreateSubTopic(subTopicEntity);
        //}

        public HttpResponseMessage Post([FromBody] SubTopicEntity subTopicEntity)
        {
            try
            {
                FunctionResponse Resp = _subTopicInterface.Create(subTopicEntity);
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

        // PUT api/subtopic/5

        public HttpResponseMessage Put(Guid id, [FromBody] SubTopicEntity subTopicEntity)
        {
            try
            {
                FunctionResponse Resp = _subTopicInterface.Update(id, subTopicEntity);
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

        [HttpPost]
        // [Route("api/Topic/GetTopicBySubjectId")]
        [Route("api/SubTopic/GetSubTopicByTopicId")]
        public HttpResponseMessage GetSubTopicByTopicId(dynamic SearchForm)
        {
            try
            {
                Guid TopicId = (Guid)SearchForm.TopicId == null ? new Guid() : (Guid)SearchForm.TopicId;

                FunctionResponse Resp = _subTopicInterface.GetSubTopicByTopicId(TopicId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<SubTopicEntity> TranslationList = (IEnumerable<SubTopicEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        //public bool Put(Guid id, [FromBody]SubTopicEntity subTopicEntity)
        //{
        //    if (id != null)
        //    {
        //        return _subTopicInterface.UpdateSubTopic(id, subTopicEntity);
        //    }
        //    return false;
        //}

        // DELETE api/subtopic/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _subTopicInterface.DeleteSubTopic(id);
            return false;
        }


    }
}
