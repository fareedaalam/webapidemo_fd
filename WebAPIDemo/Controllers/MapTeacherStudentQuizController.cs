using System;
using BusinessEntities;
using BusinessServices.Interface;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;
using System.Collections.Generic;

namespace WebAPIDemo.Controllers
{
    [ApiAuthenticationFilter]
    public class MapTeacherStudentQuizController : ApiController
    {
        private readonly IMapTeacherStudentQuizInterface _iMapTeacherStudentQuizInterface;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMapTeacherStudentQuizInterface"></param>
        public MapTeacherStudentQuizController(IMapTeacherStudentQuizInterface iMapTeacherStudentQuizInterface)
        {
            _iMapTeacherStudentQuizInterface = iMapTeacherStudentQuizInterface;
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        // DELETE: api/MapTeacherStudentQuiz/
        public void Get() { }

        /// <summary>
        /// pass quiz Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        // GET: api/MapTeacherStudentQuiz/5
        public HttpResponseMessage Get(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _iMapTeacherStudentQuizInterface.GetAssignedStudentListByQuizId(Id);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    // IEnumerable<ParamMasterEntity> TranslationList = (IEnumerable<ParamMasterEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, Resp.Message);
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: api/MapTeacherStudentQuiz
        public HttpResponseMessage Post(List <MapTeacherStudentQuizEntity> entity)
        {
            try
            {
                FunctionResponse Resp = _iMapTeacherStudentQuizInterface.AssignQuizToStudent(entity);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No_Record_Found");
                }

            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="entity"></param>
        // PUT: api/MapTeacherStudentQuiz/5
        public HttpResponseMessage Put(Guid Id, List<MapTeacherStudentQuizEntity> entity)
        {
            try
            {
                FunctionResponse Resp = _iMapTeacherStudentQuizInterface.Update(Id, entity);

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

                //return _levelInterface.Update(id, levelEntity);
                //}
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }

        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/MapTeacherStudentQuiz/5
        public void Delete(Guid id)
        {
        }

        ///// <summary>
        ///// Save Student Quiz Response
        ///// </summary>
        ///// <param name="quiz">
        ///// {"UserId": ""}
        ///// </param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("api/SaveQuizResponse")]
        //public HttpResponseMessage SaveQuizResponse(MapTeacherStudentQuizEntity quiz)
        //{
        //    try
        //    {
        //        FunctionResponse Resp = _iMapTeacherStudentQuizInterface.saveQuizResponse(quiz);

        //        if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
        //        {
        //            IEnumerable<QuizEntity> TranslationList = (IEnumerable<QuizEntity>)Resp.Data[0];
        //            return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
        //        }
        //        else if (Resp.Status == FunctionResponse.StatusType.ERROR)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Error");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
        //        //throw;
        //    }
        //}

        ///// <summary>
        ///// pass student Id
        ///// </summary>
        ///// <param name="StudentId"></param>
        ///// <returns></returns>
        //// Get: api/MapTeacherStudentQuiz/GetAssignedQuiz/5
        //[HttpPost]
        //[Route("api/MapTeacherStudentQuiz/GetStudentQuiz")]
        //public HttpResponseMessage GetStudentQuiz(dynamic StudentId)
        //{

        //    try
        //    {
        //        var studentId = (Guid)StudentId.Id;

        //        FunctionResponse Resp = _iMapTeacherStudentQuizInterface.GetStudentQuizList(studentId);
        //        if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
        //        {
        //            // IEnumerable<ParamMasterEntity> TranslationList = (IEnumerable<ParamMasterEntity>)Resp.Data[0];
        //            return Request.CreateResponse(HttpStatusCode.OK, Resp);
        //        }
        //        else if (Resp.Status == FunctionResponse.StatusType.ERROR)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.OK, Resp.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception code goes here
        //        // Logger.Error(ex);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
        //    }
        //}

        /// <summary>
        /// Save Student Quiz Response
        /// </summary>
        /// <param name="quiz">
        /// {"UserId": ""}
        /// </param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/SaveQuizResponse")]
        public HttpResponseMessage SaveQuizResponse(MapTeacherStudentQuizEntity quiz)
        {
            try
            {
                FunctionResponse Resp = _iMapTeacherStudentQuizInterface.saveQuizResponse(quiz);

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

        ///// <summary>
        ///// pass student Id
        ///// </summary>
        ///// <param name="StudentId"></param>
        ///// <returns></returns>
        //// Get: api/MapTeacherStudentQuiz/GetAssignedQuiz/5
        //[HttpPost]
        //[Route("api/MapTeacherStudentQuiz/GetStudentQuiz")]
        //public HttpResponseMessage GetStudentQuiz(dynamic StudentId)
        //{

        //    try
        //    {
        //        var studentId = (Guid)StudentId.Id;

        //        FunctionResponse Resp = _iMapTeacherStudentQuizInterface.GetStudentQuizList(studentId);
        //        if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
        //        {
        //            // IEnumerable<ParamMasterEntity> TranslationList = (IEnumerable<ParamMasterEntity>)Resp.Data[0];
        //            return Request.CreateResponse(HttpStatusCode.OK, Resp);
        //        }
        //        else if (Resp.Status == FunctionResponse.StatusType.ERROR)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.OK, Resp.Message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception code goes here
        //        // Logger.Error(ex);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
        //    }
        //}

        /// <summary>
        /// pass student Id
        /// </summary>
        /// <param name="StudentId"></param>
        /// <returns></returns>
        // Get: api/MapTeacherStudentQuiz/GetAssignedQuiz/5
        [HttpPost]
        [Route("api/MapTeacherStudentQuiz/GetStudentQuiz")]
        public HttpResponseMessage GetStudentQuiz(dynamic StudentId)
        {

            try
            {
                var studentId = (Guid)StudentId.Id;
                FunctionResponse Resp;

                if (StudentId.TeacherId != null)
                {
                    var teacherId = (Guid)StudentId.TeacherId;
                    Resp = _iMapTeacherStudentQuizInterface.GetStudentQuizList(studentId, teacherId);
                }
                else
                {
                    Resp = _iMapTeacherStudentQuizInterface.GetStudentQuizList(studentId, null);
                }


                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    // IEnumerable<ParamMasterEntity> TranslationList = (IEnumerable<ParamMasterEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, Resp.Message);
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

    }
}
