using API.Models.Surveys;
using API.Port.Database;
using API.Port.Repositories;
using API.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services.Surveys;
using Services.Validations;

namespace API
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddDatabaseConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<SurveyContext>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("MySQL");
                options.UseMySql(connection, ServerVersion.AutoDetect(connection), option => option.EnableRetryOnFailure());
            });
            builder.Services.AddTransient<DbInitialiser>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<GenericService<Survey>>();
            builder.Services.AddScoped<IGenericService<Survey>, SurveyService>(serviceProvider =>
            {
                return new SurveyService(
                    serviceProvider.GetRequiredService<GenericService<Survey>>(),
                    serviceProvider.GetRequiredService<SurveyMapper>(),
                    serviceProvider.GetRequiredService<IBus>());
            });
        }

        public static void AddMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<SurveyMapper, SurveyMapper>();
            builder.Services.AddSingleton<ValidationFailedMapper, ValidationFailedMapper>();
        }

        public static void AddRabbitMqConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.SetInMemorySagaRepositoryProvider();

                var assembly = typeof(Program).Assembly;

                x.AddConsumers(assembly);
                x.AddSagaStateMachines(assembly);
                x.AddSagas(assembly);
                x.AddActivities(assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    var connection = builder.Configuration.GetConnectionString("RabbitMQ");
                    cfg.Host(connection, "/", h =>
                    {
                        var rabbitMQConfiguration = builder.Configuration.GetSection("RabbitMQ.Configuration");
                        var user = rabbitMQConfiguration.GetValue<string>("User");
                        var password = rabbitMQConfiguration.GetValue<string>("Password");

                        h.Username(user);
                        h.Password(password);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        public static void AddHealthChecksConfiguration(this WebApplicationBuilder builder)
        {
            var connection = builder.Configuration.GetConnectionString("RabbitMQ");
            var rabbitMQConfiguration = builder.Configuration.GetSection("RabbitMQ.Configuration");
            var user = rabbitMQConfiguration.GetValue<string>("User");
            var password = rabbitMQConfiguration.GetValue<string>("Password");

            builder.Services.AddHealthChecks()
                .AddMySql(builder.Configuration.GetConnectionString("MySQL") ?? string.Empty, tags: new List<string>() { "Database" }, timeout: TimeSpan.FromSeconds(5))
                .AddRabbitMQ(rabbitConnectionString: $"amqp://{user}:{password}@{connection}");
        }
    }
}