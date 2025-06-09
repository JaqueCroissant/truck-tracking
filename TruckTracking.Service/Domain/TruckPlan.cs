namespace TruckTracking.Service.Domain;

public record TruckPlan(int Id, Driver Driver, IReadOnlyCollection<TruckPosition> Route);