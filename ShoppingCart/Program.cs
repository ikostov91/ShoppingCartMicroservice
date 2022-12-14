using Polly;
using ShoppingCartNamespace.Contracts;

namespace ShoppingCartNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            ConfigureServices(services);

            var app = builder.Build();
            Configure(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Scan(selector =>
                selector
                    .FromAssemblyOf<Program>()
                    .AddClasses()
                    .AsMatchingInterface());

            services.AddHttpClient<IProductCatalogueClient, ProductCatalogueClient>()
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt))));
        }

        private static void Configure(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}