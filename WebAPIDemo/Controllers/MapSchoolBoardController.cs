using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    public class MapSchoolBoardController : ApiController
    {
        private readonly IMapSchoolBoardInterface _schoolBoardInterface;
        public MapSchoolBoardController(IMapSchoolBoardInterface schoolBoardInterface)
        {
            _schoolBoardInterface = schoolBoardInterface;
        }
        // GET: api/MapSchoolBoard
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _schoolBoardInterface.GetBoardToSchool();

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
        // POST: api/MapSchoolBoard
        public HttpResponseMessage Post(dynamic data)
        {
            try
            {

                FunctionResponse Resp = _schoolBoardInterface.AssignBoardToSchool(data);

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

        // GET: api/MapSchoolBoard/5
        [HttpPost]
        [Route("api/MapSchoolBoard/GetById")]
        public HttpResponseMessage GetById([FromBody] dynamic data)
        {
            try
            {
                Guid BoardId = (Guid)data.BoardId;
                Guid SchoolId = (Guid)data.SchoolId;
                FunctionResponse Resp = _schoolBoardInterface.GetBoardToSchoolById(BoardId, SchoolId);

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

        // PUT: api/MapSchoolBoard/5
        public bool Put(MapSchoolBoardEntity data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                _schoolBoardInterface.UpdateBoardSchool(data);
            }

            return true;
        }

        // DELETE: api/MapSchoolBoard/5
        public bool Delete([FromBody] dynamic data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                var BoardId = (Guid)data.BoardId;
                var SchoolId = (Guid)data.SchoolId;

                _schoolBoardInterface.DeleteBoardSchool(BoardId, SchoolId);
            }

            return true;
        }

        [HttpPost]
        [Route("api/MapSchoolBoard/DeleteMapping")]
        public HttpResponseMessage DeleteMapping([FromBody] dynamic data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                var BoardId = (Guid)data.BoardId;
                var SchoolId = (Guid)data.SchoolId;

                FunctionResponse Resp = _schoolBoardInterface.DeleteBoardSchool(BoardId, SchoolId);
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
