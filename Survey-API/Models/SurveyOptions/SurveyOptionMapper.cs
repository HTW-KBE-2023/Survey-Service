using API.Models.SurveyOptions;
using Boxed.Mapping;
using MessagingContracts.Survey;

namespace API.Models.Surveys;

public class SurveyOptionMapper : IMapper<SurveyCreated.SurveyOption, SurveyOption>, IMapper<SurveyOption, SurveyCreated.SurveyOption>
{
    public void Map(SurveyCreated.SurveyOption source, SurveyOption destination)
    {
        destination.Id = source.SurveyOptionId;
        destination.Text = source.Text;
        destination.TimesSelected = source.TimesSelected;
        destination.Position = source.Position;
    }

    public void Map(SurveyOption source, SurveyCreated.SurveyOption destination)
    {
        destination.SurveyOptionId = source.Id;
        destination.Text = source.Text;
        destination.TimesSelected = source.TimesSelected;
        destination.Position = source.Position;
    }
}