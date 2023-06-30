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
    public class ParamDetailController : ApiController
    {
        private readonly IParamDetailsInterface _paramDetailsInterface;
        public ParamDetailController(IParamDetailsInterface paramDetailsInterface)
        {
            _paramDetailsInterface = paramDetailsInterface;

        }

        // GET: api/ParamDetail
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.GetParamDetailsAll();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<ParamDetailEntity> TranslationList = (IEnumerable<ParamDetailEntity>)Resp.Data[0];
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
        // GET: api/ParamDetail/5
        public HttpResponseMessage Get(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.GetParamDetailsById(Id);
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
        // POST: api/ParamDetail
        public HttpResponseMessage Post(ParamDetailEntity entity)
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.CreateParamDetails(entity);

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
        // PUT: api/ParamDetail/5
        public HttpResponseMessage Put(Guid Id, ParamDetailEntity entity)
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.UpdateParamDetails(Id, entity);
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
        // DELETE: api/ParamDetail/5
        public HttpResponseMessage Delete(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _paramDetailsInterface.DeleteParamDetails(Id);

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



        //GetParamDetailsByParamName
        [HttpPost]
        [Route("Api/ParamDetail/GetParams")]
        public HttpResponseMessage GetParamDetailsByParamName(dynamic data)
        {
            try
            {
                string paramName = (string)data.ParamName;
                FunctionResponse Resp = _paramDetailsInterface.GetParamDetailsByParamName(paramName);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<ParamDetailEntity> TranslationList = (IEnumerable<ParamDetailEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
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
    }
}
