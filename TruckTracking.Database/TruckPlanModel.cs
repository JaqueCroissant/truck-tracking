namespace TruckTracking.Database;

public class TruckPlanModel
{
    public int Id { get; set; }
    
    public int DriverId { get; set; }
    public required DriverModel Driver { get; set; }
    
    public required ICollection<TruckPositionModel> Route { get; set; }
}