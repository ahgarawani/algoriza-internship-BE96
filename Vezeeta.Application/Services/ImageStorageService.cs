using Microsoft.AspNetCore.Http;

namespace Vezeeta.Application.Services
{
    public class ImageStorageService
    {
        public static string SaveImage(IFormFile Image, int userId)
        {

            if (Image == null) return null;

            string StorageDirPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Vezeeta.Application", "ImageStorage");
            Directory.CreateDirectory(StorageDirPath);
            string filename = $@"Image-User{userId}-{DateTime.Now.Ticks}{Path.GetExtension(Image.FileName)}";
            string path = Path.Combine(StorageDirPath, filename);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                Image.CopyTo(stream);
            }

            return path;
        }

        public static void DeleteImage(string path)
        {
            if(File.Exists(path))
                File.Delete(path);

        }

    }
}