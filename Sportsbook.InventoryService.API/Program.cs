using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the ServiceCollection
builder.ConfigureServices();

var app = builder.Build();

// Configure app in the Startup
app.Configure();

app.Run();
