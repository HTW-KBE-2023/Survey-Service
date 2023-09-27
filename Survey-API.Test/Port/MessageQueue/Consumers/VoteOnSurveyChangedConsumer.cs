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
        var survey = new Survey()
        {
            Id = Guid.NewGuid(),
            SurveyOptions = new List<SurveyOption>()
            {
                new (){ Id = Guid.NewGuid(), TimesSelected = 0 }
            }
        };

        var fakeLogger = new Mock<ILogger<VoteOnSurveyAddedConsumer>>();
        var fakeGenericService = new Mock<IGenericService<Survey>>();

        fakeGenericService.Setup(mock => mock.GetById(It.IsAny<Guid>())).Verifiable(Times.Once);
        fakeGenericService.Setup(mock => mock.Update(It.IsAny<Survey>())).Verifiable(Times.Once);

        fakeGenericService.Setup(mock => mock.GetById(It.IsAny<Guid>())).Returns(survey);

        var fakeContext = new Mock<ConsumeContext<VoteOnSurveyChanged>>();

        fakeContext.Setup(mock => mock.Message).Returns(new VoteOnSurveyChanged()
        {
            SurveyId = survey.Id,
            Changes = new List<(Guid, int)>() {
                (survey.SurveyOptions.First().Id, 1)
            }
        });

        var consumer = new VoteOnSurveyAddedConsumer(fakeLogger.Object, fakeGenericService.Object);

        consumer.Consume(fakeContext.Object);

        fakeGenericService.Verify();
    }
}