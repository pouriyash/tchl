using DNTBreadCrumb.Core;
using Tochal.Core.DomainModels.SSOT;
using Tochal.Web.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;

namespace Tochal.Web.Controllers
{

    public class FileController : Controller
    {
        private readonly IOptions<FileConfig> _fileConfig;

        public FileController(IOptions<FileConfig> fileConfig)
        {
            _fileConfig = fileConfig;
        }

        public ActionResult Image(string field = "")
        {
            ViewBag.Field = field;
            return View();
        }

        public ActionResult File(string field = "", long? length = null, string type = "image")
        {
            ViewBag.Field = field;
            ViewBag.Length = length;
            ViewBag.FileType = type;
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(string field, IFormFile file)
        {
            // TODO: upload file
            ViewBag.Field = field;

            try
            {
                ViewBag.FileName = FileHelper.SaveFile(file, _fileConfig.Value, FileType.image);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(string field, IFormFile file, long? length, string type = "image")
        {
            // TODO: upload file
            ViewBag.Field = field;

            try
            {
                ViewBag.FileName = FileHelper.SaveFile(file, _fileConfig.Value, FileType.file);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

            return View();
        }

        [HttpPost]
        public JsonResult UploadFile2(IFormFile File, long? length, string type = "image")
        {
            var files = Request.Form.Files;
            //todo:try
            var FileName = FileHelper.SaveFile(File, _fileConfig.Value, FileType.file);
            return Json(FileName);



        }
    }

}