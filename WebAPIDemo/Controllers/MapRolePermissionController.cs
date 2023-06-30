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
    /// <summary>
    /// 
    /// </summary>
    [ApiAuthenticationFilter]    
    public class MapRolePermissionController : ApiController
    {
        private readonly IMapRolePermissionInterface _mapRolePermissionInterface;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iMapRolePermissionInterface"></param>
        public MapRolePermissionController(IMapRolePermissionInterface iMapRolePermissionInterface)
        {
            _mapRolePermissionInterface = iMapRolePermissionInterface;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/MapRolePermission
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _mapRolePermissionInterface.GetRoleToPermission();
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<MapRolePermissionEntity> TranslationList = (IEnumerable<MapRolePermissionEntity>)Resp.Data[0];

                    return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/MapRolePermission/5
        public string Get(int id)
        {
            return "value";
        }
        /// <summary>
        ///  Assign Role to Permission
        /// </summary>
        /// <param name="data">[{"RoleId":"","PermissionId":""}]</param>
        /// <returns>Success Message</returns>
        // POST: api/MapRolePermission
        public HttpResponseMessage Post(dynamic data)
        {

            try
            {

                List<MapRolePermissionEntity> list = new List<MapRolePermissionEntity>();

                for (int i = 0; i < data.Count; i++)
                {
                    var mapping = new MapRolePermissionEntity
                    {
                        RoleId = (Guid)data[i]["RoleId"],// == null ? new Guid() : (Guid)data.RoleId,
                        PermissionId = (Guid)data[i]["PermissionId"],// == null ? new Guid() : (Guid)data.PermissionId,

                    };
                    list.Add(mapping);

                }



                FunctionResponse Resp = _mapRolePermissionInterface.AssignRoleToPermission(list);


                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    // var TranslationList = Json(Resp.Data[0]);
                    //  IEnumerable<MapRolePermissionEntity> TranslationList = (IEnumerable<MapRolePermissionEntity>)Resp.Data[0];

                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internel Server Error");
            }

        }

        // PUT: api/MapRolePermission/5
        public void Put(int id, [FromBody]string value)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">{PermissionId:'', RoleId:''}</param>
        /// <returns></returns>
        // DELETE: api/MapRolePermission/5
        public HttpResponseMessage Delete(dynamic data)
        {
            try
            {
                var PermissionId = (Guid)data.PermissionId == null ? new Guid() : (Guid)data.PermissionId;
                var RoleId = (Guid)data.RoleId == null ? new Guid() : (Guid)data.RoleId;

                FunctionResponse Resp = _mapRolePermissionInterface.DeleteRolePermission(PermissionId, RoleId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    // IEnumerable<MapRolePermissionEntity> TranslationList = (IEnumerable<MapRolePermissionEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have already Assigned this");
            }

        }
    }
}
