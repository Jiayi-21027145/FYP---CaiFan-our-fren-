/*using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using static System.Net.WebRequestMethods;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;


namespace FYP5.Controllers
{
    public class DishIdenController : Controller
    {


        private readonly string PREDICTKEY = "e832a2efc271455a8841f61716b060bc";
         private readonly string ENDPOINT = "https://jiakpeng.cognitiveservices.azure.com/customvision/v3.0/Prediction/c664e071-4ac3-4e9a-9b96-34f3aab38e82/detect/iterations/Iteration1/image";


         public IActionResult Index()
         {
             ViewData["userid"] =
             User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

             return View();
         }

        [HttpGet]
        //[AllowAnonymous]
        public IActionResult Add(string id)
        {
            ViewData["UserId"] =
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(ImageUploads im)
        {
            ModelState.Remove("ImageData");       // No Need to Validate "Photo" - cannot be changed.
                                                  //ModelState.Remove("SubmittedBy"); // Ignore "SubmittedBy". See claim below.
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["MsgType"] = "warning";
                return View("Add");
            }
            else
            {
                string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                string picfilename = DoPhotoUpload(im.photo);
                string sql = @"INSERT INTO ImageUploads (UserId, ImageLc, ImageDt,
                                   ImageData )
                            VALUES ({0}, '{1}', '{2:yyyy-MM-dd HH:mm}', '{3}')";

                string insert = string.Format(sql, userid, im.ImageLc, im.ImageDt,
                                              picfilename);

                if (DBUtl.ExecSQL(insert) == 1)
                {
                    TempData["Message"] = "Dish Identification Successfully Added.";
                    TempData["MsgType"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Message"] = DBUtl.DB_Message;
                    ViewData["ExecSQL"] = DBUtl.DB_SQL;
                    ViewData["MsgType"] = "danger";
                    return View("Add");
                }
            }
        }

        string predictionEndpoint = 
            $"{ENDPOINT}?Prediction-Key={PREDICTKEY}&Content-Type=application/octet-stream";

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
        string resultString = JsonConvert.SerializeObject(result);
        //JArray predictions = (JArray)result.GetValue("predictions");

        return View("Result", resultString);


        private string DoPhotoUpload(IFormFile photo)
        {
            string fext = Path.GetExtension(photo.FileName);
            string uname = Guid.NewGuid().ToString();
            string fname = uname + fext;
            string fullpath = Path.Combine(_env.WebRootPath, "ImageUploads/" + fname);
            using (FileStream fs = new(fullpath, FileMode.Create))
            {
                photo.CopyTo(fs);
            }
            return fname;
        }
        private readonly IWebHostEnvironment _env;

        public DishIdenController(IWebHostEnvironment environment)
        {
            _env = environment;
        }

    }
}*/



