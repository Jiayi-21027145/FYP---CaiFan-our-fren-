using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision. CustomVision.Prediction.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace FYP5
{
    class Program
    {
        static void Main()
        {
            string trainingApiKey = "4980e1cfe48f4edf90ba3ae1f4c4ce08";
            string trainingEndpoint = "https://southeastasia.api.cognitive.microsoft.com/";
            string projectId = "8d326136-ae70-4a79-b84a-4fe2ea1f95e5";

            var customVisionService = new CustomVisionservice(trainingApiKey, trainingEndpoint, projectId);

            //specify the path to the image we want to upload 
            string imagePath = "path_to_your_image.jpg";

            //specify the tags for the image
            string[] tags = { "tag1", "tag2" };

            //Upload and tag the image
            customVisionService.UploadAndTagImage(imagePath, tags);
            Console.WriteLine("Image uploaded and tagged successfully!");
        }
    }
    public class CustomVisionservice
    {
        private string apiKey;
        private string endpoint;
        private string projectId;

        public CustomVisionservice (string apiKey, string endpoint, string projectId)
        {
            this.apiKey = apiKey;
            this.endpoint = endpoint;
            this.projectId = projectId;
        }

        public void UploadAndTagImage(string imagePath, string[] tags)
        {

            var trainingApi = new TrainingApi() { apiKey = apiKey, Endpoint = endpoint };

            var imageStream = File.OpenRead(imagePath);
            var image = trainingApi.CreateImagesFromData(projectId, imageStream, false);

            var imageId = image.Images[0].ImageId;
            var imageTags = tags.Select(tag => new ImageTagCreateEntry(tag)).ToList();

            trainingApi.CreateImagesTags(projectId, new ImageIdCreateBatch(imageId, imageTags));


        }
        }

   
     
}
