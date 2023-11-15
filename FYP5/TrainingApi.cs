using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;

namespace FYP5
{
    internal class TrainingApi
    {
        public TrainingApi()
        {
        }

        public string? apiKey { get; set; }
        public string? Endpoint { get; set; }

        internal void CreateImagesTags(string projectId, ImageIdCreateBatch imageIdCreateBatch)
        {
            throw new NotImplementedException();
        }
    }
}