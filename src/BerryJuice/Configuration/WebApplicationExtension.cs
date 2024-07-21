using BerryJuice.Blazor.Components;

namespace BerryJuice.Configuration;

public static class WebApplicationExtension
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        var isBlazorEnabled = app.Configuration["BERRYJUICE_USE_BLAZOR"] == "true";

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();

        if (isBlazorEnabled)
        {
            if (app.Environment.IsDevelopment())
                app.UseWebAssemblyDebugging();
            else
                app.UseExceptionHandler("/Error", createScopeForErrors: true);

            app.UseAntiforgery();
            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddInteractiveServerRenderMode()
                .AddAdditionalAssemblies(typeof(BerryJuice.Blazor.Client._Imports).Assembly);
        }

        return app;
    }
}
