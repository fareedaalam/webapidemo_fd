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
    public class CityController : ApiController
    {
        // GET: City
        private readonly ICityInterface _cityInterface;
        private object cityEntities;

        public CityController(ICityInterface cityInterface)
        {
            _cityInterface = cityInterface;
        }

        /// <summary>
        /// Get City list stateid
        /// </summary>
        /// <param name="SearchForm">Pass StateId in post data feild </param>
        /// <returns>list of states by releted conuntry </returns>
        [HttpPost]
        [Route("api/City/GetCityByStateId")]
        public HttpResponseMessage GetCityByStateId(dynamic SearchForm)
        {
            try
            {
                Guid StateId = (Guid)SearchForm.StateId == null ? new Guid() : (Guid)SearchForm.StateId;

                FunctionResponse Resp = _cityInterface.GetCityByStateId(StateId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<CityEntity> TranslationList = (IEnumerable<CityEntity>)Resp.Data[0];
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




        // GET: api/Country
        public HttpResponseMessage Get()
        {
           
            FunctionResponse Resp = _cityInterface.GetAllcities();
            try
            {
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<CityEntity> TranslationList = (IEnumerable<CityEntity>)Resp.Data[0];
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

        // GET: api/Country/5
        public HttpResponseMessage Get(Guid id)
        {
            //var city = _cityInterface.GetcityById(id);
            //if (city != null)
            //    return Request.CreateResponse(HttpStatusCode.OK, city);
            //return Request.CreateResponse(HttpStatusCode.OK, "No city found for given Id");

            FunctionResponse Resp = _cityInterface.GetcityById(id);

            try
            {
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<CityEntity> TranslationList = (IEnumerable<CityEntity>)Resp.Data[0];
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



        // POST: api/Role
        public Guid Post([FromBody] CityEntity cityEntity)
        {
            return _cityInterface.CreateCity(cityEntity);
        }

        // PUT: api/Role/5
        public bool Put(Guid id, [FromBody]CityEntity cityEntity)
        {
            if (id != null)
            {
                return _cityInterface.UpdateCity(id, cityEntity);
            }
            return false;
        }

        // DELETE: api/Role/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _cityInterface.DeleteCity(id);
            return false;
        }
    }
}
