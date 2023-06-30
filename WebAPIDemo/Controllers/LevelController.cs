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
    public class LevelController : ApiController
    {
        private readonly ILevelInterface _levelInterface;
        public LevelController(ILevelInterface levelInterface)
        {
            _levelInterface = levelInterface;
        }

        // GET: api/Level
        [ApiAuthenticationFilter(false)]
        public HttpResponseMessage Get()
        {
            try
            {
                FunctionResponse Resp = _levelInterface.GetAll();

                //var levels = _levelInterface.GetAll();
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<LevelEntity> levelsEntities = (IEnumerable<LevelEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, levelsEntities);
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // GET: api/Level/5
        [ApiAuthenticationFilter(false)]
        public HttpResponseMessage Get(Guid id)
        {
            // return "value";
            var level = _levelInterface.GetById(id);
            if (level != null)
                return Request.CreateResponse(HttpStatusCode.OK, level);
            return Request.CreateResponse(HttpStatusCode.OK, "No Record Found For Given Id");
        }

        // POST: api/Level
        [ApiAuthenticationFilter(false)]
        public HttpResponseMessage Post([FromBody] LevelEntity levelEntity)
        {
            try
            {
                FunctionResponse Resp = _levelInterface.Create(levelEntity);
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

        // PUT: api/Level/5
        [ApiAuthenticationFilter(false)]
        public HttpResponseMessage Put(Guid id, [FromBody] LevelEntity levelEntity)
        {
            try
            {

                // var res = Authenticate();
                //if (id != null)
                //{
                FunctionResponse Resp = _levelInterface.Update(id, levelEntity);

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
            //return false;
        }

        // DELETE: api/Level/5
        [ApiAuthenticationFilter(false)]
        //public bool Delete(Guid id)
        //{
        //    if (id != null)
        //        return _levelInterface.Delete(id);
        //    return false;
        //}


        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                FunctionResponse Resp = _levelInterface.Delete(id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
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


        //public HttpResponseMessage GetLevel(dynamic searchform)
        //{
        //    try
        //    {
        //        string Level_Name = (string)searchform.Name;

        //        var level = _levelInterface.
        //        Console.WriteLine("API USER: " + level);
        //        //IEnumerable<UserEntity> TranslationList = (IEnumerable<UserEntity>)user;

        //        var response = Request.CreateResponse(HttpStatusCode.OK, level);

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception code goes here
        //        // Logger.Error(ex);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
        //    }
        //}


        //public string get_code()
        //{


        //    return null;
        //}
    }
}
