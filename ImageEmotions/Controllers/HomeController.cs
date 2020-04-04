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

        [HttpPost]
        public async Task<ActionResult> FakeSubmitFile()
        {
            string results = "[{\"faceId\":\"4b3fe17b-3e75-4561-946f-f1679a6b7dce\",\"faceRectangle\":{\"top\":998,\"left\":969,\"width\":566,\"height\":566},\"faceAttributes\":{\"smile\":1.0,\"headPose\":{\"pitch\":1.8,\"roll\":0.7,\"yaw\":2.0},\"gender\":\"male\",\"age\":30.0,\"facialHair\":{\"moustache\":0.6,\"beard\":0.6,\"sideburns\":0.6},\"glasses\":\"NoGlasses\",\"emotion\":{\"anger\":0.0,\"contempt\":0.0,\"disgust\":0.0,\"fear\":0.0,\"happiness\":1.0,\"neutral\":0.0,\"sadness\":0.0,\"surprise\":0.0},\"blur\":{\"blurLevel\":\"low\",\"value\":0.0},\"exposure\":{\"exposureLevel\":\"goodExposure\",\"value\":0.49},\"noise\":{\"noiseLevel\":\"low\",\"value\":0.19},\"makeup\":{\"eyeMakeup\":false,\"lipMakeup\":false},\"accessories\":[{\"type\":\"headwear\",\"confidence\":1.0}],\"occlusion\":{\"foreheadOccluded\":true,\"eyeOccluded\":false,\"mouthOccluded\":false},\"hair\":{\"bald\":0.0,\"invisible\":true,\"hairColor\":[]}}}]";

            //if (file == null)
            //    return View("Results", null);

            //try
            //{
            //    results = await Task.Run(() => FileService.ProcessFileAsync(file));
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { success = false, error = "Something went wrong...", exception = ex });
            //}

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
