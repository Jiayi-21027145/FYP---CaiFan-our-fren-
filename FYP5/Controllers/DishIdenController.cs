/*using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;
using RP.SOI.DotNet.Utils;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Mono.TextTemplating;

namespace FYP5.Controllers;

public class DishIdenController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly AppDbContext _dbCtx;




        /* public IActionResult Index()
         {
             ViewData["userid"] =
             User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

             return View();
         }*/

    private readonly string PREDICTKEY = "0ffd60af00334e318c044feb4c735afa";
    private readonly string ENDPOINT = "https://fyppp.cognitiveservices.azure.com/customvision/v3.0/Prediction/6202816f-770e-4475-bf0f-4439480ee8e6/detect/iterations/Iteration4/image";

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        /*ViewData["userid"] =
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value;*/
        return View();
    }

    private string DoPhotoUpload(IFormFile photo)
    {
        string fext = Path.GetExtension(photo.FileName);
        string uname = Guid.NewGuid().ToString();
        string fname = uname + fext;
        string fullpath = Path.Combine(_env.WebRootPath, "photos/" + fname);
        using (FileStream fs = new(fullpath, FileMode.Create))
        {
            photo.CopyTo(fs);
        }
        return fname;
    }

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

        /*string predictionEndpoint = 
            $"{ENDPOINT}?Prediction-Key={PREDICTKEY}&Content-Type=application/octet-stream";

        string picfilename = DoPhotoUpload(set.Photo);
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

        return View("Result", resultString);*/


                    if (menuItem != null)
                    {
                        // Perform null checks on boundingBox values
                        var boundingBoxToken = p["boundingBox"];
                        if (boundingBoxToken != null)
                        {
                            // Create a BoundingBox object if all required properties are present
                            BoundingBox boundingBox = new BoundingBox
                            {
                                Left = boundingBoxToken["left"]?.Value<double>() ?? 0,
                                Top = boundingBoxToken["top"]?.Value<double>() ?? 0,
                                Width = boundingBoxToken["width"]?.Value<double>() ?? 0,
                                Height = boundingBoxToken["height"]?.Value<double>() ?? 0
                            };

                            // Now create the Prediction object
                            var predictionObj = new Prediction
                            {
                                TagName = tagName,
                                Probability = probability,
                                Box = boundingBox,
                                LowestPrice = menuItem.LowestPrice,
                                HighestPrice = menuItem.HighestPrice,
                                HighestNv = menuItem.HighestNv,
                                LowestNv = menuItem.LowestNv,
                                AverageNv = menuItem.AverageNv// Assuming you want to use AverageNv as the calorie count
                            };
                            set.Prediction.Add(predictionObj);
                        }
                    }
                }
            }
        }

        set.ImageName = picfilename;

       
        return View("Result", set);


    }

    }
}



