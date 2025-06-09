using TruckTracking.Service.Domain;

namespace TruckTracking.Service.Commands;

public interface IGetTruckPlansCommand
{
    public Task<IEnumerable<TruckPlan>> ExecuteAsync(
        int? minimumDriverAge = null, 
        string? countryCode = null, 
        DateOnly? fromDate = null, 
        DateOnly? toDate = null);
}

public class GetTruckPlansCommand(ITruckPlanRepository repository) : IGetTruckPlansCommand
{
    public async Task<IEnumerable<TruckPlan>> ExecuteAsync(
        int? minimumDriverAge = null, 
        string? countryCode = null, 
        DateOnly? fromDate = null, 
        DateOnly? toDate = null)
    {
        DateTimeOffset? from = fromDate.HasValue ? new DateTimeOffset(fromDate.Value, TimeOnly.MinValue, TimeSpan.Zero) : null;
        DateTimeOffset? to = toDate.HasValue ? new DateTimeOffset(toDate.Value, TimeOnly.MaxValue, TimeSpan.Zero) : null;
        
        var truckPlans = await repository.GetTruckPlans(minimumDriverAge, countryCode, from, to);

        var filterByCountry = !string.IsNullOrWhiteSpace(countryCode);
        var filterByDate = from.HasValue && to.HasValue;

        if (!filterByCountry && !filterByDate)
        {
            return truckPlans;
        }
        
        var filteredTruckPlans = new List<TruckPlan>();
        
        foreach (var t in truckPlans)
        {
            var filteredRoute = t.Route.ToArray();

            if (filterByCountry)
            {
                filteredRoute = filteredRoute.Where(x => x.Country
                    .Equals(countryCode, StringComparison.InvariantCultureIgnoreCase)).ToArray();
            }
            
            if (filterByDate)
            {
                filteredRoute = filteredRoute.Where(x => x.Timestamp >= from && x.Timestamp <= to).ToArray();
            }
            
            filteredTruckPlans.Add(t with { Route = filteredRoute });
        }
        
        return filteredTruckPlans;
    }
}