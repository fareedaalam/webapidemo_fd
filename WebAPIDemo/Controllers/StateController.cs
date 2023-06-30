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
    /// <summary>
    /// 
    /// </summary>
    public class StateController : ApiController
    {
        // GET: State
        private readonly IStateInterface _stateInterface;
        //  private object stateEntities;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateInterface"></param>
        public StateController(IStateInterface stateInterface)
        {
            _stateInterface = stateInterface;
        }


        /// <summary>
        /// Get State list by countyid
        /// </summary>
        /// <param name="SearchForm">Pass ContryId in post data feild </param>
        /// <returns>list of states by releted conuntry </returns>
        [HttpPost]
        [Route("api/State/GetStateByCountryId")]
        public HttpResponseMessage GetStateByCountryId(dynamic SearchForm)
        {
            try
            {
                Guid CountryId = (Guid)SearchForm.CountryId == null ? new Guid() : (Guid)SearchForm.CountryId;

                FunctionResponse Resp = _stateInterface.GetStateByCountryId(CountryId);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<StateEntity> TranslationList = (IEnumerable<StateEntity>)Resp.Data[0];
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


        // GET: api/State
        public HttpResponseMessage Get()
        {
            var states = _stateInterface.GetAllStates();
            if (states != null)
            {
                var statesEntities = states as List<StateEntity> ?? states.ToList();
                if (statesEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, statesEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "States Not Found");
        }

        // GET: api/State/5
        public HttpResponseMessage Get(Guid id)
        {
            var state = _stateInterface.GetStateById(id);
            if (state != null)
                return Request.CreateResponse(HttpStatusCode.OK, state);
            return Request.CreateResponse(HttpStatusCode.OK, "No state found for given Id");
        }

        // POST: api/State
        public Guid Post([FromBody] StateEntity stateEntity)
        {
            return _stateInterface.CreateState(stateEntity);
        }

        // PUT: api/State/5
        public bool Put(Guid id, [FromBody]StateEntity stateEntity)
        {
            if (id != null)
            {
                return _stateInterface.UpdateState(id, stateEntity);
            }
            return false;
        }

        // DELETE: api/State/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _stateInterface.DeleteState(id);
            return false;
        }
    }
}
