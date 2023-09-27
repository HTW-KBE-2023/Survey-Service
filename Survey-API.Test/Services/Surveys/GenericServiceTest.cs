using API.Models.SurveyOptions;
using API.Models.Surveys;
using API.Port.Database;
using API.Port.Repositories;
using API.Services;
using MassTransit;
using MessagingContracts.Survey;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services.Surveys;

namespace API.Test.Services.Surveys
{
    public class SurveyServiceTest : IDisposable
    {
        private readonly SurveyContext _context;

        public SurveyServiceTest()
        {
            var options = new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(databaseName: nameof(GenericServiceTest))
                .Options;

            _context = new SurveyContext(options);

            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void WhenCreateIsCalledThenTheUnderlyingServiceShouldBeCalledAndMessagePublished()
        {
            var survey = new Survey()
            {
                Completed = false,
                Title = "Nice Survey!",
                Description = "Description",
                SurveyOptions =
                {
                    new() {
                        Position = 1,
                        Text = "Option 1",
                        TimesSelected = 1
                    },
                    new() {
                        Position = 2,
                        Text = "Option 2",
                        TimesSelected = 0
                    }
                }
            };

            var mapper = new SurveyMapper();
            var fakeBus = new Mock<IBus>();
            var fakeGenericService = new Mock<IGenericService<Survey>>();

            fakeBus.Setup(mock => mock.Publish(It.IsAny<SurveyCreated>(), default)).Verifiable(Times.Once);
            fakeGenericService.Setup(mock => mock.Create(survey)).Verifiable(Times.Once);

            SurveyService service = new(fakeGenericService.Object, mapper, fakeBus.Object);

            service.Create(survey);

            fakeBus.Verify();
            fakeGenericService.Verify();
        }

        [Fact]
        public void WhenUpdateIsCalledThenTheUnderlyingServiceShouldBeCalled()
        {
            var survey = new Survey()
            {
                Completed = false,
                Title = "Nice Survey!",
                Description = "Description",
                SurveyOptions =
                {
                    new() {
                        Position = 1,
                        Text = "Option 1",
                        TimesSelected = 1
                    },
                    new() {
                        Position = 2,
                        Text = "Option 2",
                        TimesSelected = 0
                    }
                }
            };

            var mapper = new SurveyMapper();
            var fakeBus = new Mock<IBus>();
            var fakeGenericService = new Mock<IGenericService<Survey>>();

            fakeGenericService.Setup(mock => mock.Update(survey)).Verifiable(Times.Once);

            SurveyService service = new(fakeGenericService.Object, mapper, fakeBus.Object);

            service.Update(survey);

            fakeGenericService.Verify();
        }

        [Fact]
        public void WhenGetByIdIsCalledThenTheUnderlyingServiceShouldBeCalled()
        {
            var surveyId = Guid.NewGuid();

            var mapper = new SurveyMapper();
            var fakeBus = new Mock<IBus>();
            var fakeGenericService = new Mock<IGenericService<Survey>>();

            fakeGenericService.Setup(mock => mock.GetById(surveyId)).Verifiable(Times.Once);

            SurveyService service = new(fakeGenericService.Object, mapper, fakeBus.Object);

            service.GetById(surveyId);

            fakeGenericService.Verify();
        }

        [Fact]
        public void WhenGetAllIsCalledThenTheUnderlyingServiceShouldBeCalled()
        {
            var mapper = new SurveyMapper();
            var fakeBus = new Mock<IBus>();
            var fakeGenericService = new Mock<IGenericService<Survey>>();

            fakeGenericService.Setup(mock => mock.GetAll()).Verifiable(Times.Once);

            SurveyService service = new(fakeGenericService.Object, mapper, fakeBus.Object);

            service.GetAll();

            fakeGenericService.Verify();
        }

        [Fact]
        public void WhenDeleteByIdIsCalledThenTheUnderlyingServiceShouldBeCalled()
        {
            var surveyId = Guid.NewGuid();

            var mapper = new SurveyMapper();
            var fakeBus = new Mock<IBus>();
            var fakeGenericService = new Mock<IGenericService<Survey>>();

            fakeGenericService.Setup(mock => mock.DeleteById(surveyId)).Verifiable(Times.Once);

            SurveyService service = new(fakeGenericService.Object, mapper, fakeBus.Object);

            service.DeleteById(surveyId);

            fakeGenericService.Verify();
        }
    }
}