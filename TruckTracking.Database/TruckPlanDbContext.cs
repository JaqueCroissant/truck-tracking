using Microsoft.EntityFrameworkCore;

namespace TruckTracking.Database;

public class TruckPlanDbContext(DbContextOptions<TruckPlanDbContext> options) : DbContext(options)
{
    public DbSet<DriverModel> Drivers { get; set; }
    public DbSet<TruckPositionModel> TruckPositions { get; set; }
    public DbSet<TruckPlanModel> TruckPlans { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DriverModel>()
            .HasMany(d => d.TruckPlans)
            .WithOne(p => p.Driver)
            .HasForeignKey(p => p.DriverId);
        
        modelBuilder.Entity<TruckPlanModel>()
            .HasMany(p => p.Route)
            .WithOne(tp => tp.TruckPlan)
            .HasForeignKey(tp => tp.TruckPlanId);
    }

    public void SeedData()
    {
        if (!Database.EnsureCreated() || TruckPlans.Any())
        {
            return;
        }

        var testData = new[]
        {
            DataSet1(DateTime.Parse("2018-02-13T01:01:01")),
            DataSet2(DateTime.Parse("2018-02-23T11:51:31")),
            DataSet3(DateTime.Parse("2018-02-01T21:43:53")),
            DataSet4(DateTime.Parse("2018-02-24T16:16:12"))
        };
        
        AddRange(testData);
        SaveChanges();
    }
    
    
    
    private static TruckPlanModel DataSet1(DateTimeOffset now) =>
        new()
        {
            Driver = new DriverModel
            {
                FirstName = "Jürgen",
                LastName = "Meier",
                DateOfBirth = new DateTime(1965, 3, 15),
                TruckPlans = new List<TruckPlanModel>()
            },
            Route = new List<TruckPositionModel>
            {
                new() { Country = "DE", Latitude = 47.7280, Longitude = 12.8785, Timestamp = now },
                new() { Country = "DE", Latitude = 47.7192, Longitude = 12.8915, Timestamp = now.AddMinutes(5) },
                new() { Country = "DE", Latitude = 47.7140, Longitude = 12.9028, Timestamp = now.AddMinutes(10) },
                new() { Country = "AT", Latitude = 47.7090, Longitude = 12.9140, Timestamp = now.AddMinutes(15) },
                new() { Country = "AT", Latitude = 47.7009, Longitude = 12.9312, Timestamp = now.AddMinutes(20) },
                new() { Country = "AT", Latitude = 47.6905, Longitude = 12.9481, Timestamp = now.AddMinutes(25) },
                new() { Country = "AT", Latitude = 47.6802, Longitude = 12.9644, Timestamp = now.AddMinutes(30) },
                new() { Country = "AT", Latitude = 47.7930, Longitude = 13.0030, Timestamp = now.AddMinutes(35) },
                new() { Country = "AT", Latitude = 47.8010, Longitude = 13.0175, Timestamp = now.AddMinutes(40) },
                new() { Country = "AT", Latitude = 47.8095, Longitude = 13.0550, Timestamp = now.AddMinutes(45) }
            }
        };
    
    private static TruckPlanModel DataSet2(DateTimeOffset now) =>
        new()
        {
            Driver = new DriverModel
            {
                FirstName = "Elżbieta",
                LastName = "Kowalczyk",
                DateOfBirth = new DateTime(1972, 8, 2),
                TruckPlans = new List<TruckPlanModel>()
            },
            Route = new List<TruckPositionModel>
            {
                new() { Country = "PL", Latitude = 52.3280, Longitude = 14.6068, Timestamp = now },
                new() { Country = "PL", Latitude = 52.3320, Longitude = 14.5860, Timestamp = now.AddMinutes(5) },
                new() { Country = "PL", Latitude = 52.3360, Longitude = 14.5670, Timestamp = now.AddMinutes(10) },
                new() { Country = "DE", Latitude = 52.3400, Longitude = 14.5480, Timestamp = now.AddMinutes(15) },
                new() { Country = "DE", Latitude = 52.3450, Longitude = 14.5280, Timestamp = now.AddMinutes(20) },
                new() { Country = "DE", Latitude = 52.3500, Longitude = 14.5080, Timestamp = now.AddMinutes(25) },
                new() { Country = "DE", Latitude = 52.3550, Longitude = 14.4880, Timestamp = now.AddMinutes(30) },
                new() { Country = "DE", Latitude = 52.3600, Longitude = 14.4680, Timestamp = now.AddMinutes(35) },
                new() { Country = "DE", Latitude = 52.3650, Longitude = 14.4480, Timestamp = now.AddMinutes(40) },
                new() { Country = "DE", Latitude = 52.3700, Longitude = 14.4280, Timestamp = now.AddMinutes(45) }
            }
        };
    
