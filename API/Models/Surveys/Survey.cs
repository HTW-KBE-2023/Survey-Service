using API.Models.Participants;
using API.Models.SurveyOptions;

namespace API.Models.Surveys
{
    public class Survey : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool Completed { get; set; }

        public IList<Participant> Participants { get; set; } = new List<Participant>();
        public IList<SurveyOption> SurveyOptions { get; set; } = new List<SurveyOption>();

        public void AddVote(Participant participant, SurveyOption option)
        {
            if (participant is null || Participants.Contains(participant))
            {
                return;
            }

            var selectedOption = SurveyOptions.FirstOrDefault(selection => selection.Id == option.Id);
            if (selectedOption is null)
            {
                return;
            }

            Participants.Add(participant);
            selectedOption.TimesSelected++;
        }
    }
}