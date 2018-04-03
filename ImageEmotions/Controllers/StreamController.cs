using ImageEmotions.Models;
using ImageEmotions.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImageEmotions.Controllers
{
    public class StreamController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SubmitImage(string image)
        {
            string results = "";

            try
            {
                var imageData = Convert.FromBase64String(image);

                results = await Task.Run(() => ImageService.ProcessImageAsync(imageData));
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message as object);
            }

            EmotionsViewModel vm = new EmotionsViewModel(results);

            return Json(new { success = true, result = results });

        }
    }
}
