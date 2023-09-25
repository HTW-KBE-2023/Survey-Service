using API.Models.Surveys;
using API.Services;
using MassTransit;
using MessagingContracts.Survey;

namespace API.Port.MessageQueue.Consumers;

public class VoteOnSurveyChangedConsumer : IConsumer<VoteOnSurveyAdded>
{
    private readonly ILogger<VoteOnSurveyAdded> _logger;
    private readonly IGenericService<Survey> _surveyService;

    public VoteOnSurveyChangedConsumer(ILogger<VoteOnSurveyAdded> logger, IGenericService<Survey> surveyService)
    {
        _logger = logger;
        _surveyService = surveyService;
    }

    public Task Consume(ConsumeContext<VoteOnSurveyAdded> context)
    {
        var message = context.Message;

        var survey = _surveyService.GetById(message.SurveyId);
        if (survey is null)
        {
            return Task.CompletedTask;
        }

        var participant = survey.Participants.FirstOrDefault(option => option.Id == message.ParticipantId);
        var option = survey.SurveyOptions.FirstOrDefault(option => option.Id == message.OptionId);

        survey.AddVote(participant, option);

        _surveyService.Update(survey);

        return Task.CompletedTask;
    }
}