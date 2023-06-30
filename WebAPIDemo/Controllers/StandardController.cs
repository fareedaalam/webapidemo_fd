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
    /// <summary>
    /// 
    /// </summary>
    public class StandardController : ApiController
    {

        private readonly IStandardInterface _iStandardInterface;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iStandardInterface"></param>
        public StandardController(IStandardInterface iStandardInterface)
        {
            _iStandardInterface = iStandardInterface;
        }
        /// <summary>
        /// Get All Standarad
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _iStandardInterface.GetAll();
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<StandardEntity> TranslationList = (IEnumerable<StandardEntity>)Resp.Data[0];

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

        // GET: api/Standard/5
        /// <summary>
        /// Get Standard by Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Json Result</returns>
        public HttpResponseMessage Get(Guid Id)
        {

            try
            {
                FunctionResponse Resp = _iStandardInterface.GetById(Id);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<StandardEntity> TranslationList = (IEnumerable<StandardEntity>)Resp.Data[0];


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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="standardEntity"></param>
        /// <returns></returns>
        // POST: api/Standard
        //public HttpResponseMessage Post([FromBody] StandardEntity standardEntity)
        //{
        //    try
        //    {

        //        FunctionResponse Resp = _iStandardInterface.Create(standardEntity);

        //        if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        //        }
        //        else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
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
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internel Server Error");
        //    }
        //}
        public HttpResponseMessage Post([FromBody] StandardEntity standardEntity)
        {
            try
            {
                FunctionResponse Resp = _iStandardInterface.Create(standardEntity);
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



        // PUT: api/Standard/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public HttpResponseMessage Put(Guid Id, StandardEntity entity)
        //{
        //    try
        //    {

        //        FunctionResponse Resp = _iStandardInterface.Update(Id, entity);

        //        if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        //        }
        //        else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
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
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internel Server Error");
        //    }
        //}
        public HttpResponseMessage Put(Guid id, [FromBody] StandardEntity standardEntity)
        {
            try
            {
                FunctionResponse Resp = _iStandardInterface.Update(id, standardEntity);
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


        // DELETE: api/Standard/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(Guid Id)
        {
            try
            {

                FunctionResponse Resp = _iStandardInterface.Delete(Id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internel Server Error");
            }
        }
    }
}
