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
    public void WhenSurveyIsCreatedThenTitelShouldBeNull()
    {
        var survey = new Survey();

        Assert.Null(survey.Title);
    }

    [Fact]
    public void WhenSurveyIsCreatedThenDescriptionShouldBeNull()
    {
        var survey = new Survey();

        Assert.Null(survey.Description);
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

    [Fact]
    public void WhenSurveyOptionIsCreatedThenParticipantsShouldBeEmpty()
    {
        var survey = new Survey();

        Assert.Empty(survey.Participants);
    }
}