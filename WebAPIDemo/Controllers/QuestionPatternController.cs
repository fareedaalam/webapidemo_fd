using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    [ApiAuthenticationFilter]
    public class QuestionPatternController : ApiController
    {
        // GET: api/QuestionPattern

        private readonly IQustionPatternInterface _queInterface;
        public QuestionPatternController(IQustionPatternInterface queInterface)
        {
            _queInterface = queInterface;

        }

        public HttpResponseMessage Get()
        {
            try
            {

                FunctionResponse Resp = _queInterface.GetAllquestion();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<QuestionPatternEntity> TranslationList = (IEnumerable<QuestionPatternEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
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

        // GET: api/QuestionPattern/5
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                FunctionResponse Resp = _queInterface.GetquestionById(id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<QuestionPatternEntity> TranslationList = (IEnumerable<QuestionPatternEntity>)Resp.Data[0];
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
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
                //throw;
            }

           
            //if (que != null)
            //    return Request.CreateResponse(HttpStatusCode.OK, que);
            //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Board found for this id");
        }

        // POST: api/QuestionPattern
        public HttpResponseMessage Post([FromBody] QuestionPatternEntity queEntity)
        {
            try
            {

                FunctionResponse Resp = _queInterface.Createquestion(queEntity);

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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // PUT: api/QuestionPattern/5
        public HttpResponseMessage Put(Guid id, [FromBody]QuestionPatternEntity queEntity)
        {
            try
            {
                FunctionResponse Resp = _queInterface.Updatequestion(id, queEntity);
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
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No_Record_Found");
                }

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // DELETE: api/QuestionPattern/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                FunctionResponse Resp = _queInterface.Deletequestion(id);

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
        /// <param name="QuestionPatternEntity">
        /// {"TopicId": "","SubTopicId": "","CategorySubTopicId": "", "LevelId": "","BoardId": "","SubjectId": "","StandardId": "" }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Question/GetQP")]
        public HttpResponseMessage GetQuestionPattern(QuestionPatternEntity queEntity)
        {
            try
            {
                FunctionResponse Resp = _queInterface.GetQuestionPattern(queEntity);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<QuestionPatternEntity> TranslationList = (IEnumerable<QuestionPatternEntity>)Resp.Data[0];
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

        /// <summary>
        /// for remove duplicate category name for display purpose
        /// </summary>
        /// <param name="queEntity">{"TopicId": "","SubTopicId": "","CategorySubTopicId": ""}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Question/GetQPC")]
        public HttpResponseMessage GetQuestionPattern_Category(QuestionPatternEntity queEntity)
        {
            try
            {
                FunctionResponse Resp = _queInterface.GetQuestionPattern_Category(queEntity);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<QuestionPatternEntity> TranslationList = (IEnumerable<QuestionPatternEntity>)Resp.Data[0];
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

        /// <summary>
        /// Get Question Pattern by purticular Id
        /// </summary>
        /// <param name="QuestionPatternEntity">
        /// {"TopicId": "","SubTopicId": "","CategorySubTopicId": "", "LevelId": "","BoardId": "","SubjectId": "","StandardId": "" }
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Question/GetQPS")]
        public HttpResponseMessage GetQuestionPatternsWithSolutions(QuestionPatternEntity queEntity)
        {
            try
            {
                FunctionResponse Resp = _queInterface.GetQuestionPatternSolution(queEntity);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<QuestionPatternEntity> TranslationList = (IEnumerable<QuestionPatternEntity>)Resp.Data[0];
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
