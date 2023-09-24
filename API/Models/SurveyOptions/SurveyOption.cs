namespace API.Models.SurveyOptions
{
    public class SurveyOption : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Text { get; set; } = string.Empty;
        public int Position { get; set; } = 1;
        public int TimesSelected { get; set; } = 0;
    }
}