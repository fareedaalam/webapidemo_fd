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
    public class EmailController : ApiController
    {

        private readonly IEmailInterface _IEmailInterface;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="iEmailInterface"></param>
        public EmailController(IEmailInterface iEmailInterface)
        {
            _IEmailInterface = iEmailInterface;
        }
        // GET: api/Email
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Email/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Email
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchForm">string To,  string Cc, string Subject, string Body</param>
        public HttpResponseMessage Post([FromBody] dynamic SearchForm)
        {   
            try
            {
                FunctionResponse Resp = new FunctionResponse();
                string To = SearchForm.To;
                string Cc= SearchForm.Cc;
                string Subject = SearchForm.Subject == null ? string.Empty : SearchForm.Subject;
                string Body = SearchForm.Body == null ? string.Empty : SearchForm.Body;

                Resp = _IEmailInterface.SendEmail(To, Cc, Subject, Body);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Sent");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        // PUT: api/Email/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Email/5
        public void Delete(int id)
        {
        }
    }
}
