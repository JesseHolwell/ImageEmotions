using ImageEmotions.Common;
using ImageEmotions.Models;
using ImageEmotions.Services;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImageEmotions.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SubmitFile(HttpPostedFileBase file)
        {
            string results = "";

            if (file == null)
                return View("Results", null);

            try
            {
                results = await Task.Run(() => FileService.ProcessFileAsync(file));
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Something went wrong...", exception = ex });
            }

            EmotionsViewModel vm = new EmotionsViewModel(results);

            if (vm.Emotions.Scores.Count > 0)
                return Json(new { success = true, view = RazorViewString.RenderRazorViewToString(this, "Results", vm) });
            else
                return Json(new { success = false, error = "Something went wrong..." });

        }

        //TODO: submit Url
        //[HttpPost]
        //public async Task<ActionResult> SubmitUrl(string url)
    }
}
