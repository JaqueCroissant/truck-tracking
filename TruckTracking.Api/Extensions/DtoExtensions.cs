using TruckTracking.Dtos;
using TruckTracking.Service.Domain;

namespace TruckTracking.Extensions;

public static class DtoExtensions
{
    public static IEnumerable<TruckPlanDto> ToDtos(this IEnumerable<TruckPlan> truckPlans) => truckPlans.Select(ToDto);
    
    private static TruckPlanDto ToDto(this TruckPlan t) => new(t.Id, t.Driver.ToDto(), t.Route.Select(ToDto).ToArray());

    private static DriverDto ToDto(this Driver d) => new(d.FirstName, d.LastName, d.DateOfBirth);
    
    private static TruckPositionDto ToDto(this TruckPosition t) => new(t.Latitude, t.Longitude, t.Country, t.Timestamp);
}