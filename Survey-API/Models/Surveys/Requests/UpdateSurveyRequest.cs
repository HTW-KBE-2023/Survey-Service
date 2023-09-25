using API.Models.SurveyOptions;

namespace API.Models.Surveys.Requests;

public class UpdateSurveyRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public IList<SurveyOption> SurveyOptions { get; set; }
}