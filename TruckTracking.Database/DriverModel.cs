namespace TruckTracking.Database;

public class DriverModel
{
    public int Id { get; set; }
    
    public int TruckPlanId { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; } 
    
    public DateTime DateOfBirth { get; set; }
    
    public ICollection<TruckPlanModel> TruckPlans { get; set; }
}