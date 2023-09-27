using API.Models.SurveyOptions;
using API.Models.Surveys;
using API.Models.Surveys.Requests;
using API.Models.Surveys.Responses;
using Boxed.Mapping;
using MessagingContracts.Survey;
using Services.Surveys;

namespace API.Test.Services.Surveys
{
    public class SurveyMapperTest

    {
        [Fact]
        public void WhenSurveyIsMappedToSurveyResponseThenPropertiesShouldBeTheSame()
        {
            IMapper<Survey, SurveyResponse> mapper = new SurveyMapper();
            var survey = new Survey()
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                Completed = true,
                SurveyOptions = new List<SurveyOption>()
                {
                    new(){ Id = Guid.NewGuid(), Text = "Text1", Position = 0, TimesSelected = 1 },
                    new(){ Id = Guid.NewGuid(), Text = "Text2", Position = 1, TimesSelected = 543 },
                    new(){ Id = Guid.NewGuid(), Text = "Text3", Position = 2, TimesSelected = 66 }
                }
            };

            var response = mapper.Map(survey);

            Assert.Equal(survey.Id, response.Id);
            Assert.Equal(survey.Title, response.Title);
            Assert.Equal(survey.Description, response.Description);
            Assert.Equal(survey.Completed, response.Completed);
            Assert.Equal(survey.SurveyOptions.Count, response.SurveyOptions.Count);

            for (int i = 0; i < survey.SurveyOptions.Count; i++)
            {
                Assert.Equal(survey.SurveyOptions[i].Id, response.SurveyOptions[i].Id);
                Assert.Equal(survey.SurveyOptions[i].Text, response.SurveyOptions[i].Text);
                Assert.Equal(survey.SurveyOptions[i].Position, response.SurveyOptions[i].Position);
                Assert.Equal(survey.SurveyOptions[i].TimesSelected, response.SurveyOptions[i].TimesSelected);
            }
        }

        [Fact]
        public void WhenSurveyIsMappedToSurveyCreatedThenPropertiesShouldBeTheSame()
        {
            IMapper<Survey, SurveyCreated> mapper = new SurveyMapper();
            var survey = new Survey()
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                Completed = true,
                SurveyOptions = new List<SurveyOption>()
                {
                    new(){ Id = Guid.NewGuid(), Text = "Text1", Position = 0, TimesSelected = 1 },
                    new(){ Id = Guid.NewGuid(), Text = "Text2", Position = 1, TimesSelected = 543 },
                    new(){ Id = Guid.NewGuid(), Text = "Text3", Position = 2, TimesSelected = 66 }
                }
            };

            var surveyCreated = mapper.Map(survey);

            Assert.Equal(survey.Id, surveyCreated.SurveyId);
            Assert.Equal(survey.Title, surveyCreated.Title);
            Assert.Equal(survey.Description, surveyCreated.Description);
            Assert.Equal(survey.SurveyOptions.Count, surveyCreated.SurveyOptions.Count);

