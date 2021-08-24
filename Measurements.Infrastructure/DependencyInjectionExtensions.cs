using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Measurements.Infrastructure.Contexts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<WeatherInformation>(options => options.UseSqlServer(configuration.GetConnectionString("WeatherInformation")));

            services.AddScoped<DbContext, WeatherInformation>();

            return services;
        }
    }
}
