using BerryJuice.Blazor.Components;
using BerryJuice.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureConfiguration(args);

builder.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    if (Environment.GetEnvironmentVariable("BERRYJUICE_USE_BLAZOR") == "true")
        app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    if (Environment.GetEnvironmentVariable("BERRYJUICE_USE_BLAZOR") == "true")
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

if (Environment.GetEnvironmentVariable("BERRYJUICE_USE_BLAZOR") == "true")
    app.UseAntiforgery();

app.MapControllers();

if (Environment.GetEnvironmentVariable("BERRYJUICE_USE_BLAZOR") == "true")
{
    app.MapRazorComponents<App>()
        .AddInteractiveWebAssemblyRenderMode()
        .AddInteractiveServerRenderMode()
        .AddAdditionalAssemblies(typeof(BerryJuice.Blazor.Client._Imports).Assembly);
}

app.Run();
