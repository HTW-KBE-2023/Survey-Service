using API.Models.SurveyOptions;
using API.Port.Database;
using API.Port.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Test.Port.Repositories
{
    public class GenericRepositoryTest : IDisposable
    {
        private readonly SurveyContext _context;

        private readonly SurveyOption _option1 = new()
        {
            Id = Guid.NewGuid(),
            Text = "Epic Option",
            Position = 2,
            TimesSelected = 0
        };

        private readonly SurveyOption _option2 = new()
        {
            Id = Guid.NewGuid(),
            Text = "Epic Option Numero Two",
            Position = 1,
            TimesSelected = 500
        };

        public GenericRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(databaseName: nameof(GenericRepositoryTest))
                .Options;

            _context = new SurveyContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Add(_option1);
            _context.Add(_option2);

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void WhenEntityExistsThenGetByIDShouldReturnEntity()
        {
            IGenericRepository<SurveyOption> optionRepository = new GenericRepository<SurveyOption>(_context);

            var result = optionRepository.GetByID(_option1.Id);

            Assert.Equal(_option1, result);
        }

        [Fact]
        public void WhenEntityExistsThenGetByIDShouldReturnNull()
        {
            IGenericRepository<SurveyOption> optionRepository = new GenericRepository<SurveyOption>(_context);

            var result = optionRepository.GetByID(Guid.Empty);

            Assert.Null(result);
        }

        [Fact]
        public void WhenEntityExistsThenGetShouldReturnAll()
        {
            IGenericRepository<SurveyOption> optionRepository = new GenericRepository<SurveyOption>(_context);

            var result = optionRepository.Get();

            Assert.Equal(2, result.Count());
            Assert.Contains(_option1, result);
            Assert.Contains(_option2, result);
        }

        [Fact]
        public void WhenEntityExistsThenGetWithFilterShouldReturnFilteredEntities()
        {
            IGenericRepository<SurveyOption> optionRepository = new GenericRepository<SurveyOption>(_context);

            var result = optionRepository.Get(filter: option => option.TimesSelected > 100);

            Assert.Single(result);
            Assert.Contains(_option2, result);
        }

        [Fact]
        public void WhenEntityExistsThenGetWithOrderShouldReturnOrderedEntities()
        {
            IGenericRepository<SurveyOption> optionRepository = new GenericRepository<SurveyOption>(_context);

            var result = optionRepository.Get(orderBy: option => option.OrderBy(option => option.Position));

            Assert.Equal(2, result.Count());
            Assert.Equal(_option2, result.ElementAt(0));
            Assert.Equal(_option1, result.ElementAt(1));
        }

        [Fact]
        public void WhenEntityIsNewThenInsertShouldAddEntityToContext()
        {
            IGenericRepository<SurveyOption> optionRepository = new GenericRepository<SurveyOption>(_context);

            SurveyOption _option3 = new()
            {
                Id = Guid.NewGuid(),
                Text = "Epic Option Numero Two",
                Position = 1,
                TimesSelected = 500
            };

            optionRepository.Insert(_option3);
            _context.SaveChanges();

            Assert.Equal(3, _context.SurveyOptions.Count());
            Assert.True(_context.SurveyOptions.Contains(_option3));
        }

        [Fact]
        public void WhenEntityIsModifiedThenUpdateShouldChangeEntityInContext()
        {
            IGenericRepository<SurveyOption> optionRepository = new GenericRepository<SurveyOption>(_context);

            _option2.Text = "Updated Text";

            optionRepository.Update(_option2);
            _context.SaveChanges();

            Assert.Equal("Updated Text", _context.SurveyOptions.Find(_option2.Id)?.Text);
        }
    }
}