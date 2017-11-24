using ImageEmotions.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImageEmotions.Controllers
{
    public class HomeController : Controller
    {
        //irrelephant
        private static string uri = "http://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";
        private static Uri formaturi = new Uri(uri);
        private static readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file, string url)
        {

            string results = "";

            try
            {
                //TODO: Support URLs
                if (file != null)
                    results = await Task.Run(() => ProcessImageAsync(file));
                else
                    results = await Task.Run(() => ProcessUrlAsync(url));

                //results = await Task.Run(() => ProcessImageAsync(file));

            }
            catch (Exception ex)
            {
                return View("Error", ex.Message as object);
            }

            EmotionsViewModel vm = new EmotionsViewModel(results);

            return View("Results", vm);

        }

        public async Task<string> ProcessImageAsync(HttpPostedFileBase file)
        {
            //convert HttpPostedFileBase into a octet stream
            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] byteData = b.ReadBytes(file.ContentLength);

            //process byte data
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                ServicePointManager.Expect100Continue = false;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //client.DefaultRequestHeaders.ExpectContinue = false;
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "e39efb06499b4fa988f354e3d43e7834");
                using (var myResponse = await client.PostAsync(uri, content))
                {
                    var results = await myResponse.Content.ReadAsStringAsync();
                    return results;
                }
            }
        }

        public async Task<string> ProcessUrlAsync(string url)
        {
            //process url
            //using (var content = new StringContent(@"{'url': '" + url + "' }"))
            //{
            var content = new StringContent(@"{'url': 'https://res.cloudinary.com/demo/image/upload/w_400/woman.jpg' }");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //using (var client = new HttpClient())
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "e39efb06499b4fa988f354e3d43e7834");
            using (var myResponse = await client.PostAsync(formaturi, content))
            {
                var results = await myResponse.Content.ReadAsStringAsync();
                return results;
            }
            //}
        }
    }
}