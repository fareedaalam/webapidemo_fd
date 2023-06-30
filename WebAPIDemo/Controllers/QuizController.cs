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
   // [Authorize(Roles = Role.Admin)]
    //[AuthorizationRequired]
    public class QuizController : ApiController
    {
        private readonly IQuizInterface _quizInterface;
        public QuizController(IQuizInterface quizInterface)
        {
            _quizInterface = quizInterface;
        }

        // GET: api/Quiz
        public HttpResponseMessage Get()
        {
            try
            {

                FunctionResponse Resp = _quizInterface.GetAll();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<QuizEntity> TranslationList = (IEnumerable<QuizEntity>)Resp.Data[0];
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

        // GET: api/Quiz/5
      /// <summary>
      /// Pass UserId 
      /// </summary>
      /// <param name="Id"></param>
      /// <returns></returns>
        public HttpResponseMessage Get(Guid Id)
        {
            try
            {

                FunctionResponse Resp = _quizInterface.GetById(Id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<QuizEntity> TranslationList = (IEnumerable<QuizEntity>)Resp.Data[0];
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

        // POST: api/Quiz
        public HttpResponseMessage Post(QuizEntity entity)
        {
            try
            {
                FunctionResponse Resp = _quizInterface.Create(entity);
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

        // PUT: api/Quiz/5
        public HttpResponseMessage Put(Guid Id, QuizEntity entity)
        {
            try
            {
                FunctionResponse Resp = _quizInterface.Update(Id, entity);
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

        // DELETE: api/Quiz/5
        public HttpResponseMessage Delete(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _quizInterface.Delete(Id);

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
        /// Get Quiz by particular Id
        /// </summary>
        /// <param name="quiz">
        /// {"UserId": ""}
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Quiz/GetQuizByUser")]
        public HttpResponseMessage GetQuiz(QuizEntity quiz)
        {
            try
            {
                FunctionResponse Resp = _quizInterface.GetQuizByUser(quiz);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<QuizEntity> TranslationList = (IEnumerable<QuizEntity>)Resp.Data[0];
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
        /// Save Student Quiz Response
        /// </summary>
        /// <param name="quiz">
        /// {"UserId": ""}
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Quiz/SaveQuizResponse")]
        public HttpResponseMessage SaveQuizResponse(QuizEntity quiz)
        {
            try
            {
                FunctionResponse Resp = _quizInterface.saveQuizResponse(quiz);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    //IEnumerable<QuizEntity> TranslationList = (IEnumerable<QuizEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
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
