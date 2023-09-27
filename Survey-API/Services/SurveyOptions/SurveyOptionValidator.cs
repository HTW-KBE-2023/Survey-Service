using API.Models.SurveyOptions;
using FluentValidation;

namespace Services.Surveys;

public class SurveyOptionValidator : AbstractValidator<SurveyOption>
{
    public SurveyOptionValidator()
    {
        RuleFor(survey => survey.Id).NotEmpty();
    }
}