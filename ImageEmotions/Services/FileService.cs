using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace ImageEmotions.Services
{
    public static class FileService
    {
        public static async Task<string> ProcessFileAsync(HttpPostedFileBase file)
        {
            //convert HttpPostedFileBase into a octet stream
            BinaryReader b = new BinaryReader(file.InputStream);
            byte[] byteData = b.ReadBytes(file.ContentLength);

            return await ImageService.ProcessImageAsync(byteData);

        }
    }
}