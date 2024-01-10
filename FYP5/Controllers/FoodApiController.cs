using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FYP5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodApiController : ControllerBase
    {

        private readonly string _predictionKey = "e832a2efc271455a8841f61716b060bc";
        private readonly string _endpointUrlForImageUrl = "https://jiakpeng.cognitiveservices.azure.com/customvision/v3.0/Prediction/c664e071-4ac3-4e9a-9b96-34f3aab38e82/detect/iterations/Iteration1/url";
        //private readonly string _endpointUrlForImageFile = "https://jiakpeng.cognitiveservices.azure.com/customvision/v3.0/Prediction/c664e071-4ac3-4e9a-9b96-34f3aab38e82/detect/iterations/Iteration1/image";

        [HttpPost("predict-url")]
        public async Task<IActionResult> PredictFromUrl(string imageUrl)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Prediction-Key", _predictionKey);

                var json = JsonConvert.SerializeObject(new { Url = imageUrl });
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_endpointUrlForImageUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                return new ContentResult { Content = responseString, ContentType = "application/json" };
            }
        }





        // GET: api/<FoodApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FoodApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FoodApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FoodApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FoodApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
