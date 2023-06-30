using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    public class MapSchoolTeacherController : ApiController
    {
        private readonly IMapSchoolTeacherInterface _schoolTeacherInterface;
        public MapSchoolTeacherController(IMapSchoolTeacherInterface schoolTeacherInterface)
        {
            _schoolTeacherInterface = schoolTeacherInterface;
        }

        // GET: api/MapSchoolTeacher
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _schoolTeacherInterface.GetTeacherToSchool();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data[0]);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: api/MapSchoolTeacher/5
        [HttpPost]
        [Route("api/MapSchoolTeacher/GetById")]
        public HttpResponseMessage GetById([FromBody] dynamic data)
        {
            try
            {
                Guid TeacherId = (Guid)data.TeacherId;
                Guid SchoolId = (Guid)data.SchoolId;
                FunctionResponse Resp = _schoolTeacherInterface.GetTeacherToSchoolById(TeacherId, SchoolId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data[0]);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: api/MapSchoolTeacher
        public HttpResponseMessage Post(dynamic data)
        {
            try
            {
                FunctionResponse Resp = _schoolTeacherInterface.AssignTeacherToSchool(data);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status == FunctionResponse.StatusType.DUPLICATE)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else if (Resp.Status != FunctionResponse.StatusType.ERROR)
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internel Server Error");
            }
        }

        // PUT: api/MapSchoolTeacher/5
        public bool Put(MapSchoolTeacherEntity data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                _schoolTeacherInterface.UpdateTeacherSchool(data);
            }

            return true;
        }

        // DELETE: api/MapSchoolTeacher/5
        public bool Delete([FromBody] dynamic data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                var TeacherId = (Guid)data.TeacherId;
                var SchoolId = (Guid)data.SchoolId;

                _schoolTeacherInterface.DeleteTeacherSchool(TeacherId, SchoolId);
            }

            return true;
        }

        [HttpPost]
        [Route("api/MapSchoolTeacher/DeleteMapping")]
        public HttpResponseMessage DeleteMapping([FromBody] dynamic data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                var TeacherId = (Guid)data.TeacherId;
                var SchoolId = (Guid)data.SchoolId;

                FunctionResponse Resp = _schoolTeacherInterface.DeleteTeacherSchool(TeacherId, SchoolId);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status != FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
        }
    }
}
