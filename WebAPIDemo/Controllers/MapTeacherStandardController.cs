using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    public class MapTeacherStandardController : ApiController
    {
        private readonly IMapTeacherStandardInterface _teacherStandardInterface;
        public MapTeacherStandardController(IMapTeacherStandardInterface teacherStandardInterface)
        {
            _teacherStandardInterface = teacherStandardInterface;
        }

        // GET: api/MapTeacherStandard
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _teacherStandardInterface.GetStandardToTeacher();

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

        // GET: api/MapTeacherStandard/5
        [HttpPost]
        [Route("api/MapTeacherStandard/GetById")]
        public HttpResponseMessage GetById([FromBody] dynamic data)
        {
            try
            {
                Guid TeacherId = (Guid)data.TeacherId;
                Guid StandardId = (Guid)data.StandardId;
                FunctionResponse Resp = _teacherStandardInterface.GetStandardToTeacherById(StandardId, TeacherId);

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

        // POST: api/MapTeacherStandard
        public HttpResponseMessage Post(dynamic data)
        {
            try
            {
                FunctionResponse Resp = _teacherStandardInterface.AssignStandardToTeacher(data);

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

        // PUT: api/MapTeacherStandard/5
        public bool Put(MapTeacherStandardEntity data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                _teacherStandardInterface.UpdateStandardTeacher(data);
            }

            return true;
        }

        // DELETE: api/MapTeacherStandard/5
        public bool Delete([FromBody] dynamic data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                var TeacherId = (Guid)data.TeacherId;
                var StandardId = (Guid)data.StandardId;

                _teacherStandardInterface.DeleteStandardTeacher(StandardId, TeacherId);
            }

            return true;
        }

        [HttpPost]
        [Route("api/MapTeacherStandard/DeleteMapping")]
        public HttpResponseMessage DeleteMapping([FromBody] dynamic data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                var TeacherId = (Guid)data.TeacherId;
                var StandardId = (Guid)data.StandardId;

                FunctionResponse Resp = _teacherStandardInterface.DeleteStandardTeacher(StandardId, TeacherId);
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
