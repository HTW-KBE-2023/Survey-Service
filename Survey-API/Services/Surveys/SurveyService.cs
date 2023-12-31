﻿using API.Models.Surveys;
using API.Models.Validations;
using API.Services;
using Boxed.Mapping;
using MassTransit;
using MessagingContracts.Survey;

namespace Services.Surveys
{
    public class SurveyService : IGenericService<Survey>
    {
        private IGenericService<Survey> _genericService;
        private IMapper<Survey, SurveyCreated> _surveyToSurveyStartedMapper;
        private IMapper<Survey, SurveyUpdated> _surveyToSurveyUpdatedMapper;
        private IBus _bus;

        public SurveyService(IGenericService<Survey> genericService, SurveyMapper surveyMapper, IBus bus)
        {
            _genericService = genericService;
            _surveyToSurveyStartedMapper = surveyMapper;
            _surveyToSurveyUpdatedMapper = surveyMapper;
            _bus = bus;
        }

        public Result<Survey, ValidationFailed> Create(Survey entity)
        {
            var message = _surveyToSurveyStartedMapper.Map(entity);
            _bus.Publish(message);
            return _genericService.Create(entity);
        }

        public void DeleteById(object id)
        {
            _genericService.DeleteById(id);
        }

        public IEnumerable<Survey> GetAll()
        {
            return _genericService.GetAll();
        }

        public Survey? GetById(object id)
        {
            return _genericService.GetById(id);
        }

        public Result<Survey?, ValidationFailed> Update(Survey entity)
        {
            var message = _surveyToSurveyUpdatedMapper.Map(entity);
            _bus.Publish(message);
            return _genericService.Update(entity);
        }
    }
}