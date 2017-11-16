using ImageEmotions.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImageEmotions.Controllers
{
    public class ImageController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            //TODO: convert HttpPostedFileBase into a octet stream

            Task.Run(() => IndexAsync());

            var results = @"[{'faceRectangle':{
                'height': 89,
                'left': 95,
                'top': 54,
                'width': 89
            },
            'scores': {
                'anger': 7.96481145e-11,
                'contempt': 8.144482e-13,
                'disgust': 4.03720182e-11,
                'fear': 2.92296515e-13,
                'happiness': 1,
                'neutral': 1.95017071e-12,
                'sadness': 1.07769696e-13,
                'surprise': 7.470441e-11
            }]";


            EmotionsViewModel vm = new EmotionsViewModel(results);

            return View("Results", vm);
        }

        public async Task IndexAsync()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "8054ac830fda40b7a531c8e310ccd0fc");

            var uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
            }
        }
    }
}