using API.Models.SurveyOptions;
using API.Models.Surveys;
using API.Models.Surveys.Requests;
using API.Models.Surveys.Responses;
using Boxed.Mapping;
using MessagingContracts.Survey;

namespace API.Models.Surveys;

public class SurveyMapper :
    IMapper<Survey, SurveyResponse>,
    IMapper<SurveyCreated, Survey>,
    IMapper<Survey, SurveyCreated>,
    IMapper<CreateSurveyRequest, Survey>,
    IMapper<UpdateSurveyRequest, Survey>
{
    private readonly IMapper<SurveyOption, SurveyCreated.SurveyOption> _toMessageQueueMapper = new SurveyOptionMapper();
    private readonly IMapper<SurveyCreated.SurveyOption, SurveyOption> _fromMessageQueueMapper = new SurveyOptionMapper();

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

    public void Map(SurveyCreated source, Survey destination)
    {
        destination.Id = source.SurveyId;
        destination.Title = source.Title;
        destination.Description = source.Description;
        destination.SurveyOptions = _fromMessageQueueMapper.MapList(source.SurveyOptions);
    }

    public void Map(Survey source, SurveyCreated destination)
    {
        destination.SurveyId = source.Id;
        destination.Title = source.Title;
        destination.Description = source.Description;
        destination.SurveyOptions = _toMessageQueueMapper.MapList(source.SurveyOptions);
    }
}