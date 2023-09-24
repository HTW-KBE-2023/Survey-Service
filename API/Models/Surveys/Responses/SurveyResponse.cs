using API.Models.Participants;
using API.Models.SurveyOptions;

namespace API.Models.Surveys.Responses;

public class SurveyResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Completed { get; set; }
    public IList<Participant> Participants { get; set; } = new List<Participant>();
    public IList<SurveyOption> SurveyOptions { get; set; } = new List<SurveyOption>();
}