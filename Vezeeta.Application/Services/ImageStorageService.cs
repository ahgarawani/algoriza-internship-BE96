using Microsoft.AspNetCore.Http;

namespace Vezeeta.Application.Services
{
    public class ImageStorageService
    {
        public static string SaveImage(IFormFile Image, int userId)
        {

            if (Image == null) return null;
            
            var rootDirpath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "ImageStorage");
            Directory.CreateDirectory(rootDirpath);
            var filename = $@"Image-User{userId}-{DateTime.Now.Ticks}{Path.GetExtension(Image.FileName)}";
            var path = Path.Combine(rootDirpath, filename);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                Image.CopyTo(stream);
            }

            return path;
        }

    }
}