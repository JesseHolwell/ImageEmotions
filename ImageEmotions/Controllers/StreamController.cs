using ImageEmotions.Models;
using ImageEmotions.Services;
using System;
using System.Threading.Tasks;
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

            string strData = image.Substring("data:image/png;base64,".Length);
            byte[] data = Convert.FromBase64String(strData);

            //var data = image;

            try
            {
                results = await ImageService.ProcessImageAsync(data);
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
