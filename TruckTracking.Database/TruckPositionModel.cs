namespace TruckTracking.Database;

public class TruckPositionModel
{
    public int Id { get; set; }
    public int TruckPlanId { get; set; }
    
    public string Country { get; set; }
    
    public required double Latitude { get; set; } 
    
    public required double Longitude { get; set; }
    
    public required DateTimeOffset Timestamp { get; set; }
    
    public TruckPlanModel TruckPlan { get; set; }
}