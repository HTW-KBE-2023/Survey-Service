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
            _context.Database.EnsureCreated();
        }
    }
}