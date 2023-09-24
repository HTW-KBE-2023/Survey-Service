using API.Models.Participants;
using API.Models.SurveyOptions;
using API.Models.Surveys;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>()
                        .Navigation(survey => survey.SurveyOptions)
                        .AutoInclude();

            modelBuilder.Entity<Survey>()
                        .Navigation(survey => survey.Participants)
                        .AutoInclude();
        }
    }
}