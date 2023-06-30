using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    public class MapSchoolStandardController : ApiController
    {
        private readonly IMapSchoolStandardInterface _schoolStandardInterface;
        public MapSchoolStandardController(IMapSchoolStandardInterface schoolStandardInterface)
        {
            _schoolStandardInterface = schoolStandardInterface;
        }
        // GET: api/MapSchoolStandard
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _schoolStandardInterface.GetStandardToSchool();

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

        // GET: api/MapSchoolStandard/5
        [HttpPost]
        [Route("api/MapSchoolStandard/GetById")]
        public HttpResponseMessage GetById([FromBody] dynamic data)
        {
            try
            {
                Guid StandardId = (Guid)data.StandardId;
                Guid SchoolId = (Guid)data.SchoolId;
                FunctionResponse Resp = _schoolStandardInterface.GetStandardToSchoolById(StandardId, SchoolId);

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

        // POST: api/MapSchoolStandard
        public HttpResponseMessage Post(dynamic data)
        {
            try
            {
                FunctionResponse Resp = _schoolStandardInterface.AssignStandardToSchool(data);

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

        // PUT: api/MapSchoolStandard/5
        public bool Put( MapSchoolStandardEntity data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                _schoolStandardInterface.UpdateStandardSchool(data);
            }

            return true;
        }
        // DELETE: api/MapSchoolStandard/5
        public bool Delete([FromBody] dynamic data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                var StandardId = (Guid)data.StandardId;
                var SchoolId = (Guid)data.SchoolId;

                _schoolStandardInterface.DeleteStandardSchool(StandardId, SchoolId);
            }

            return true;
        }

        [HttpPost]
        [Route("api/MapSchoolStandard/DeleteMapping")]
        public HttpResponseMessage DeleteMapping([FromBody] dynamic data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                var StandardId = (Guid)data.StandardId;
                var SchoolId = (Guid)data.SchoolId;

                FunctionResponse Resp = _schoolStandardInterface.DeleteStandardSchool(StandardId, SchoolId);
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
