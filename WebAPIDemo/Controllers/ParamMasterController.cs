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
    /// <summary>
    /// 
    /// </summary>
    [ApiAuthenticationFilter]
    public class ParamMasterController : ApiController
    {
        private readonly IParamDetailsInterface _paramDetailsInterface;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramDetailsInterface"></param>
        public ParamMasterController(IParamDetailsInterface paramDetailsInterface)
        {
            _paramDetailsInterface = paramDetailsInterface;

        }      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.GetParamAll();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<ParamMasterEntity> TranslationList = (IEnumerable<ParamMasterEntity>)Resp.Data[0];
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
     /// 
     /// </summary>
     /// <param name="Id"></param>
     /// <returns></returns>
        public HttpResponseMessage Get(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.GetParamById(Id);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                   // IEnumerable<ParamMasterEntity> TranslationList = (IEnumerable<ParamMasterEntity>)Resp.Data[0];
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
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(ParamMasterEntity entity)
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.CreateParam(entity);

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
      /// 
      /// </summary>
      /// <param name="Id"></param>
      /// <param name="entity"></param>
      /// <returns></returns>
        public HttpResponseMessage Put(Guid Id, ParamMasterEntity entity)
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.UpdateParam(Id, entity);
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
       /// 
       /// </summary>
       /// <param name="Id"></param>
       /// <returns></returns>
        public HttpResponseMessage Delete(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.DeleteParam(Id);

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
