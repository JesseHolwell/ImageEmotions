using ImageEmotions.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImageEmotions.Controllers
{
    public class HomeController : Controller
    {
        public static readonly HttpClient client = new HttpClient();
        private static string uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Stream()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Submit(HttpPostedFileBase file, string url)
        {
            string results = "";

            try
            {
                //TODO: Support URLs
                //if (file != null)
                //    await Task.Run(() => ProcessImageAsync(file));
                //else
                //    await Task.Run(() => ProcessUrlAsync(url));

                results = await Task.Run(() => ProcessImageAsync(file));

            }
            catch (Exception ex)
            {
                return View("Error", ex.Message as object);
            }

            EmotionsViewModel vm = new EmotionsViewModel(results);

            return View("Results", vm);

        }

        [HttpPost]
        public async Task<ActionResult> SubmitFromCanvas(string image)
        {
            string results = "";

            string strData = image.Substring("data:image/png;base64,".Length);
            byte[] data = Convert.FromBase64String(strData);

            try
            {
                results = await Task.Run(() => ProcessImageAsync(data));
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message as object);
            }

            EmotionsViewModel vm = new EmotionsViewModel(results);

            return Json(new { success = true, result = results });

        }

        public async Task<string> ProcessImageAsync(HttpPostedFileBase file)
        {
            //convert HttpPostedFileBase into a octet stream
            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] byteData = b.ReadBytes(file.ContentLength);

            return await ProcessImageAsync(byteData);
        }

        public async Task<string> ProcessImageAsync(byte[] byteData)
        {
            //process byte data
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "9d50196588bd4444ad9c23dd452f772c");
                    using (var myResponse = await client.PostAsync(uri, content))
                    {
                        var results = await myResponse.Content.ReadAsStringAsync();
                        return results;
                    }
                }
            }
        }

        //public async Task ProcessUrlAsync(string url)
        //{
        //    //process url
        //    var content = new StringContent(@"{'url': 'https://res.cloudinary.com/demo/image/upload/w_400/woman.jpg' }");
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    myResponse = await client.PostAsync(uri, content);
        //    results = await myResponse.Content.ReadAsStringAsync();
        //}


        async void MakeRequest()
        {
            //var client = new HttpClient();
            //var queryString = HttpUtility.ParseQueryString(string.Empty);

            //// Request headers
            //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{subscription key}");

            //// Request parameters
            //queryString["outputStyle"] = "aggregate";
            //var uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognizeinvideo?" + queryString;

            //HttpResponseMessage response;

            // Request body
            //byte[] byteData = Encoding.UTF8.GetBytes("{body}");

            //using (var content = new ByteArrayContent(byteData))
            //{
            //    content.Headers.ContentType = new MediaTypeHeaderValue("< your content type, i.e. application/json >");
            //    response = await client.PostAsync(uri, content);
            //}

        }

    }





}
