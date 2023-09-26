using API.Models.Surveys;
using API.Services;
using MassTransit;
using MessagingContracts.Survey;

namespace API.Port.MessageQueue.Consumers;

public class VoteOnSurveyAddedConsumer : IConsumer<VoteOnSurveyChanged>
{
    private readonly ILogger<VoteOnSurveyAddedConsumer> _logger;
    private readonly IGenericService<Survey> _surveyService;

    public VoteOnSurveyAddedConsumer(ILogger<VoteOnSurveyAddedConsumer> logger, IGenericService<Survey> surveyService)
    {
        _logger = logger;
        _surveyService = surveyService;
    }

    public Task Consume(ConsumeContext<VoteOnSurveyChanged> context)
    {
        var message = context.Message;

        var survey = _surveyService.GetById(message.SurveyId);
        if (survey is null)
        {
            return Task.CompletedTask;
        }
        foreach (var (optionId, change) in message.Changes)
        {
            survey.ChangeVote(optionId, change);
        }

        _surveyService.Update(survey);

        return Task.CompletedTask;
    }
}