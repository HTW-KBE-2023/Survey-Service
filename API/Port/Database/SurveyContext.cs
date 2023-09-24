using API.Models.Participants;
using API.Models.SurveyOptions;
using API.Models.Surveys;
using Microsoft.EntityFrameworkCore;

namespace API.Port.Database
{
    public class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}