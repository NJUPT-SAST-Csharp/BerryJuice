using BerryJuice.Blazor.Components;
using BerryJuice.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureConfiguration(args);

builder.ConfigureServices();

var isBlazorEnabled = builder.Configuration["BERRYJUICE_USE_BLAZOR"] == "true";

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    if (isBlazorEnabled)
        app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    if (isBlazorEnabled)
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

if (isBlazorEnabled)
    app.UseAntiforgery();

app.MapControllers();

if (isBlazorEnabled)
{
    app.MapRazorComponents<App>()
        .AddInteractiveWebAssemblyRenderMode()
        .AddInteractiveServerRenderMode()
        .AddAdditionalAssemblies(typeof(BerryJuice.Blazor.Client._Imports).Assembly);
}

app.Run();
