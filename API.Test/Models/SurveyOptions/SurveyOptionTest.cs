using API.Models.SurveyOptions;

namespace API.Test.Models.SurveyOptions
{
    public class SurveyOptionTest
    {
        [Fact]
        public void WhenSurveyOptionIsCreatedThenIdShouldBeNull()
        {
            var option = new SurveyOption();

            Assert.NotEqual(Guid.Empty, option.Id);
        }

        [Fact]
        public void WhenSurveyOptionIsCreatedTextShouldBeNull()
        {
            var option = new SurveyOption();

            Assert.Null(option.Text);
        }

        [Fact]
        public void WhenSurveyOptionIsCreatedThenPositionShouldBeOne()
        {
            var option = new SurveyOption();

            Assert.Equal(1, option.Position);
        }

        [Fact]
        public void WhenSurveyOptionIsCreatedThenTimesSelectedShouldBeZero()
        {
            var option = new SurveyOption();

            Assert.Equal(0, option.TimesSelected);
        }
    }
}