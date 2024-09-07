using Accounts.Infrastructure.Persistence;
using BerryJuice.Blazor.Components;
using BerryJuice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using _Imports = BerryJuice.Blazor.Client._Imports;

namespace BerryJuice.Configuration;

public static class WebApplicationExtension
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        var isBlazorEnabled = app.Configuration[key: "BERRYJUICE_USE_BLAZOR"] == "true";

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
            {
                app.UseWebAssemblyDebugging();
            } else
            {
                app.UseExceptionHandler(errorHandlingPath: "/Error", createScopeForErrors: true);
            }

            app.UseAntiforgery();
            app
               .MapRazorComponents<App>()
               .AddInteractiveWebAssemblyRenderMode()
               .AddInteractiveServerRenderMode()
               .AddAdditionalAssemblies(typeof(_Imports).Assembly);
        }

        // apply migrations
        using var scope = app.Services.CreateScope();
        var accountsContext = scope.ServiceProvider.GetRequiredService<AccountsContext>();
        accountsContext.Database.Migrate();

        var berryJuiceContext = scope.ServiceProvider.GetRequiredService<BerryJuiceDbContext>();
        berryJuiceContext.Database.Migrate();

        return app;
    }
}
