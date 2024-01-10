using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FYP5.Controllers
{
    public class DishIdenController : Controller
    {
       /* public IActionResult Index()
        {
            *//*ViewData["userid"] =
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value;*//*

            return View();
        }*/

        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Construct the URL to the uploaded image
                var imageUrl = Url.Content($"~/wwwroot/{fileName}");

                // Make an HTTP call to the API
                using (var client = new HttpClient())
                {
                    // If your application is running on localhost, you can use this as the base address
                    // If it's hosted, you'll need the base address of your application
                    client.BaseAddress = new Uri("http://localhost:5000"); // Replace with your application's URL

                    // Construct the relative URL to your API endpoint
                    var apiEndpoint = $"/api/FoodApi/predict-url?imageUrl={Uri.EscapeDataString(imageUrl)}";

                    HttpResponseMessage response = await client.PostAsync(apiEndpoint, null);

                    if (response.IsSuccessStatusCode)
                    // To get the 200 
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        // Now, return this content to your view or process it as needed
                        return View("Result", responseContent);
                    }
                    else
                    {
                        // Handle errors
                        return View("Error");
                    }
                }
            }

            return View("Error"); // Or however you want to handle errors
        }
    }
}