    private static TruckPlanModel DataSet3(DateTimeOffset now) =>
        new()
        {
            Driver = new DriverModel
            {
                FirstName = "Luca",
                LastName = "Bianchi",
                DateOfBirth = new DateTime(1986, 3, 29),
                TruckPlans = new List<TruckPlanModel>()
            },
            Route = new List<TruckPositionModel>
            {
                new() { Country = "FR", Latitude = 47.5890, Longitude = 7.5610, Timestamp = now },               // Saint-Louis (FR)
                new() { Country = "FR", Latitude = 47.5880, Longitude = 7.5700, Timestamp = now.AddMinutes(5) },
                new() { Country = "FR", Latitude = 47.5870, Longitude = 7.5800, Timestamp = now.AddMinutes(10) },
                new() { Country = "CH", Latitude = 47.5860, Longitude = 7.5900, Timestamp = now.AddMinutes(15) }, // Border crossing
                new() { Country = "CH", Latitude = 47.5850, Longitude = 7.6000, Timestamp = now.AddMinutes(20) },
                new() { Country = "CH", Latitude = 47.5840, Longitude = 7.6100, Timestamp = now.AddMinutes(25) },
                new() { Country = "CH", Latitude = 47.5830, Longitude = 7.6200, Timestamp = now.AddMinutes(30) },
                new() { Country = "CH", Latitude = 47.5820, Longitude = 7.6300, Timestamp = now.AddMinutes(35) },
                new() { Country = "CH", Latitude = 47.5810, Longitude = 7.6400, Timestamp = now.AddMinutes(40) },
                new() { Country = "CH", Latitude = 47.5800, Longitude = 7.6500, Timestamp = now.AddMinutes(45) }  // Basel outskirts
            }
        };
    
    private static TruckPlanModel DataSet4(DateTimeOffset now) =>
        new()
        {
            Driver = new DriverModel
            {
                FirstName = "Sofie",
                LastName = "Madsen",
                DateOfBirth = new DateTime(1991, 10, 18),
                TruckPlans = new List<TruckPlanModel>()
            },
            Route = new List<TruckPositionModel>
            {
                new() { Country = "DE", Latitude = 49.1010, Longitude = 13.2120, Timestamp = now },                 // Bayerisch Eisenstein
                new() { Country = "DE", Latitude = 49.0990, Longitude = 13.2200, Timestamp = now.AddMinutes(5) },
                new() { Country = "DE", Latitude = 49.0975, Longitude = 13.2265, Timestamp = now.AddMinutes(10) },
                new() { Country = "CZ", Latitude = 49.0960, Longitude = 13.2330, Timestamp = now.AddMinutes(15) },  // Crosses into CZ
                new() { Country = "CZ", Latitude = 49.0935, Longitude = 13.2400, Timestamp = now.AddMinutes(20) },
                new() { Country = "CZ", Latitude = 49.0900, Longitude = 13.2475, Timestamp = now.AddMinutes(25) },
                new() { Country = "CZ", Latitude = 49.0870, Longitude = 13.2550, Timestamp = now.AddMinutes(30) },
                new() { Country = "CZ", Latitude = 49.0840, Longitude = 13.2625, Timestamp = now.AddMinutes(35) },
                new() { Country = "CZ", Latitude = 49.0805, Longitude = 13.2700, Timestamp = now.AddMinutes(40) },
                new() { Country = "CZ", Latitude = 49.0780, Longitude = 13.2770, Timestamp = now.AddMinutes(45) }   // Near Železná Ruda center
            }
        };
}