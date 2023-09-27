using API.Models.SurveyOptions;
using API.Models.Surveys;
using API.Port.MessageQueue.Consumers;
using API.Services;
using MassTransit;
using MessagingContracts.Survey;
using Microsoft.Extensions.Logging;
using Moq;

namespace API.Test.Port.MessageQueue.Consumers;

public class VoteOnSurveyAddedConsumerTest
{
    [Fact]
    public void WhenContextIsCreatedThenContainsOneSetForeachModel()
    {
        var surveyOption = new SurveyOption()
        {
            Id = Guid.NewGuid(),
            TimesSelected = 0
        };

        var fakeLogger = new Mock<ILogger<SurveyOptionSelectionChangedConsumer>>();
        var fakeGenericService = new Mock<IGenericService<SurveyOption>>();

        fakeGenericService.Setup(mock => mock.GetById(It.IsAny<Guid>())).Verifiable(Times.Once);
        fakeGenericService.Setup(mock => mock.Update(It.IsAny<SurveyOption>())).Verifiable(Times.Once);

        fakeGenericService.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns(surveyOption);

        var fakeContext = new Mock<ConsumeContext<SurveyOptionSelectionChanged>>();

        fakeContext.Setup(mock => mock.Message).Returns(new SurveyOptionSelectionChanged()
        {
            SurveyOptionId = surveyOption.Id,
            TimesSelected = 1
        });

        var consumer = new SurveyOptionSelectionChangedConsumer(fakeLogger.Object, fakeGenericService.Object);

        consumer.Consume(fakeContext.Object);

        fakeGenericService.Verify();
    }
}