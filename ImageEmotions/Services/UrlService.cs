using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ImageEmotions.Services
{
    public static class UrlService
    {
        public static async Task<string> ProcessUrlAsync(string url)
        {
            const string testImage = @"{'url': 'https://res.cloudinary.com/demo/image/upload/w_400/woman.jpg' }";

            var content = new StringContent(testImage);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await CognitiveService.Submit(content);
        }
    }
}