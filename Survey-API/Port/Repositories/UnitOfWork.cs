﻿using API.Models;
using API.Models.SurveyOptions;
using API.Models.Surveys;
using API.Port.Database;

namespace API.Port.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SurveyContext _context;

        private readonly ICollection<object> _genericRepositories = new List<object>();

        private IGenericRepository<Survey>? _surveyRepository;
        private IGenericRepository<SurveyOption> _surveyOptionRepository;

        public UnitOfWork(SurveyContext context)
        {
            _context = context;

            _genericRepositories.Add(new GenericRepository<Survey>(_context));
            _genericRepositories.Add(new GenericRepository<SurveyOption>(_context));
        }

        public IGenericRepository<Survey> SurveyRepository
        {
            get
            {
                _surveyRepository ??= GetGenericRepository<Survey>();
                return _surveyRepository;
            }
        }

        public IGenericRepository<SurveyOption> SurveyOptionRepository
        {
            get
            {
                _surveyOptionRepository ??= GetGenericRepository<SurveyOption>();
                return _surveyOptionRepository;
            }
        }

        public IGenericRepository<TEntity> GetGenericRepository<TEntity>() where TEntity : IEntity
        {
            var genericRepository = _genericRepositories.OfType<IGenericRepository<TEntity>>().SingleOrDefault()
                ?? throw new ArgumentException($"No Repository for the Type {typeof(TEntity).Name} could be found!");

            return genericRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}