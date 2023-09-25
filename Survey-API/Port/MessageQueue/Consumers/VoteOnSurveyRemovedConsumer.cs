using API.Models.Surveys;
using API.Services;
using MassTransit;
using MessagingContracts.Survey;

namespace API.Port.MessageQueue.Consumers;

public class VoteOnSurveyRemovedConsumer : IConsumer<VoteOnSurveyRemoved>
{
    private readonly ILogger<VoteOnSurveyRemoved> _logger;
    private readonly IGenericService<Survey> _surveyService;

    public VoteOnSurveyRemovedConsumer(ILogger<VoteOnSurveyRemoved> logger, IGenericService<Survey> surveyService)
    {
        _logger = logger;
        _surveyService = surveyService;
    }

    public Task Consume(ConsumeContext<VoteOnSurveyRemoved> context)
    {
        var message = context.Message;

        var survey = _surveyService.GetById(message.SurveyId);
        if (survey is null)
        {
            return Task.CompletedTask;
        }

        var participant = survey.Participants.FirstOrDefault(option => option.Id == message.ParticipantId);
        var option = survey.SurveyOptions.FirstOrDefault(option => option.Id == message.OptionId);

        survey.RemoveVote(participant, option);

        _surveyService.Update(survey);

        return Task.CompletedTask;
    }
}