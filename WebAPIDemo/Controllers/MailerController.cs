using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessServices.Interface;

namespace WebAPIDemo.Controllers
{
    public class MailerController : ApiController
    {
        private readonly IMailerInterface _iMailerInterface;

        public MailerController(IMailerInterface iMailerInterface)
        {
            _iMailerInterface = iMailerInterface;

        }
        // GET: api/Mailer
        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            try
            {
                
                FunctionResponse Resp = _iMailerInterface.GetAll();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    //Send Verification Mail To User
                   // Resp = _userInterface.SendVerificationMail(userEntity, Resp, _IEmailInterface);
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data[0]);
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
       /// Get By Id
       /// </summary>
       /// <param name="Id"></param>
       /// <returns></returns>
        public HttpResponseMessage Get(Guid Id)
        {
            FunctionResponse Resp = _iMailerInterface.GetById(Id);

            try
            {
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                   // IEnumerable<MailerEntity> TranslationList = (IEnumerable<MailerEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data[0]);
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        [HttpGet]
        [Route("api/User/GetByName")]
        public HttpResponseMessage Get(string Name)
        {
            FunctionResponse Resp = _iMailerInterface.GetByName(Name.Trim());

            try
            {
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    //IEnumerable<MailerEntity> TranslationList = (IEnumerable<MailerEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data[0]);
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        // POST: api/Mailer
        /// <summary>
        /// Insert Data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public  HttpResponseMessage Post(MailerEntity entity)
        {
            FunctionResponse Resp = _iMailerInterface.Create(entity);

            try
            {
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                   // IEnumerable<MailerEntity> TranslationList = (IEnumerable<MailerEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

      /// <summary>
      /// Update Entity
      /// </summary>
      /// <param name="Id"></param>
      /// <param name="entity"></param>
      /// <returns></returns>
        public  HttpResponseMessage Put(Guid Id, MailerEntity entity)
        {
            FunctionResponse Resp = _iMailerInterface.Update(Id,entity);

            try
            {
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    // IEnumerable<MailerEntity> TranslationList = (IEnumerable<MailerEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

       /// <summary>
       /// Delete Data
       /// </summary>
       /// <param name="Id"></param>
       /// <returns></returns>
        public  HttpResponseMessage Delete(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _iMailerInterface.Delete(Id);

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
