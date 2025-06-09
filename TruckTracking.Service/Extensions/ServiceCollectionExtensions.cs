using Microsoft.Extensions.DependencyInjection;
using TruckTracking.Database;
using TruckTracking.Service.Commands;
using TruckTracking.Service.Domain;

namespace TruckTracking.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServiceDependencies(this IServiceCollection services)
    {
        services.ConfigureDatabase();
        services.AddTransient<ITruckPlanRepository, TruckPlanRepository>();
        services.AddTransient<IGetTruckPlansCommand, GetTruckPlansCommand>();
        services.AddTransient<IGetTruckPlansKilometersDrivenCommand, GetTruckPlansKilometersDrivenCommand>();
    }
}