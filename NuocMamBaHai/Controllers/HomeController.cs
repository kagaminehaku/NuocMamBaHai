using Microsoft.AspNetCore.Mvc;
using NuocMamBaHai.Models;
using System.Diagnostics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

namespace NuocMamBaHai.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var selectedImages = GetRandomImages(4);
            return View(selectedImages);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string[] GetRandomImages(int count)
        {
            var imageDirectory = "wwwroot/images";
            var imageFiles = Directory.GetFiles(imageDirectory, "*.jpg");

            var random = new Random();
            var selectedImages = imageFiles.OrderBy(x => random.Next()).Take(count).ToArray();
            var targetWidth = 300;
            var targetHeight = 450;

            for (int i = 0; i < selectedImages.Length; i++)
            {
                using (var image = Image.Load(selectedImages[i]))
                {
                    image.Mutate(x => x
                        .Resize(new ResizeOptions
                        {
                            Size = new Size(targetWidth, targetHeight),
                            Mode = ResizeMode.Max
                        }));
                    image.Save(selectedImages[i], new JpegEncoder());
                }
            }
            for (int i = 0; i < selectedImages.Length; i++)
            {
                selectedImages[i] = selectedImages[i].Replace('\\', '/').Replace("wwwroot/", "");
            }

            return selectedImages;
        }
    }
}
