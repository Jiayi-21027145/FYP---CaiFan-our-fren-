using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FYP5.Controllers;

public class DishIdenController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly AppDbContext _dbCtx;

    public DishIdenController(IWebHostEnvironment environment, AppDbContext dbCtx)
    {
        _env = environment;
        _dbCtx = dbCtx;
        DbSet<Menu> dbs = _dbCtx.Menu;

    }


    private readonly string PREDICTKEY = "0ffd60af00334e318c044feb4c735afa";
    private readonly string ENDPOINT = "https://fyppp.cognitiveservices.azure.com/customvision/v3.0/Prediction/6202816f-770e-4475-bf0f-4439480ee8e6/detect/iterations/Iteration4/image";

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    private string DoPhotoUpload(IFormFile photo)
    {
        string fext = Path.GetExtension(photo.FileName);
        string uname = Guid.NewGuid().ToString();
        string fname = uname + fext;
        string fullpath = Path.Combine(_env.WebRootPath, "UserHistory/" + fname);
        using (FileStream fs = new(fullpath, FileMode.Create))
        {
            photo.CopyTo(fs);
        }
        return fname;
    }

    [Authorize]
    [HttpPost]
    public IActionResult Index(Dataset set, IFormFile photo)
    {
        ModelState.Remove("Picture");     // No Need to Validate "Picture" - derived from "Photo".

        string predictionEndpoint =
           $"{ENDPOINT}?Prediction-Key={PREDICTKEY}&Content-Type=application/octet-stream";

        _dbCtx.Dataset.Add(set);
        set.UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        set.Picture = DoPhotoUpload(set.Photo); ;
        int res = _dbCtx.SaveChanges();
        
        // Create the HTTP client and request headers
        HttpClient client = new HttpClient();
        // Read the image file into a byte array
        byte[] imageData;
        using (var stream = photo.OpenReadStream())
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                imageData = memoryStream.ToArray();
            }
        }

        // Set the content type and body of the request
        HttpContent content = new ByteArrayContent(imageData);

        // Make the prediction request
        HttpResponseMessage response = client.PostAsync(predictionEndpoint, content).Result;

        // Read the response and parse the prediction results
        string responseString = response.Content.ReadAsStringAsync().Result;
        dynamic result = JObject.Parse(responseString);
        JArray predictions = result.predictions;
        set.Prediction = new List<Prediction>();
        foreach (var p in predictions)
        {
            double probability = p["probability"]?.Value<double>() ?? 0;
            if (probability >= 0.9)
            {
                string? tagName = p["tagName"]?.ToString();
                if (!string.IsNullOrEmpty(tagName))
                {
                    var menuItem = _dbCtx.Menu
                     .Where(m => EF.Functions.Like(m.FoodName, tagName))
                     .FirstOrDefault();

                    if (menuItem != null)
                    {
                        // Perform null checks on boundingBox values
                        var boundingBoxToken = p["boundingBox"];
                        if (boundingBoxToken != null)
                        {
                            // Now create the Prediction object
                            var predictionObj = new Prediction
                            {
                                Lefts = boundingBoxToken["left"]?.Value<double>() ?? 0,
                                Tops = boundingBoxToken["top"]?.Value<double>() ?? 0,
                                Width = boundingBoxToken["width"]?.Value<double>() ?? 0,
                                Height = boundingBoxToken["height"]?.Value<double>() ?? 0,
                                //DatasetId = set.DatasetId,
                                MenuId = menuItem.MenuId,
                                TagName = tagName,
                                Probability = probability,
                                LowestPrice = menuItem.LowestPrice,
                                HighestPrice = menuItem.HighestPrice,
                                HighestNv = menuItem.HighestNv,
                                LowestNv = menuItem.LowestNv,
                                AverageNv = menuItem.AverageNv// Assuming you want to use AverageNv as the calorie count
                            };
                            set.Prediction.Add(predictionObj);
                            int _ = _dbCtx.SaveChanges();

                            if (res > 0 && set.Prediction.Count > 0)
                            {
                                
                                // Assuming your History model has properties to store summary information
                                // such as total calories and average price, and a way to represent the dishes
                                int highestCalories = set.Prediction.Sum(p => p.HighestNv ?? 0);
                                int lowestCalories = set.Prediction.Sum(p => p.LowestNv ?? 0);
                                decimal avgPriceRange = set.Prediction.Average(p => (p.LowestPrice ?? 0 + p.HighestPrice ?? 0) / 2);
                                string dishes = string.Join(", ", set.Prediction.Select(p => p.TagName).Distinct());
                                decimal highestPrice = set.Prediction.Sum(p => p.HighestPrice ?? 0);
                                decimal lowestPrice = set.Prediction.Sum(p => p.LowestPrice ?? 0);
                                var tagNames = set.Prediction.Select(p => p.TagName).Distinct().Take(6).ToList();

                                // Add null entries if there are less than 6 predictions to avoid index out of range issues.
                                while (tagNames.Count < 6)
                                {
                                    tagNames.Add(null);
                                }
                                var existingHistory = _dbCtx.History.FirstOrDefault(h => h.UserId == set.UserId && h.UploadDate.Date == set.DateTime.Date && h.Location == set.Location);

                                if (existingHistory != null)
                                {
                                    // Update the existing record to prevent duplicates
                                    existingHistory.DishOne = tagNames.ElementAtOrDefault(0) ?? string.Empty;
                                    existingHistory.DishTwo = tagNames.ElementAtOrDefault(1) ?? string.Empty;
                                    existingHistory.DishThree = tagNames.ElementAtOrDefault(2) ?? string.Empty;
                                    existingHistory.DishFour = tagNames.ElementAtOrDefault(3) ?? string.Empty;
                                    existingHistory.DishFive = tagNames.ElementAtOrDefault(4) ?? string.Empty;
                                    existingHistory.DishSix = tagNames.ElementAtOrDefault(5) ?? string.Empty;
                                    existingHistory.CaloriesRange = $"{lowestCalories} - {highestCalories} kcal";
                                    existingHistory.AverageCalories = (highestCalories + lowestCalories) / 2;
                                    existingHistory.PriceRange = $"{lowestPrice:C} - {highestPrice:C}";
                                    existingHistory.AveragePrice = (highestPrice + lowestPrice) / 2;
                                    _dbCtx.Update(existingHistory);
                                }
                                else
                                {
                                    // No existing record, so create a new one
                                    var history = new History
                                    {

                                        UserId = set.UserId,
                                        UploadDate = set.DateTime, // Or DateTime.Now if you want the upload timestamp
                                        Location = set.Location,
                                        DishOne = tagNames.ElementAtOrDefault(0) ?? string.Empty, // These will be null if there's no prediction
                                        DishTwo = tagNames.ElementAtOrDefault(1) ?? string.Empty,
                                        DishThree = tagNames.ElementAtOrDefault(2) ?? string.Empty,
                                        DishFour = tagNames.ElementAtOrDefault(3) ?? string.Empty,
                                        DishFive = tagNames.ElementAtOrDefault(4) ?? string.Empty,
                                        DishSix = tagNames.ElementAtOrDefault(5) ?? string.Empty,
                                        CaloriesRange = $"{lowestCalories} - {highestCalories} kcal", // Again, adjust as needed
                                        AverageCalories = (highestCalories + lowestCalories) / 2,
                                        PriceRange = $"{lowestPrice:C} - {highestPrice:C}",
                                        AveragePrice = avgPriceRange,
                                        Image = set.Picture // or the filename you saved from the photo upload
                                    };
                                    _dbCtx.History.Add(history);
                                }

                                // Finally, save changes to the database
                                _dbCtx.SaveChanges();

                            }
                        }
                    }
                }
            }
        }

        return View("Result", set);
    }

    
 
       
    
}

