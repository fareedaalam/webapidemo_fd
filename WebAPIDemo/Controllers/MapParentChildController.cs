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
    public class MapParentChildController : ApiController
    {
        private readonly IMapParentChildInterface _mapParentChildInterface;

        public MapParentChildController(IMapParentChildInterface iMapParentChildInterface)
        {
            _mapParentChildInterface = iMapParentChildInterface;
        }

        // GET: api/MapParentChild
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _mapParentChildInterface.GetAssignedChildList();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<MapParentChildEntity> TranslationList = (IEnumerable<MapParentChildEntity>)Resp.Data[0];
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

        // GET: api/MapParentChild/5
        public HttpResponseMessage Get(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _mapParentChildInterface.GetAssignedChildListByParentId(Id);
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
        [Route("api/MapParentChild/GetByChildId")]
        public HttpResponseMessage GetByChildId(Guid Id)
        {
            try
            {
                FunctionResponse Resp = _mapParentChildInterface.GetAssignedChildListByChildId(Id);
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


        /// <summary>
        /// Assign Child To Parent
        /// </summary>
        /// <param name="data">ParentId,ChildEmail=Email</param>
        /// <returns></returns> 

        public HttpResponseMessage Post(MapParentChildEntity data)
        {
            try
            {
                FunctionResponse Resp = _mapParentChildInterface.AssignChildToPerent(data);

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
        /// <param name="Id"></param>
        /// <param name="value"></param>
        public HttpResponseMessage Put(Guid Id, MapParentChildEntity entity)
        {
            try
            {
                FunctionResponse Resp = _mapParentChildInterface.Update(entity);

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
        /// Not working due to refrence mapping
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(dynamic data)
        {

            try
            {
                var _ParentId = data.ParentId;
                var _ChildId = data.ChildId;

                FunctionResponse Resp = _mapParentChildInterface.RemoveChildToPerent(_ParentId, _ChildId);

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
        /// <summary>
        /// Take object of {ParentId:'',ChildId:''}
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MapParentChild/DeleteMapping")]
        public HttpResponseMessage DeleteMapping(MapParentChildEntity entity)
        {
            try
            {
                // var _ParentId = entity.ParentId;
                // var _ChildId = entity.ChildId;
                FunctionResponse Resp = _mapParentChildInterface.RemoveChildToPerent(entity.ParentId, entity.ChildId);

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

        [HttpPost]
        [Route("api/MapParentChild/ChildListData")]
        public HttpResponseMessage ChildListData(dynamic data)
        {
            try
            {
                var parentId = (Guid)data.Id;

                FunctionResponse Resp = _mapParentChildInterface.GetAssignedChildListByParentIdWithQuizData(parentId);

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

