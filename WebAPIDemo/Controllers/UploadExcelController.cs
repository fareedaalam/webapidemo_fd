using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    public class UploadExcelController : ApiController
    {
        private readonly IUploadExcelInterface _uploadExcelInterface;
        public UploadExcelController(IUploadExcelInterface uploadExcelInterface)
        {
            _uploadExcelInterface = uploadExcelInterface;
        }
        // GET: api/UploadExcel
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/UploadExcel/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/UploadExcel
        public HttpResponseMessage Post(ExcelData excelData)
        {     
            

            try
            {
                if (ModelState.IsValid)
                {
                    FunctionResponse Resp = _uploadExcelInterface.UploadExcel(excelData);

                    if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Success");
                    }
                    //else if (Resp.Status == FunctionResponse.StatusType.DUPLICATE)
                    //{
                    //    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                    //}
                    else if (Resp.Status != FunctionResponse.StatusType.ERROR)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Please select correct file.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internel Server Error");
            }
        }

        // PUT: api/UploadExcel/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/UploadExcel/5
        //public void Delete(int id)
        //{
        //}
    }
}
