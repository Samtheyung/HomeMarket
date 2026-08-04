using HomeMarket.Services.Interfaces;

namespace HomeMarket.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;


        public ImageService(
            IWebHostEnvironment environment)
        {
            _environment = environment;
        }



        public async Task<string> UploadImageAsync(
            IFormFile file)
        {

            if (file == null || file.Length == 0)
                throw new Exception(
                    "Invalid image");


            var extension =
                Path.GetExtension(file.FileName);


            var fileName =
                $"{Guid.NewGuid()}{extension}";


            var folder =
                Path.Combine(
                    _environment.WebRootPath,
                    "images",
                    "products");


            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }


            var filePath =
                Path.Combine(
                    folder,
                    fileName);



            using (var stream =
                new FileStream(
                    filePath,
                    FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }



            return
                $"/images/products/{fileName}";
        }



        public Task DeleteImageAsync(string imagePath)
        {

            var fullPath =
                Path.Combine(
                    _environment.WebRootPath,
                    imagePath.TrimStart('/'));



            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }


            return Task.CompletedTask;
        }
    }
}
