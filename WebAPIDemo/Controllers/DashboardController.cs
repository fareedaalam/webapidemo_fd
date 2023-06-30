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
    public class DashboardController : ApiController
    {
        private readonly IDashboardInterface _dashboardInterface;
        public DashboardController(IDashboardInterface dashboardInterface)
        {
            _dashboardInterface = dashboardInterface;

        }
        // GET: api/Dashboard
        //public HttpResponseMessage Get()
        //{
        //    return Request.CreateResponse(HttpStatusCode.NotImplemented, "Not Implemented");

        //    //try
        //    //{
        //    //    FunctionResponse Resp = _dashboardInterface.StudentDashboard();

        //    //    if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
        //    //    {
        //    //        IEnumerable<BoardEntity> TranslationList = (IEnumerable<BoardEntity>)Resp.Data[0];
        //    //        return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
        //    //    }
        //    //    else if (Resp.Status == FunctionResponse.StatusType.ERROR)
        //    //    {
        //    //        return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
        //    //    }
        //    //    else
        //    //    {
        //    //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    // Log exception code goes here
        //    //    // Logger.Error(ex);
        //    //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
        //    //}
        //}

        // GET: api/Dashboard/5
        //public HttpResponseMessage Get(Guid Id)
        //{
        //    try
        //    {
        //       // FunctionResponse Resp = _dashboardInterface.StudentDashboard(Id);
        //        FunctionResponse Resp = _dashboardInterface.TeacherDashboard(Id);

        //        if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
        //        {
        //            //IEnumerable<QuizEntity> TranslationList = (IEnumerable<QuizEntity>)Resp.Data[0];
        //            return Request.CreateResponse(HttpStatusCode.OK, Resp.Data[0]);
        //        }
        //        else if (Resp.Status == FunctionResponse.StatusType.ERROR)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception code goes here
        //        // Logger.Error(ex);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
        //    }
        //}

        // POST: api/Dashboard
        public HttpResponseMessage Post(UserEntity entity)
        {
            try
            {
                FunctionResponse Resp = null;
                if (entity.RoleName == "Student")
                {
                     Resp = _dashboardInterface.StudentDashboard(entity.Id);
                }
                else if (entity.RoleName == "Teachers")
                {
                     Resp = _dashboardInterface.TeacherDashboard(entity.Id);
                }

                else if (entity.RoleName == "Parent")
                {
                    Resp = _dashboardInterface.ParentDashboard(entity.Id);
                }

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    //IEnumerable<QuizEntity> TranslationList = (IEnumerable<QuizEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Data);
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

        //// PUT: api/Dashboard/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Dashboard/5
        //public void Delete(int id)
        //{
        //}
    }
}
