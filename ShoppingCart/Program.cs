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
                    .FromExecutingAssembly()
                    .AddClasses()
                    .AsImplementedInterfaces());
        }

        private static void Configure(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}