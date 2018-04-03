using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ImageEmotions.Services
{
    public static class ImageService
    {
        public static async Task<string> ProcessImageAsync(byte[] byteData)
        {
            //process byte data
            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                return await CognitiveService.Submit(content);
            }
        }
    }
}