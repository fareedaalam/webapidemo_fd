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
    public class LocationController : ApiController
    {
        // GET: Location
        private readonly ILocationInterface _locationInterface;
        private object locationEntities;

        public LocationController(ILocationInterface locationInterface)
        {
            _locationInterface = locationInterface;
        }
        // GET: api/Location
        public HttpResponseMessage Get()
        {
           
            FunctionResponse Resp = _locationInterface.GetAllLocation();
            try
            {
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<LocationEntity> TranslationList = (IEnumerable<LocationEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }

        }

        // GET: api/Location/5
        public HttpResponseMessage Get(Guid id)
        {
         
            try
            {
                FunctionResponse Resp = _locationInterface.GetLocationById(id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<LocationEntity> TranslationList = (IEnumerable<LocationEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
                }
                else if (Resp.Status != FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "Error");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        // POST: api/Location
        public Guid Post([FromBody] LocationEntity locationEntity)
        {
            return _locationInterface.CreateLocation(locationEntity);
        }

        // PUT: api/Location/5
        public bool Put(Guid id, [FromBody]LocationEntity locationEntity)
        {
            if (id != null)
            {
                return _locationInterface.UpdateLocation(id, locationEntity);
            }
            return false;
        }

        // DELETE: api/Location/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _locationInterface.DeleteLocation(id);
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchForm">CityId</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Location/GetLocationByCityId")]
        public HttpResponseMessage GetLocationByCityId(dynamic SearchForm)
        {
            try
            {
                Guid CityId = (Guid)SearchForm.CityId == null ? new Guid() : (Guid)SearchForm.CityId;

                FunctionResponse Resp = _locationInterface.GetLocationByCityId(CityId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                   // var data = Mapper.Map<List<tbl_Map_Role_Permission>, List<MapRolePermissionEntity>>(Model);
                    IEnumerable<LocationEntity> TranslationList = (IEnumerable<LocationEntity>)Resp.Data[0];
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }

        }
    }
}
