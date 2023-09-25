using API.Models.Surveys;
using API.Models.Surveys.Requests;
using API.Models.Surveys.Responses;
using API.Models.Validations;
using API.Models.Validations.Responses;
using API.Services;
using Boxed.Mapping;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[Route("surveys")]
[ApiController]
public class SurveyController : ControllerBase
{
    private readonly ILogger<SurveyController> _logger;
    private readonly IGenericService<Survey> _surveyService;
    private readonly IMapper<Survey, SurveyResponse> _surveyToSurveyResponseMapper;
    private readonly IMapper<CreateSurveyRequest, Survey> _createSurveyRequestToSurveyMapper;
    private readonly IMapper<UpdateSurveyRequest, Survey> _updateSurveyRequestToSurveyMapper;
    private readonly IMapper<ValidationFailed, ValidationFailureResponse> _validationMapper;

    public SurveyController(ILogger<SurveyController> logger,
                             IGenericService<Survey> surveyService,
                             SurveyMapper surveyMapper,
                             ValidationFailedMapper validationMapper)
    {
        _logger = logger;
        _surveyService = surveyService;
        _surveyToSurveyResponseMapper = surveyMapper;
        _createSurveyRequestToSurveyMapper = surveyMapper;
        _validationMapper = validationMapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SurveyResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> Create([FromBody] CreateSurveyRequest request)
    {
        var survey = _createSurveyRequestToSurveyMapper.Map(request);

        var result = _surveyService.Create(survey);
        var response = result.Match<IActionResult>(
            _ => CreatedAtAction(nameof(Get), new { id = survey.Id }, _surveyToSurveyResponseMapper.Map(survey)),
            failed => BadRequest(_validationMapper.Map(failed)));

        return Task.FromResult(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SurveyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSurveyRequest request)
    {
        var survey = _updateSurveyRequestToSurveyMapper.Map(request);
        survey.Id = id;

        var result = _surveyService.Update(survey);

        var response = result.Match<IActionResult>(
            monster => monster is not null ? Ok(_surveyToSurveyResponseMapper.Map(monster)) : NotFound(),
            failed => BadRequest(_validationMapper.Map(failed)));

        return Task.FromResult(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SurveyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Get([FromRoute] Guid id)
    {
        var survey = _surveyService.GetById(id);
        if (survey is not null)
        {
            var response = _surveyToSurveyResponseMapper.Map(survey);
            return Task.FromResult<IActionResult>(Ok(response));
        }

        return Task.FromResult<IActionResult>(NotFound());
    }

    [HttpGet]
    [ProducesResponseType(typeof(SurveysResponse), StatusCodes.Status200OK)]
    public Task<IActionResult> GetAll()
    {
        var surveys = _surveyService.GetAll();
        var surveysResponse = new SurveysResponse()
        {
            Items = _surveyToSurveyResponseMapper.MapList(surveys)
        };

        return Task.FromResult<IActionResult>(Ok(surveysResponse));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var surveys = _surveyService.GetById(id);
        if (surveys is not null)
        {
            _surveyService.DeleteById(id);
            return Task.FromResult<IActionResult>(Ok());
        }
        return Task.FromResult<IActionResult>(NotFound());
    }
}