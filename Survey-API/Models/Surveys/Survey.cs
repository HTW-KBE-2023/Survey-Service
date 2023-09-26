using API.Models.SurveyOptions;

namespace API.Models.Surveys
{
    public class Survey : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool Completed { get; set; }

        public IList<SurveyOption> SurveyOptions { get; set; } = new List<SurveyOption>();

        public void ChangeVote(Guid optionId, int change)
        {
            var option = SurveyOptions.FirstOrDefault(selection => selection.Id == optionId);
            if (option is null)
            {
                return;
            }

            option.TimesSelected += change;
        }
    }
}