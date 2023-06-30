using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    [ApiAuthenticationFilter]
    public class BoardController : ApiController
    {
        private readonly IBoardInterface _brdInterface;
        public BoardController(IBoardInterface brdInterface)
        {
            _brdInterface = brdInterface;

        }

        // GET: api/Board
        public HttpResponseMessage Get()
        {
            try
            {

                FunctionResponse Resp = _brdInterface.GetAll();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<BoardEntity> TranslationList = (IEnumerable<BoardEntity>)Resp.Data[0];
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

        // GET: api/Board/5
        public HttpResponseMessage Get(Guid id)
        {
            var brd = _brdInterface.GetById(id);
            if (brd != null)
                return Request.CreateResponse(HttpStatusCode.OK, brd);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Board found for this id");
        }

        // POST: api/Board
        public HttpResponseMessage Post([FromBody] BoardEntity brdEntity)
        {
            try
            {
                FunctionResponse Resp = _brdInterface.Create(brdEntity);
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
        // PUT: api/Board/5
        /// <param name="id"></param>
        /// <param name="brdEntity"></param>
        public HttpResponseMessage Put(Guid id, [FromBody]BoardEntity brdEntity)
        {
            try
            {
                FunctionResponse Resp = _brdInterface.Update(id, brdEntity);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No_Record_Found");
                }

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        // DELETE: api/Board/5
        public bool Delete(Guid id)
        {
            if (id != null)
                return _brdInterface.Delete(id);
            return false;
        }
    }
}
