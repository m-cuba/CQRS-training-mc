using Serilog;
using Sportsbook.ServiceTemplate.Application.Extensions;
using Sportsbook.ServiceTemplate.Infrastructure.Extensions;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(loggingBuilder =>
          loggingBuilder.AddSerilog(dispose: true));

        builder.Services.AddInfraServices(builder.Configuration);

        builder.Services.AddApiVersioning(options =>
        {
            //indicating whether a default version is assumed when a client does
            // does not provide an API version.
            options.AssumeDefaultVersionWhenUnspecified = true;
        }).AddApiExplorer(options =>
        {
            // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });
        builder.Services.AddControllers();


        // Add Application layer services:
        builder.Services.AddApplicationServices();
        builder.Services.AddValidationServices();

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public static void Configure(this WebApplication app)
    {
        app.Services.ApplyMigrations();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.UseHttpsRedirection();
    }
}
