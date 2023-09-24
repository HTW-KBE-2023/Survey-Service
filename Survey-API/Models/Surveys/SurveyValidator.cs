using FluentValidation;

namespace API.Models.Surveys;

public class SurveyValidator : AbstractValidator<Survey>
{
    public SurveyValidator()
    {
        RuleFor(survey => survey.Id).NotEmpty();
        RuleFor(survey => survey.Title).NotEmpty();
        RuleFor(survey => survey.Description).NotNull();
        RuleFor(survey => survey.SurveyOptions).Must(surveyOptions => surveyOptions.Count >= 2 && surveyOptions.Count <= 8);
        RuleFor(survey => survey.Participants).NotNull();
    }
}