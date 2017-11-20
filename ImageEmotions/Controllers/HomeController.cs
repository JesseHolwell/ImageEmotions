using ImageEmotions.Models;
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

        //irrelephant


        public static readonly HttpClient client = new HttpClient();
        private static string uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file, string url)
        {

            string results = "";

            //try
            //{
            //if (file != null)
            //    await Task.Run(() => ProcessImageAsync(file));
            //else
            //await Task.Run(() => ProcessUrlAsync(url));

            results = await Task.Run(() => ProcessImageAsync(file));

            //}
            //catch (Exception ex)
            //{
            //    return View("Error", ex.Message as object);
            //}

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

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "e39efb06499b4fa988f354e3d43e7834");
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
    }
}