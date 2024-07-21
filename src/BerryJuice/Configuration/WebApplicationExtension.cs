using BerryJuice.Blazor.Components;

namespace BerryJuice.Configuration;

public static class WebApplicationExtension
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        var isBlazorEnabled = app.Configuration["BERRYJUICE_USE_BLAZOR"] == "true";

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

        return app;
    }
}
