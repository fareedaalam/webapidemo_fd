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
    [ApiAuthenticationFilter]
    public class PermissionController : ApiController
    {
        private readonly IPermissionInterface _permissionInterface;
        public PermissionController(IPermissionInterface permissionInterface)
        {
            _permissionInterface = permissionInterface;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/Permission
        public HttpResponseMessage Get()
        {
            var permissions = _permissionInterface.GetAllPermissions();
            if (permissions != null)
            {
                var permissionEntity = permissions as List<PermissionEntity> ?? permissions.ToList();
                if (permissionEntity.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, permissionEntity);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Permissions Not Found");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Permission/5
        public HttpResponseMessage Get(Guid id)
        {
            var permission = _permissionInterface.GetPermissionById(id);
            if (permission != null)
                return Request.CreateResponse(HttpStatusCode.OK, permission);
            return Request.CreateResponse(HttpStatusCode.OK, "No permission found for given Id");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="permissionEntity"></param>
        /// <returns></returns>
        // POST: api/Permission
        //public Guid Post([FromBody]PermissionEntity permissionEntity)
        //{
        //    return _permissionInterface.CreatePermission(permissionEntity);
        //}
        public HttpResponseMessage Post([FromBody] PermissionEntity permissionEntity)
        {
            try
            {
                FunctionResponse Resp = _permissionInterface.CreatePermission(permissionEntity);
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
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="permissionEntity"></param>
        /// <returns></returns>
        // PUT: api/Permission/5
        //public bool Put(Guid id, [FromBody]PermissionEntity permissionEntity)
        //{
        //    if (id != null)
        //    {
        //        return _permissionInterface.UpdatePermission(id, permissionEntity);
        //    }
        //    return false;
        //}
        public HttpResponseMessage Put(Guid id, [FromBody]PermissionEntity permissionEntity)
        {
            try
            {
                FunctionResponse Resp = _permissionInterface.UpdatePermission(id, permissionEntity);
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
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Permission/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _permissionInterface.DeletePermission(id);
            return false;
        }
    }
}
