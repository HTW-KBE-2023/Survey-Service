using API.Models.Participants;
using API.Models.SurveyOptions;

namespace API.Test.Models.Participants
{
    public class ParticipantTest
    {
        [Fact]
        public void WhenSurveyOptionIsCreatedThenIdShouldBeNull()
        {
            var participant = new Participant();

            Assert.NotEqual(Guid.Empty, participant.Id);
        }
    }
}