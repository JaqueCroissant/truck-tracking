using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace TruckTracking.Database;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<TruckPlanDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
    }
}