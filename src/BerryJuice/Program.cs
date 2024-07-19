using BerryJuice.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (Environment.GetEnvironmentVariable("USE_BLAZOR") == "true")
    builder
        .Services.AddRazorComponents()
        .AddInteractiveServerComponents()
        .AddInteractiveWebAssemblyComponents();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    if (Environment.GetEnvironmentVariable("USE_BLAZOR") == "true")
        app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    if (Environment.GetEnvironmentVariable("USE_BLAZOR") == "true")
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAntiforgery();

if (Environment.GetEnvironmentVariable("USE_BLAZOR") == "true")
{
    app.MapControllers();
    app.MapRazorComponents<App>()
        .AddInteractiveWebAssemblyRenderMode()
        .AddInteractiveServerRenderMode()
        .AddAdditionalAssemblies(typeof(BerryJuice.Blazor.Client._Imports).Assembly);
}

app.Run();
