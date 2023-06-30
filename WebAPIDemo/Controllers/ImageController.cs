using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.IO;
using DataModel;

namespace WebAPIDemo.Controllers
{
    public class ImageController : ApiController
    {
        public string ImageName { get; private set; }

        [HttpPost]
        [Route("api/UploadImage")]

        public HttpResponseMessage UploadImage()
        {
            string image_name = null;
            var httprequest = HttpContext.Current.Request;

            var postedfile = httprequest.Files["Image"];

            image_name = new String(Path.GetFileNameWithoutExtension(postedfile.FileName).Take(10).ToArray()).Replace(" ","*");
            image_name = image_name + DateTime.Now.ToString("yymmssfff")+Path.GetExtension(postedfile.FileName);
            var filepath = HttpContext.Current.Server.MapPath("~/Image/"+image_name);

            postedfile.SaveAs(filepath);

            using (PCM_LearningBuddyEntities db = new PCM_LearningBuddyEntities())
            {
                tbl_Image_Content tbl_Image = new tbl_Image_Content();
                {

                    ImageName = image_name;



                }

                db.tbl_Image_Content.Add(tbl_Image);
                db.SaveChanges();

            }

                return Request.CreateResponse(HttpStatusCode.Created);


        }

    }
}
