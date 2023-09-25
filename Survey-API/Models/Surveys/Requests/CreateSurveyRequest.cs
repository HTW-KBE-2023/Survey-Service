using API.Models.SurveyOptions;

namespace API.Models.Surveys.Requests;

public class CreateSurveyRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IList<SurveyOption> SurveyOptions { get; set; } = new List<SurveyOption>();
}