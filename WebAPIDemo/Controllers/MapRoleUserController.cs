using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    [ApiAuthenticationFilter]
    /// <summary>
    /// Assign Role To User
    /// </summary>
    public class MapRoleUserController : ApiController
    {
        private readonly IMapRoleUserInterface _roleInterface;
        // Public constructor to initialize subtopic service instance
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRoleInterface"></param>
        public MapRoleUserController(IMapRoleUserInterface iRoleInterface)
        {
            _roleInterface = iRoleInterface;
        }
        /// <summary>
        /// Get All Mapped Role
        /// </summary>
        /// <returns></returns>
        // GET: api/MapRoleUser
        public HttpResponseMessage Get()
        {
            
            try
            {
                FunctionResponse Resp = _roleInterface.GetRoleToUser();

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
        /// <summary>
        /// Get mapping by role id
        /// </summary>
        /// <param name="id">PassRole ID </param>
        /// <returns></returns>
        // GET: api/MapRoleUser/5
        public HttpResponseMessage Get(Guid id)
        {
            //IEnumerable<MapRoleUserEntity> maproles = _roleInterface.GetRoleToUserById(id);
            //if (maproles != null)
            //{
            //    //var mapRoleUserEntities = maproles as List<MapRoleUserEntity> ?? maproles.ToList();
            //    // if (mapRoleUserEntities.Any())
            //    return Request.CreateResponse(HttpStatusCode.OK, maproles);
            //}
           // return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record Not Found");

            try
            {
                FunctionResponse Resp = _roleInterface.GetRoleByUserID(id);

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
        /// <summary>
        /// Assign Role to any user
        /// </summary>
        /// <param name="data">UserId and RoleId</param>
        /// <returns></returns>
        //// POST: api/MapRoleUser
        public HttpResponseMessage Post(dynamic data)
        {
            try
            {

                Guid RoleId = (Guid)data.RoleId;
                Guid UserId = (Guid)data.UserId;
                //        UserId
                //List<MapRoleUserEntity> list = new List<MapRoleUserEntity>();

                //for (int i = 0; i < data.Count; i++)
                //{
                //    var mapping = new MapRoleUserEntity
                //    {
                //        RoleId = (Guid)data[i]["RoleId"],// == null ? new Guid() : (Guid)data.RoleId,
                //        UserId = (Guid)data[i]["UserId"],// == null ? new Guid() : (Guid)data.PermissionId,

                //    };
                //    list.Add(mapping);

                //}

                FunctionResponse Resp = _roleInterface.AssignRoleToUser(RoleId, UserId);


                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    // var TranslationList = Json(Resp.Data[0]);
                    //  IEnumerable<MapRolePermissionEntity> TranslationList = (IEnumerable<MapRolePermissionEntity>)Resp.Data[0];

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
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internel Server Error");
            }


        }


        //// PUT: api/MapRoleUser/5
        //public bool Put(Guid id, [FromBody]MapRoleUserEntity mapRoleUserEntity)
        //{
        //    if (id != null)
        //    {
        //        return _mRoleUserInterface.Update(id, mapRoleUserEntity);
        //    }
        //    return false;
        //}

        // DELETE: api/MapRoleUser/5
        /// <summary>
        /// Delete Assing Role 
        /// </summary>
        /// <param name="data">RoleId and UserId</param>
        /// <returns></returns>
        public bool Delete([FromBody] dynamic data)
        {

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            else
            {
                var UserId = (Guid)data.UserId;
                var RoleId = (Guid)data.RoleId;

                _roleInterface.DeleteRoleUser(RoleId, UserId);

            }

            return true;
        }

        //Another methood 
        /// <summary>
        /// Get Role By userID. Just pass UserId 
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[Route("api/MapRoleUser/GetRoleByUserid")]
        //public HttpResponseMessage GetMapRole_User_ById(dynamic searchform)
        //{
        //    Guid UserId = (Guid)searchform.UserId == null ? new Guid() : (Guid)searchform.UserId;

        //    IEnumerable<MapRoleUserEntity> maproles = _roleInterface.GetRoleToUserById(UserId);

        //    if (maproles != null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, maproles);
        //    }

        //    else
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Record Not Found");

        //}
        /// <summary>
        /// get user role by user id
        /// </summary>
        /// <param name="SearchForm">UserId</param>
        /// <returns>if Success Return Json List else string message</returns>
        [HttpPost]
        [Route("api/MapRoleUser/GetRoleByUserId")]
        public HttpResponseMessage GetRoleByUserId(dynamic SearchForm)
        {
            try
            {
                Guid UserId = (Guid)SearchForm.UserId == null ? new Guid() : (Guid)SearchForm.UserId;

                FunctionResponse Resp = _roleInterface.GetRoleByUserID(UserId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                   // IEnumerable<MapRoleUserEntity> TranslationList = (IEnumerable<MapRoleUserEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data[0]);
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
    }
}
