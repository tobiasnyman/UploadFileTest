using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UploadFileWithASPNETMVCtest.Controllers
{
    public class UploadFileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private bool isValidContentType(string contentType)
        {
            return contentType.Equals("image/png") || contentType.Equals("image/gif") || contentType.Equals("image/jpeg") || contentType.Equals("image/jpg");
        }

        private bool isValidContentLength(int contentLength)
        {
            return ((contentLength / 1024) / 1024 < 1); //1MB
        }

        [HttpPost]
        public ActionResult Process(HttpPostedFileBase photo)
        {
            if (!isValidContentType(photo.ContentType))
            {
                ViewBag.Error = "Only JPG, JPEG, PNG & GIF files are allowed";
                return View("Index");
            }
            else if (!isValidContentLength(photo.ContentLength))
            {
                ViewBag.Error = "Your file is too large.";
                return View("Index");
            }
            else
            {
                if (photo.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    photo.SaveAs(path);
                    ViewBag.fileName = photo.FileName;
                }
                return View("Success");
            }
        }
    }
}