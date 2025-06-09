using TruckTracking.Database;
using TruckTracking.Service.Domain;

namespace TruckTracking.Service.Extensions;

public static class DomainExtensions
{
    private static Driver ToDriver(this DriverModel d) =>
        new (d.FirstName, d.LastName, d.DateOfBirth);
    
    private static TruckPosition ToTruckPosition(this TruckPositionModel t) =>
        new (t.Latitude, t.Longitude, t.Country, t.Timestamp);
    
    public static TruckPlan ToTruckPlan(this TruckPlanModel t) =>
        new (t.Id, t.Driver.ToDriver(), t.Route.Select(ToTruckPosition).ToArray());
}