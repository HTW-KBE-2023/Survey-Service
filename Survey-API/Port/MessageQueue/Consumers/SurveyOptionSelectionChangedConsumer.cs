using API.Models.SurveyOptions;
using API.Services;
using MassTransit;
using MessagingContracts.Survey;

namespace API.Port.MessageQueue.Consumers;

public class SurveyOptionSelectionChangedConsumer : IConsumer<SurveyOptionSelectionChanged>
{
    private readonly ILogger<SurveyOptionSelectionChangedConsumer> _logger;
    private readonly IGenericService<SurveyOption> _surveyOptionService;

    public SurveyOptionSelectionChangedConsumer(ILogger<SurveyOptionSelectionChangedConsumer> logger, IGenericService<SurveyOption> surveyOptionService)
    {
        _logger = logger;
        _surveyOptionService = surveyOptionService;
    }

    public Task Consume(ConsumeContext<SurveyOptionSelectionChanged> context)
    {
        var message = context.Message;

        var surveyOption = _surveyOptionService.GetById(message.SurveyOptionId);
        _surveyOptionService.Update(surveyOption);

        return Task.CompletedTask;
    }
}