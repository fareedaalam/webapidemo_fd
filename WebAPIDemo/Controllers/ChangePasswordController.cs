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
    public class ChangePasswordController : ApiController
    {
        private readonly IUserInterface _userInterface;

        public ChangePasswordController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
           
        }
        // GET: api/ChangePassword
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ChangePassword/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ChangePassword
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ChangePassword/5
        public HttpResponseMessage Put(Guid id, [FromBody]UserEntity userEntity)
        {
            try
            {
                FunctionResponse Resp = _userInterface.UpdateUser(id, userEntity, userEntity.IsActive);
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
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }


        // DELETE: api/ChangePassword/5
        public void Delete(int id)
        {
        }
    }
}
