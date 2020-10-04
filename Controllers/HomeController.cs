using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using S3_Presigned_Keys_jQuery.Logic;
using S3_Presigned_Keys_jQuery.Models;

namespace S3_Presigned_Keys_jQuery.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {


            return View();
        }

        public IActionResult GeneratePresignedUrl(string fileName)
        {
            DateTime expiryTime = DateTime.Now.AddMinutes(120);

            S3 s3 = new S3();

            string url = s3.GeneratePreSignedVideoURL(fileName, expiryTime);
            if (string.IsNullOrWhiteSpace(url))
                return StatusCode(500);

            var result = new GenerateFilenameResult
            {
                PresignedUrl = url,
                FileName = fileName
            };

            return Json(JsonConvert.SerializeObject(result));
        }
    }
}
