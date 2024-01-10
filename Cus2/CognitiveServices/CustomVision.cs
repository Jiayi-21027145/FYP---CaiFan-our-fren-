using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models;
using ApiKeyServiceClientCredentials = Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials;

namespace CognitiveServices;

public class CustomVision
{
	private string Key = "e832a2efc271455a8841f61716b060bc";
	private string Endpoint = "https://jiakpeng.cognitiveservices.azure.com/";
	private string ProjectID = "c664e071-4ac3-4e9a-9b96-34f3aab38e82";
	private string publishedModelName = "Iteration1";
	private double minProbability = 0.75;

	private CustomVisionTrainingClient trainingApi;
	private CustomVisionPredictionClient predictionApi;
	private Project project;

	public CustomVision()
	{
		trainingApi = AuthenticateTraining(Endpoint, Key);
		predictionApi = AuthenticatePrediction(Endpoint, Key);
		project = trainingApi.GetProject(new Guid(ProjectID));
	}

	private CustomVisionTrainingClient AuthenticateTraining(string endpoint, string trainingKey)
	{
		return new CustomVisionTrainingClient(new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(trainingKey))
		{
			Endpoint = endpoint
		};
	}

	private CustomVisionPredictionClient AuthenticatePrediction(string endpoint, string predictionKey)
	{
		return new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(predictionKey))
		{
			Endpoint = endpoint
		};
	}

	public List<PredictionModel> DetectObjects(string imageFile)
	{
		using (var stream = File.Open(imageFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			var result = predictionApi.DetectImage(project.Id, publishedModelName, stream);
			return result.Predictions.Where(x => x.Probability > minProbability).ToList();
		}
	}
}
