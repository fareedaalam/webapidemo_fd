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
    public class SubjectController : ApiController
    {
        private readonly ISubjectInterface _subjectInterface;
        
        // Public constructor to initialize subject service instance
        public SubjectController(ISubjectInterface subjectInterface)
        {
            _subjectInterface = subjectInterface;
            //flag = "created";
        }

        // GET api/subject       
        public HttpResponseMessage Get()
        {
            var subjects = _subjectInterface.GetAllSubject();
            if (subjects != null)
            {
                var subjectEntities = subjects as List<SubjectEntity> ?? subjects.ToList();
                if (subjectEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, subjectEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Subject not found");
        }

        // GET api/subject/5
        public HttpResponseMessage Get(Guid id)
        {
            var subject = _subjectInterface.GetSubjectById(id);
            if (subject != null)
                return Request.CreateResponse(HttpStatusCode.OK, subject);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No subject found for this id");
        }

        // POST api/subject

        //public Guid Post([FromBody] SubjectEntity subjectEntity)
        //{
        //    return _subjectInterface.CreateSubject(subjectEntity);
        //}


        public HttpResponseMessage Post([FromBody] SubjectEntity subjectEntity)
        {
            try
            {
                FunctionResponse Resp = _subjectInterface.CreateSubject(subjectEntity);
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


        // PUT api/subject/5
        //public bool Put(Guid id, [FromBody]SubjectEntity subjectEntity)
        //{
        //    if (id != null)
        //    {
        //        return _subjectInterface.UpdateSubject(id, subjectEntity);
        //    }
        //    return false;
        //}

        public HttpResponseMessage Put(Guid id, [FromBody] SubjectEntity subjectEntity)
        {
            try
            {
                FunctionResponse Resp = _subjectInterface.UpdateSubject(id, subjectEntity);
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

        // DELETE api/subject/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _subjectInterface.DeleteSubject(id);
            return false;
        }
    }
}
