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
    public class CurriculumController : ApiController
    {
        private readonly ICurriculumInterface _curriculumInterface;
        // private object countryEntities;

        public CurriculumController(ICurriculumInterface curriculumInterface)
        {
            _curriculumInterface = curriculumInterface;
        }


        // GET: api/Curriculum
        public HttpResponseMessage Get()
        {
            try
            {

                FunctionResponse Resp = _curriculumInterface.GetAll();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    //IEnumerable<CurriculumEntity> TranslationList = (IEnumerable<CurriculumEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else if (Resp.Status == FunctionResponse.StatusType.NO_RECORD)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: api/Curriculum/5
        public HttpResponseMessage Get(Guid Id)
        {
            try
            {

                FunctionResponse Resp = _curriculumInterface.GetById(Id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<CurriculumEntity> TranslationList = (IEnumerable<CurriculumEntity>)Resp.Data[0];
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

        // POST: api/Curriculum
        /// <summary>
        /// 
        /// </summary>
        /// <param name="curriculumEntity"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(CurriculumEntity curriculumEntity)
        {
            try
            {
                FunctionResponse Resp = _curriculumInterface.Create(curriculumEntity);
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

        // PUT: api/Curriculum/5
        public HttpResponseMessage Put(Guid id, CurriculumEntity curriculumEntity)
        {
            try
            {
                FunctionResponse Resp = _curriculumInterface.Update(id, curriculumEntity);
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

        // DELETE: api/Curriculum/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                FunctionResponse Resp = _curriculumInterface.Delete(id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
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


        //[HttpPost]
        //[Route("api/Curriculum/Filter")]
        //public HttpResponseMessage Filter(CurriculumEntity entity)
        //{
        //    try
        //    {

        //        FunctionResponse Resp = _curriculumInterface.Filter(entity);

        //        if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
        //        {
        //            IEnumerable<CurriculumEntity> TranslationList = (IEnumerable<CurriculumEntity>)Resp.Data[0];
        //            return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
        //        }
        //        else if (Resp.Status == FunctionResponse.StatusType.ERROR)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
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
        /// 
        /// </summary>
        /// <param name="SearchForm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Curriculum/GetByUser")]
        public HttpResponseMessage GetByUser(dynamic SearchForm)
        {
            try
            {
                var UserId = (Guid)SearchForm.UserId;

                FunctionResponse Resp = _curriculumInterface.GetByUser(UserId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<CurriculumEntity> TranslationList = (IEnumerable<CurriculumEntity>)Resp.Data[0];
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

    }
}
