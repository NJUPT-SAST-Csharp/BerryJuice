using BerryJuice.Configuration;

var builder = WebApplication.CreateBuilder(args).ConfigureConfiguration(args).ConfigureServices();

var app = builder.Build();

app.ConfigureApplication().Run();

// Just a test to examine whether it works
