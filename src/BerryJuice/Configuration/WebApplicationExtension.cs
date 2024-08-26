using BerryJuice.Blazor.Components;
using _Imports = BerryJuice.Blazor.Client._Imports;

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
                app.UseExceptionHandler("/Error", true);

            app.UseAntiforgery();
            app.MapRazorComponents<App>()
               .AddInteractiveWebAssemblyRenderMode()
               .AddInteractiveServerRenderMode()
               .AddAdditionalAssemblies(typeof(_Imports).Assembly);
        }

        return app;
    }
}