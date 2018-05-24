using System.Net.Http;
using System.Threading.Tasks;

namespace ImageEmotions.Services
{
    public static class CognitiveService
    {
        private const string Uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";
        private const string SubscriptionKey = "9d50196588bd4444ad9c23dd452f772c";

        //private const string Uri = "https://australiaeast.api.cognitive.microsoft.com/face/v1.0/recognize";
        //private const string SubscriptionKey = "6234f33a006f40029c1e54d86baedda4";

        public static async Task<string> Submit(HttpContent content)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

                using (var myResponse = await client.PostAsync(Uri, content))
                {
                    var results = await myResponse.Content.ReadAsStringAsync();
                    return results;
                }
            }
        }

        //async void MakeRequest()
        //{
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

        //}
    }
}