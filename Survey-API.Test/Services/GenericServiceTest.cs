using API.Models.SurveyOptions;
using API.Models.Surveys;
using API.Port.Database;
using API.Port.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Services.Surveys;

namespace API.Test.Services
{
    public class GenericServiceTest : IDisposable
    {
        private readonly SurveyContext _context;

        public GenericServiceTest()
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
        public void WhenEntityIsValidThenCreateShouldAddToDatabase()
        {
            var surveyValidator = new SurveyValidator();
            var unitOfWork = new UnitOfWork(_context);
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

            IGenericService<Survey> surveyService = new GenericService<Survey>(unitOfWork, surveyValidator);

            var result = surveyService.Create(survey);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.Equal(survey, result.Value);
            Assert.Single(_context.Surveys);
        }

        [Fact]
        public void WhenEntityIsNotValidThenCreateShouldReturnNegativeResult()
        {
            var surveyValidator = new SurveyValidator();
            var unitOfWork = new UnitOfWork(_context);

            var survey = new Survey()
            {
                Id = Guid.Empty
            };

            IGenericService<Survey> surveyService = new GenericService<Survey>(unitOfWork, surveyValidator);

            var result = surveyService.Create(survey);

            Assert.True(result.IsError);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);

            _ = result.Match(
                success =>
                {
                    Assert.Fail("Success schould not be called!");
                    return false;
                },
                failure =>
                {
                    Assert.Equal(3, failure.Errors.Count());
                    return true;
                }
            );

            Assert.Empty(_context.Surveys);
        }

        [Fact]
        public void WhenEntityIsValidThenUpdateShouldUpdateDatabaseRecord()
        {
            var surveyValidator = new SurveyValidator();

            SurveyOption option = new() { Position = 1, Text = "Option 1", TimesSelected = 0 };

            var survey = new Survey()
            {
                Title = "Nice Survey!",
                SurveyOptions =
                {
                    option,
                    new() { Position = 2, Text = "Option 2", TimesSelected = 0 }
                }
            };

            _context.Add(survey);
            _context.SaveChanges();

            Assert.Equal(0, _context.Surveys.First().SurveyOptions.First().TimesSelected);

            var unitOfWork = new UnitOfWork(_context);

            IGenericService<Survey> surveyService = new GenericService<Survey>(unitOfWork, surveyValidator);

            survey.Title = "Test";

            var result = surveyService.Update(survey);

            Assert.True(result.IsSuccess);
            Assert.False(result.IsError);
            Assert.Equal(survey, result.Value);
            Assert.Equal("Test", result.Value.Title);
        }

        [Fact]
        public void WhenEntityIsNotValidThenUpdateShouldReturnNegativeResultWithoutUpdatingDatabase()
        {
            var surveyValidator = new SurveyValidator();
            var unitOfWork = new UnitOfWork(_context);

            SurveyOption option = new() { Position = 1, Text = "Option 1", TimesSelected = 0 };

            var survey = new Survey()
            {
                SurveyOptions =
                {
                    option,
                    new() { Position = 2, Text = "Option 2", TimesSelected = 1 }
                }
            };

            _context.Add(survey);
            _context.SaveChanges();

            Assert.Equal(0, _context.Surveys.First().SurveyOptions.First().TimesSelected);

            IGenericService<Survey> surveyService = new GenericService<Survey>(unitOfWork, surveyValidator);

            survey.Description = "";

            var result = surveyService.Update(survey);

            Assert.True(result.IsError);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Value);

            _ = result.Match(
                success =>
                {
                    Assert.Fail("Success schould not be called!");
                    return false;
                },
                failure =>
                {
                    Assert.Single(failure.Errors);
                    return true;
                }
            );

            Assert.Single(_context.Surveys);
        }

        [Fact]
        public void WhenEntityDontExistThenUpdateShouldReturnDefaultEntityWithoutUpdatingDatabase()
        {
            var surveyValidator = new SurveyValidator();
            var unitOfWork = new UnitOfWork(_context);

            var survey = new Survey()
            {
                Title = "Nice Survey!",
                SurveyOptions =
                {
                    new() { Position = 1, Text = "Option 1", TimesSelected = 0 },
                    new() { Position = 2, Text = "Option 1", TimesSelected = 0 }
        }
            };

            IGenericService<Survey> surveyService = new GenericService<Survey>(unitOfWork, surveyValidator);

            var result = surveyService.Update(survey);

            Assert.False(result.IsError);
            Assert.True(result.IsSuccess);
            Assert.Equal(default, result.Value);

            _ = result.Match(
                success =>
                {
                    Assert.Equal(default, success);
                    return false;
                },
                failure =>
                {
                    Assert.Fail("Failed schould not be called!");
                    return true;
                }
            );

            Assert.Empty(_context.Surveys);
        }

        [Fact]
        public void WhenEntitiesExistThenGetAllShouldReturnAllRecords()
        {
            var surveyValidator = new SurveyValidator();
            var unitOfWork = new UnitOfWork(_context);

            var survey1 = new Survey()
            {
                Id = Guid.NewGuid()
            };
            var survey2 = new Survey()
            {
                Id = Guid.NewGuid()
            };
            _context.Add(survey1);
            _context.Add(survey2);
            _context.SaveChanges();

            IGenericService<Survey> surveyService = new GenericService<Survey>(unitOfWork, surveyValidator);
            var result = surveyService.GetAll();

            Assert.NotEmpty(result);
            Assert.Contains(survey1, result);
            Assert.Contains(survey2, result);
        }

        [Fact]
        public void WhenEntityExistThenGetByIdShouldReturnThisRecord()
        {
            var surveyValidator = new SurveyValidator();
            var unitOfWork = new UnitOfWork(_context);

            var survey1 = new Survey()
            {
            };
            var survey2 = new Survey()
            {
                Id = Guid.NewGuid()
            };
            _context.Add(survey1);
            _context.Add(survey2);
            _context.SaveChanges();

            IGenericService<Survey> surveyService = new GenericService<Survey>(unitOfWork, surveyValidator);
            var result = surveyService.GetById(survey1.Id);

            Assert.NotNull(result);
            Assert.Equal(survey1, result);
            Assert.Equal(survey1.Title, result.Title);
        }

        [Fact]
        public void WhenEntityExistThenDeleteShouldRemoveThisRecord()
        {
            var surveyValidator = new SurveyValidator();
            var unitOfWork = new UnitOfWork(_context);

            var survey1 = new Survey()
            {
                Id = Guid.NewGuid(),
                Title = "Survey Number 1",
            };
            var survey2 = new Survey()
            {
                Id = Guid.NewGuid(),
                Title = "Survey Number 1",
            };
            _context.Add(survey1);
            _context.Add(survey2);
            _context.SaveChanges();

            IGenericService<Survey> surveyService = new GenericService<Survey>(unitOfWork, surveyValidator);
            surveyService.DeleteById(survey1.Id);

            Assert.Single(_context.Surveys);
            Assert.DoesNotContain(survey1, _context.Surveys);
            Assert.Contains(survey2, _context.Surveys);
        }
    }
}