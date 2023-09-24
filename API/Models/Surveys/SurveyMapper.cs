using API.Models.Surveys;
using API.Models.Surveys.Requests;
using API.Models.Surveys.Responses;
using Boxed.Mapping;

namespace API.Models.Surveys;

public class SurveyMapper : IMapper<Survey, SurveyResponse>, IMapper<CreateSurveyRequest, Survey>, IMapper<UpdateSurveyRequest, Survey>
{
    public void Map(Survey source, SurveyResponse destination)
    {
        destination.Id = source.Id;
        destination.Title = source.Title;
        destination.Description = source.Description;
        destination.Completed = source.Completed;
        destination.SurveyOptions = source.SurveyOptions;
        destination.Participants = source.Participants;
    }

    public void Map(CreateSurveyRequest source, Survey destination)
    {
        destination.Id = source.Id;
        destination.Title = source.Title;
        destination.Description = source.Description;
        destination.Completed = source.Completed;
        destination.SurveyOptions = source.SurveyOptions;
    }

    public void Map(UpdateSurveyRequest source, Survey destination)
    {
        destination.Id = source.Id;
        destination.Title = source.Title;
        destination.Description = source.Description;
        destination.Completed = source.Completed;
        destination.SurveyOptions = source.SurveyOptions;
    }
}