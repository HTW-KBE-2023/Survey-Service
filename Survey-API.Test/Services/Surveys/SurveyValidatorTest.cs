using API.Models.SurveyOptions;
using API.Models.Surveys;
using Services.Surveys;

namespace API.Test.Services.Surveys;

public class SurveyValidatorTest
{
    [Fact]
    public void WhenSurveyIsFilledThenValidateShouldReturnValid()
    {
        var survey = new Survey()
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Description = "Description",
            SurveyOptions = new List<SurveyOption>() {
                new() { },
                new() { }
            }
        };

        var surveyValidator = new SurveyValidator();

        var result = surveyValidator.Validate(survey);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void WhenSurveyHasNoOptionsThenValidateShouldReturnInvalid()
    {
        var survey = new Survey()
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Description = "Description"
        };

        var surveyValidator = new SurveyValidator();

        var result = surveyValidator.Validate(survey);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("The Survey must have between 2 and 8 Options (Inklusiv)", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public void WhenSurveyHasMoreAsEightOptionsThenValidateShouldReturnInvalid()
    {
        var survey = new Survey()
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Description = "Description",
            SurveyOptions = new List<SurveyOption>() {
                new() { },
                new() { },
                new() { },
                new() { },
                new() { },
                new() { },
                new() { },
                new() { },
                new() { }
            }
        };

        var surveyValidator = new SurveyValidator();

        var result = surveyValidator.Validate(survey);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal("The Survey must have between 2 and 8 Options (Inklusiv)", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public void WhenSurveyIsWithoutTitelThenValidateShouldReturnInvalid()
    {
        var survey = new Survey()
        {
            Id = Guid.NewGuid(),
            Description = "Description",
            SurveyOptions = new List<SurveyOption>() {
                new() { },
                new() { }
            }
        };

        var surveyValidator = new SurveyValidator();

        var result = surveyValidator.Validate(survey);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void WhenSurveyIsWithoutDescriptionThenValidateShouldReturnInvalid()
    {
        var survey = new Survey()
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            Description = null,
            SurveyOptions = new List<SurveyOption>() {
                new() { },
                new() { }
            }
        };

        var surveyValidator = new SurveyValidator();

        var result = surveyValidator.Validate(survey);

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
    }
}