            for (int i = 0; i < survey.SurveyOptions.Count; i++)
            {
                Assert.Equal(survey.SurveyOptions[i].Id, surveyCreated.SurveyOptions[i].SurveyOptionId);
                Assert.Equal(survey.SurveyOptions[i].Text, surveyCreated.SurveyOptions[i].Text);
                Assert.Equal(survey.SurveyOptions[i].Position, surveyCreated.SurveyOptions[i].Position);
                Assert.Equal(survey.SurveyOptions[i].TimesSelected, surveyCreated.SurveyOptions[i].TimesSelected);
            }
        }

        [Fact]
        public void WhenSurveyCreatedIsMappedToSurveyThenPropertiesShouldBeTheSame()
        {
            IMapper<SurveyCreated, Survey> mapper = new SurveyMapper();
            var surveyCreated = new SurveyCreated()
            {
                SurveyId = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                SurveyOptions = new List<Option>()
                {
                    new(){ SurveyOptionId = Guid.NewGuid(), Text = "Text1", Position = 0, TimesSelected = 1 },
                    new(){ SurveyOptionId = Guid.NewGuid(), Text = "Text2", Position = 1, TimesSelected = 543 },
                    new(){ SurveyOptionId = Guid.NewGuid(), Text = "Text3", Position = 2, TimesSelected = 66 }
                }
            };

            var survey = mapper.Map(surveyCreated);

            Assert.Equal(surveyCreated.SurveyId, survey.Id);
            Assert.Equal(surveyCreated.Title, survey.Title);
            Assert.Equal(surveyCreated.Description, survey.Description);
            Assert.Equal(surveyCreated.SurveyOptions.Count, survey.SurveyOptions.Count);

            for (int i = 0; i < survey.SurveyOptions.Count; i++)
            {
                Assert.Equal(surveyCreated.SurveyOptions[i].SurveyOptionId, survey.SurveyOptions[i].Id);
                Assert.Equal(surveyCreated.SurveyOptions[i].Text, survey.SurveyOptions[i].Text);
                Assert.Equal(surveyCreated.SurveyOptions[i].Position, survey.SurveyOptions[i].Position);
                Assert.Equal(surveyCreated.SurveyOptions[i].TimesSelected, survey.SurveyOptions[i].TimesSelected);
            }
        }

        [Fact]
        public void WhenCreateSurveyRequestIsMappedToSurveyThenPropertiesShouldBeTheSame()
        {
            IMapper<CreateSurveyRequest, Survey> mapper = new SurveyMapper();
            var surveyCreated = new CreateSurveyRequest()
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                SurveyOptions = new List<SurveyOption>()
                {
                    new(){ Id = Guid.NewGuid(), Text = "Text1", Position = 0, TimesSelected = 1 },
                    new(){ Id = Guid.NewGuid(), Text = "Text2", Position = 1, TimesSelected = 543 },
                    new(){ Id = Guid.NewGuid(), Text = "Text3", Position = 2, TimesSelected = 66 }
                }
            };

            var survey = mapper.Map(surveyCreated);

            Assert.Equal(surveyCreated.Id, survey.Id);
            Assert.Equal(surveyCreated.Title, survey.Title);
            Assert.Equal(surveyCreated.Description, survey.Description);
            Assert.Equal(surveyCreated.SurveyOptions.Count, survey.SurveyOptions.Count);

            for (int i = 0; i < survey.SurveyOptions.Count; i++)
            {
                Assert.Equal(surveyCreated.SurveyOptions[i].Id, survey.SurveyOptions[i].Id);
                Assert.Equal(surveyCreated.SurveyOptions[i].Text, survey.SurveyOptions[i].Text);
                Assert.Equal(surveyCreated.SurveyOptions[i].Position, survey.SurveyOptions[i].Position);
                Assert.Equal(surveyCreated.SurveyOptions[i].TimesSelected, survey.SurveyOptions[i].TimesSelected);
            }
        }

        [Fact]
        public void WhenUpdateSurveyRequesttIsMappedToSurveyThenPropertiesShouldBeTheSame()
        {
            IMapper<UpdateSurveyRequest, Survey> mapper = new SurveyMapper();
            var surveyUpdated = new UpdateSurveyRequest()
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                SurveyOptions = new List<SurveyOption>()
                {
                    new(){ Id = Guid.NewGuid(), Text = "Text1", Position = 0, TimesSelected = 1 },
                    new(){ Id = Guid.NewGuid(), Text = "Text2", Position = 1, TimesSelected = 543 },
                    new(){ Id = Guid.NewGuid(), Text = "Text3", Position = 2, TimesSelected = 66 }
                }
            };

            var survey = mapper.Map(surveyUpdated);

            Assert.Equal(surveyUpdated.Id, survey.Id);
            Assert.Equal(surveyUpdated.Title, survey.Title);
            Assert.Equal(surveyUpdated.Description, survey.Description);
            Assert.Equal(surveyUpdated.SurveyOptions.Count, survey.SurveyOptions.Count);

            for (int i = 0; i < survey.SurveyOptions.Count; i++)
            {
                Assert.Equal(surveyUpdated.SurveyOptions[i].Id, survey.SurveyOptions[i].Id);
                Assert.Equal(surveyUpdated.SurveyOptions[i].Text, survey.SurveyOptions[i].Text);
                Assert.Equal(surveyUpdated.SurveyOptions[i].Position, survey.SurveyOptions[i].Position);
                Assert.Equal(surveyUpdated.SurveyOptions[i].TimesSelected, survey.SurveyOptions[i].TimesSelected);
            }
        }
    }
}