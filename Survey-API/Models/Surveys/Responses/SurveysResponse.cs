namespace API.Models.Surveys.Responses;

public class SurveysResponse
{
    public IEnumerable<SurveyResponse> Items { get; set; } = Enumerable.Empty<SurveyResponse>();
}