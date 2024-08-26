using BerryJuice.Configuration;

WebApplication
   .CreateBuilder(args)
   .ConfigureConfiguration(args)
   .ConfigureServices()
   .Build()
   .ConfigureApplication()
   .Run();