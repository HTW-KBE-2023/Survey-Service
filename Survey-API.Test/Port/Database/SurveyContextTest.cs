using API.Port.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Test.Port.Database
{
    public class SurveyContextTest : IDisposable
    {
        private readonly SurveyContext _context;

        public SurveyContextTest()
        {
            var options = new DbContextOptionsBuilder<SurveyContext>()
                .UseInMemoryDatabase(databaseName: nameof(SurveyContextTest))
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
        public void WhenContextIsCreatedThenContainsOneSetForeachModel()
        {
            Assert.Empty(_context.Surveys);
            Assert.Empty(_context.SurveyOptions);
        }
    }
}