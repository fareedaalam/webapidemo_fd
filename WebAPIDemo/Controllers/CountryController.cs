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
    public class CountryController : ApiController
    {
        private readonly ICountryInterface _countryInterface;
        private object countryEntities;

        public CountryController(ICountryInterface countryInterface)
        {
            _countryInterface = countryInterface;
        }
        // GET: api/Country
        public HttpResponseMessage Get()
        {
            var countries = _countryInterface.GetAllCountry();
            if (countries != null)
            {
                var countryEntities = countries as List<CountryEntity> ?? countries.ToList();
                if (countryEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, countryEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Countries Not Found");
        }

        // GET: api/Country/5
        public HttpResponseMessage Get(Guid id)
        {
            var country = _countryInterface.GetCountryById(id);
            if (country != null)
                return Request.CreateResponse(HttpStatusCode.OK, country);
            return Request.CreateResponse(HttpStatusCode.OK, "No country found for given Id");
        }

        // POST: api/Country
        //public Guid Post([FromBody] CountryEntity countryEntity)
        //{
        //    return _countryInterface.CreateCountry(countryEntity);
        //}
        public HttpResponseMessage Post([FromBody] CountryEntity countryEntity)
        {
            try
            {
                FunctionResponse Resp = _countryInterface.CreateCountry(countryEntity);
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


        // PUT: api/Country/5
        //public bool Put(Guid id, [FromBody]CountryEntity countryEntity)
        //{
        //    if (id != null)
        //    {
        //        return _countryInterface.UpdateCountry(id, countryEntity);
        //    }
        //    return false;
        //}
        public HttpResponseMessage Put(Guid id, [FromBody]CountryEntity countryEntity)
        {
            try
            {
                FunctionResponse Resp = _countryInterface.UpdateCountry(id, countryEntity);
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

        // DELETE: api/Country/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _countryInterface.DeleteCountry(id);
            return false;
        }
    }
}
