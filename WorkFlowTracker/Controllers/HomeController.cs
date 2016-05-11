using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkFlowTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This application must:";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Vijayendra Ghadge";

            return View();
        }

        public FileResult Download()
        {
            string file = "C:\\Users\\Dell\\Dropbox\\IP\\Project 1.docx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(file);
            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = "projectdescription.docx";
            return response;
        }

    }
}