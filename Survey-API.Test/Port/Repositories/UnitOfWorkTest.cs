using API.Models;
using API.Models.Participants;
using API.Models.SurveyOptions;
using API.Models.Surveys;
using API.Port.Database;
using API.Port.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace API.Test.Port.Repositories
{
    public class UnitOfWorkTest : IDisposable
    {
        private class Dummy : IEntity
        {
            public Guid Id { get; set; }
        }

        private readonly SurveyContext _context;

        public UnitOfWorkTest()
        {
            var options = new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(databaseName: nameof(UnitOfWorkTest))
                .Options;

            _context = new SurveyContext(options);

            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void WhenUnitOfWorkIsCreatedThenContainSurveyRepository()
        {
            UnitOfWork unitOfWork = new(_context);

            Assert.NotNull(unitOfWork.SurveyRepository);
            Assert.IsAssignableFrom<IGenericRepository<Survey>>(unitOfWork.SurveyRepository);
        }

        [Fact]
        public void WhenUnitOfWorkIsCreatedThenContainSurveyOptionRepository()
        {
            UnitOfWork unitOfWork = new(_context);

            Assert.NotNull(unitOfWork.SurveyOptionRepository);
            Assert.IsAssignableFrom<IGenericRepository<SurveyOption>>(unitOfWork.SurveyOptionRepository);
        }

        [Fact]
        public void WhenUnitOfWorkIsCreatedThenContainParticipantRepository()
        {
            UnitOfWork unitOfWork = new(_context);

            Assert.NotNull(unitOfWork.ParticipantRepository);
            Assert.IsAssignableFrom<IGenericRepository<Participant>>(unitOfWork.ParticipantRepository);
        }

        [Fact]
        public void WhenGetRepositoryThatsRegistersThenSearchedRepositoryShouldBeReturned()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(_context);

            var participantRepository = unitOfWork.GetGenericRepository<Participant>();

            Assert.IsAssignableFrom<IGenericRepository<Participant>>(participantRepository);
        }

        [Fact]
        public void WhenGetRepositoryThatsNotRegistersThenErrorShouldBeThrown()
        {
            IUnitOfWork unitOfWork = new UnitOfWork(_context);

            var exception = Assert.Throws<ArgumentException>(unitOfWork.GetGenericRepository<Dummy>);

            Assert.Equal($"No Repository for the Type {nameof(Dummy)} could be found!", exception.Message);
        }

        [Fact]
        public void WhenSaveIsCalledThenSaveOfUnderlyingContextShouldCAlled()
        {
            var options = new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(databaseName: "SaveChangesTestCase")
                .Options;

            var fakeSurveyContext = new Mock<SurveyContext>(() => new SurveyContext(options));
            fakeSurveyContext.Setup(mock => mock.SaveChanges()).Verifiable(Times.Once);

            IUnitOfWork unitOfWork = new UnitOfWork(fakeSurveyContext.Object);

            unitOfWork.Save();

            fakeSurveyContext.Verify();
        }
    }
}