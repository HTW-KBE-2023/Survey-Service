namespace API.Models.Surveys;

public class SurveyTest
{
    [Fact]
    public void WhenSurveyIsCreatedThenIdShouldBeNull()
    {
        var survey = new Survey();

        Assert.NotEqual(Guid.Empty, survey.Id);
    }

    [Fact]
    public void WhenSurveyIsCreatedThenTitelShouldBeEmpty()
    {
        var survey = new Survey();

        Assert.NotNull(survey.Title);
        Assert.Equal(string.Empty, survey.Title);
    }

    [Fact]
    public void WhenSurveyIsCreatedThenDescriptionShouldBeEmpty()
    {
        var survey = new Survey();

        Assert.NotNull(survey.Description);
        Assert.Equal(string.Empty, survey.Description);
    }

    [Fact]
    public void WhenSurveyIsCreatedThenCompletedShouldBeFalse()
    {
        var survey = new Survey();

        Assert.False(survey.Completed);
    }

    [Fact]
    public void WhenSurveyOptionIsCreatedThenSurveyOptionsShouldBeEmpty()
    {
        var survey = new Survey();

        Assert.Empty(survey.SurveyOptions);
    }
}