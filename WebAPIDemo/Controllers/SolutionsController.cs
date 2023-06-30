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
    /// <summary>
    /// 
    /// </summary>
    public class SolutionsController : ApiController
    {
        private readonly ISolutionsInterface _solutionsInterface;
        public SolutionsController(ISolutionsInterface solutionsInterface)
        {
            _solutionsInterface = solutionsInterface;

        }
        /// <summary>
        /// Get All solutions
        /// </summary>
        /// <returns></returns>
        // GET: api/Solutions
      //  [ActionName("Default")]         
        public HttpResponseMessage Get()
        {
            try
            {

                FunctionResponse Resp = _solutionsInterface.GetAll();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<SolutionsEntity> TranslationList = (IEnumerable<SolutionsEntity>)Resp.Data[0];
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
        /// <summary>
        /// Get solution by pattern Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        // GET: api/Solutions/5
        public HttpResponseMessage Get(Guid Id)
        {         
            try
            {
                FunctionResponse Resp = _solutionsInterface.GetByPatternId(Id);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<SolutionsEntity> TranslationList = (IEnumerable<SolutionsEntity>)Resp.Data[0];
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
        /// <summary>
        ///  get Solution by solution id (identity or primary key) pass row unique id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Solutions/GetById")]      
        public HttpResponseMessage GetByPattern(Guid Id)
        {
            try
            {
               // Guid Id = (Guid)searchform.UserName;

                FunctionResponse Resp = _solutionsInterface.GetById(Id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    //IEnumerable<SolutionsEntity> TranslationList = (IEnumerable<SolutionsEntity>)Resp.Data[0];
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
        /// <summary>
        /// Save/Create Solutions
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // POST: api/Solutions
        public HttpResponseMessage Post(SolutionsEntity entity)
        {
            try
            {
                FunctionResponse Resp = _solutionsInterface.Create(entity);

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
        /// <summary>
        /// Update solutions
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        // PUT: api/Solutions/5
        public HttpResponseMessage Put(Guid Id, SolutionsEntity entity)
        {
            try
            {
                FunctionResponse Resp = _solutionsInterface.Update(Id, entity);
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
        /// <summary>
        /// Delete Solutions
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        // DELETE: api/Solutions/5
        public HttpResponseMessage Delete(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _solutionsInterface.Delete(Id);

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
    }
}
