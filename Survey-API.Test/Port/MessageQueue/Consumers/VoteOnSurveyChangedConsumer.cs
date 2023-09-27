using API.Models.Surveys;
using API.Port.MessageQueue.Consumers;
using API.Services;
using Castle.Core.Logging;
using MassTransit;
using MessagingContracts.Survey;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Surveys;

namespace API.Test.Port.MessageQueue.Consumers;

public class VoteOnSurveyAddedConsumerTest
{
    //[Fact]
    //public void WhenContextIsCreatedThenContainsOneSetForeachModel()
    //{
    //    var fakeLogger = new Mock<ILogger<VoteOnSurveyAddedConsumer>>();
    //    var fakeService = new Mock<>();

    // var service = new SurveyService();

    // VoteOnSurveyAddedConsumer a = new VoteOnSurveyAddedConsumer(fakeLogger.Object, fakeService.Object);

    //    a.Consume();
    //}
}