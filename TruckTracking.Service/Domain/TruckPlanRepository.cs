using Microsoft.EntityFrameworkCore;
using TruckTracking.Database;
using TruckTracking.Service.Extensions;

namespace TruckTracking.Service.Domain;

public interface ITruckPlanRepository
{
    public Task<IEnumerable<TruckPlan>> GetTruckPlans(
        int? minimumDriverAge = null, 
        string? countryCode = null,
        DateTimeOffset? from = null, 
        DateTimeOffset? to = null);
}

public class TruckPlanRepository : ITruckPlanRepository
{
    private readonly TruckPlanDbContext _dbContext;
    
    public TruckPlanRepository(TruckPlanDbContext dbContext)
    {
        dbContext.SeedData();
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<TruckPlan>> GetTruckPlans(
        int? minimumDriverAge = null, 
        string? countryCode = null, 
        DateTimeOffset? from = null, 
        DateTimeOffset? to = null)
    {
        var query = _dbContext.TruckPlans
            .Include(x => x.Driver)
            .Include(x => x.Route)
            .AsQueryable();

        if (minimumDriverAge.HasValue)
        {
            var latestBirthdate = DateTime.UtcNow.Subtract(TimeSpan.FromDays(365.25 * minimumDriverAge.Value));

            query = query.Where(x => 
                x.Driver.DateOfBirth <= latestBirthdate);
        }

        if (!string.IsNullOrWhiteSpace(countryCode))
        {
            query = query.Where(x => 
                x.Route.Any(r => 
                    r.Country.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase)));
        }

        if (from.HasValue && to.HasValue)
        {
            query = query.Where(x => 
                x.Route.Any(r => 
                    r.Timestamp >= from && 
                    r.Timestamp <= to));
        }

        var result = await query.ToListAsync();

        return result.Select(x => x.ToTruckPlan());
    }
}