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
    //[ApiAuthenticationFilter]
    public class UserLogController : ApiController
    {
        private readonly IUserLogInterface _userLogInterface;
        // Public constructor to initialize category service instance
        public UserLogController(IUserLogInterface userLogInterface)
        {
            _userLogInterface = userLogInterface;
        }
        // GET api/UserLog/
        public HttpResponseMessage Get()
        {
            var userLog = _userLogInterface.GetAll();
            if (userLog != null)
            {
                var userLogEntities = userLog as List<UserLogEntity> ?? userLog.ToList();
                if (userLogEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, userLogEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Category sub topic not found");
        }
        // GET api/UserLog/5
        public HttpResponseMessage Get(Guid id)
        {
            var userLog = _userLogInterface.GetById(id);
            if (userLog != null)
                return Request.CreateResponse(HttpStatusCode.OK, userLog);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No category sub topic found for this id");
        }

        // POST api/UserLog

        public Guid Post([FromBody]UserLogEntity userLogEntity)
        {
            return _userLogInterface.Create(userLogEntity);
        }

        // PUT api/UserLog/5
        public bool Put(Guid id, [FromBody]UserLogEntity userLogEntity)
        {
            if (id != null)
            {
                return _userLogInterface.Update(id, userLogEntity);
            }
            return false;
        }

        // DELETE api/UserLog/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _userLogInterface.Delete(id);
            return false;
        }
    }
}
