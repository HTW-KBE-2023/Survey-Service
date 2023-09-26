using API.Models.SurveyOptions;
using API.Models.Surveys;

namespace API.Port.Database
{
    public class DbInitialiser
    {
        private readonly SurveyContext _context;

        public DbInitialiser(SurveyContext context)
        {
            _context = context;
        }

        public void Run()
        {
            RecreateDatabase();
            AddTestData();
        }

        private void AddTestData()
        {
            var pollOption1 = new SurveyOption()
            {
                Id = Guid.NewGuid(),
                Position = 1,
                Text = "Yes",
                TimesSelected = 0
            };
            _context.Add(pollOption1);

            var pollOption2 = new SurveyOption()
            {
                Id = Guid.NewGuid(),
                Position = 2,
                Text = "No",
                TimesSelected = 0
            };
            _context.Add(pollOption2);

            _context.Add(new Survey()
            {
                Id = Guid.NewGuid(),
                Title = "Nice Survey",
                Description = "Do you like this Test Survey?",
                Completed = false,
                SurveyOptions = new List<SurveyOption>() { pollOption1, pollOption2 }
            });

            _context.SaveChanges();
        }

        private void RecreateDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}