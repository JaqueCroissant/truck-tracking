using TruckTracking.Service.Domain;

namespace TruckTracking.Service.Commands;

public interface IGetTruckPlansKilometersDrivenCommand
{
    public decimal Execute(IReadOnlyCollection<TruckPlan> truckPlans);
}

public class GetTruckPlansKilometersDrivenCommand : IGetTruckPlansKilometersDrivenCommand
{
    private const double EarthRadiusInKilometers = 6371.0;
    
    public decimal Execute(IReadOnlyCollection<TruckPlan> truckPlans)
    {
        var summedDistances = truckPlans
            .Where(t => t.Route.Count >= 2)
            .Sum(t => TotalDistance(t.Route));

        return Math.Round(Convert.ToDecimal(summedDistances), 2);
    }
    
    private static double TotalDistance(IReadOnlyCollection<TruckPosition> positions)
    {
        var positionsArray = positions.ToArray();
        
        var total = 0.0;

        for (var i = 1; i < positionsArray.Length; i++)
        {
            var a = positionsArray[i - 1];
            var b = positionsArray[i];

            total += HaversineDistance(a.Latitude, a.Longitude, b.Latitude, b.Longitude);
        }

        return total;
    }
    
    private static double HaversineDistance(
        double latitude1, 
        double longitude1, 
        double latitude2, 
        double longitude2)
    {
        var latitudeAsRadians = ToRadians(latitude2 - latitude1);
        var longitudeAsRadians = ToRadians(longitude2 - longitude1);

        latitude1 = ToRadians(latitude1);
        latitude2 = ToRadians(latitude2);

        var a = Math.Sin(latitudeAsRadians / 2) * Math.Sin(latitudeAsRadians / 2) +
                   Math.Cos(latitude1) * Math.Cos(latitude2) *
                   Math.Sin(longitudeAsRadians / 2) * Math.Sin(longitudeAsRadians / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distanceKm = EarthRadiusInKilometers * c;
        
        return distanceKm;
    }
    
    private static double ToRadians(double angle) => angle * Math.PI / 180.0;
}