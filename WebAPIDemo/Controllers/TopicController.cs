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
    public class TopicController : ApiController
    {
        private readonly ITopicInterface _topicInterface;
        public TopicController(ITopicInterface topicInterface)
        {
            _topicInterface = topicInterface;
        }

        // GET: api/Topic
        public HttpResponseMessage Get()
        {
            var topics = _topicInterface.GetAll().OrderBy(x=>x.Name);
            if (topics != null)
            {
                var topicEntities = topics as List<TopicEntity> ?? topics.ToList();
                if (topicEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, topicEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Record Found");
        }

        // GET: api/Topic/5
        public HttpResponseMessage Get(Guid id)
        {
            // return "value";
            var topic = _topicInterface.GetById(id);
            if (topic != null)
                return Request.CreateResponse(HttpStatusCode.OK, topic);
            return Request.CreateResponse(HttpStatusCode.OK, "No Record Found For Given Id");

        }

        // POST: api/Topic
        //public Guid Post([FromBody] TopicEntity topicEntity)
        //{
        //    return _topicInterface.Create(topicEntity);
        //}

        public HttpResponseMessage Post([FromBody] TopicEntity topicEntity)
        {
            try
            {
                FunctionResponse Resp = _topicInterface.Create(topicEntity);
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

        // PUT: api/Topic/5
        //public bool Put(Guid id, [FromBody] TopicEntity topicEntity)
        //{
        //    if (id != null)
        //    {
        //        return _topicInterface.Update(id, topicEntity);
        //    }
        //    return false;
        //}

        public HttpResponseMessage Put(Guid id, [FromBody] TopicEntity topicEntity)
        {
            try
            {
                FunctionResponse Resp = _topicInterface.Update(id, topicEntity);
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


        // DELETE: api/Topic/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _topicInterface.Delete(id);
            return false;
        }

        /// <summary>
        /// Get topic by subject Id
        /// </summary>
        /// <param name="SearchForm">SubjectId</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Topic/GetTopicBySubjectId")]
        public HttpResponseMessage GetTopicBySubjectId(dynamic SearchForm)
        {
            try
            {
                Guid SubjectId = (Guid)SearchForm.SubjectId == null ? new Guid() : (Guid)SearchForm.SubjectId;

                FunctionResponse Resp = _topicInterface.GetTopicBySubjectId(SubjectId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<TopicEntity> TranslationList = (IEnumerable<TopicEntity>)Resp.Data[0];
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

    }
}