using System;
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
    public class MapTeacherChildController : ApiController    {

        private readonly IMapTeacherChildInterface _iMapTeacherChildInterface;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMapTeacherChildInterface"></param>
        public MapTeacherChildController(IMapTeacherChildInterface iMapTeacherChildInterface)
        {
            _iMapTeacherChildInterface = iMapTeacherChildInterface;
        }
        // GET: api/MapTeacherChild
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _iMapTeacherChildInterface.GetAssignedChildList();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<MapTeacherChildEntity> TranslationList = (IEnumerable<MapTeacherChildEntity>)Resp.Data[0];
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
        /// pass teacher Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        // GET: api/MapParentChild/5
        public HttpResponseMessage Get(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _iMapTeacherChildInterface.GetAssignedChildListByTeacherId(Id);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    // IEnumerable<ParamMasterEntity> TranslationList = (IEnumerable<ParamMasterEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        [HttpGet]
        [Route("api/MapTeacherChild/GetTeacherMapByChildId")]
        public HttpResponseMessage GetByChildId(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _iMapTeacherChildInterface.GetAssignedChildListByChildId(Id);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    // IEnumerable<ParamMasterEntity> TranslationList = (IEnumerable<ParamMasterEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // POST: api/MapTeacherChild
        public HttpResponseMessage Post(MapTeacherChildEntity data)
        {
            try
            {
                FunctionResponse Resp = _iMapTeacherChildInterface.AssignChildToTeacher(data);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No_Record_Found");
                }

            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internel Server Error");
            }
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/MapTeacherChild/5
        public HttpResponseMessage Put(Guid Id,MapTeacherChildEntity entity)
        {
            try
            {
                FunctionResponse Resp = _iMapTeacherChildInterface.Update(entity);

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

                //return _levelInterface.Update(id, levelEntity);
                //}
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }

        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/MapTeacherChild/5
        public void Delete(int id)
        {
        }
        [HttpPost]
        [Route("api/MapTeacherChild/DeleteMapping")]
        public HttpResponseMessage DeleteMapping(MapTeacherChildEntity entity)
        {
            try
            {
                // var _ParentId = entity.ParentId;
                // var _ChildId = entity.ChildId;
                FunctionResponse Resp = _iMapTeacherChildInterface.RemoveChildToTeacher(entity.TeacherId, entity.ChildId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
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

        [HttpPost]
        [Route("api/MapTeacherChild/ChildListData")]
        public HttpResponseMessage ChildListData(dynamic data)
        {
            try
            {
                var teacherId = (Guid)data.Id;

                FunctionResponse Resp = _iMapTeacherChildInterface.GetAssignedChildListByTeacherIdWithQuizData(teacherId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data);
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
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
