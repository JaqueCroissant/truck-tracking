namespace TruckTracking.Dtos;

public record TruckPlanDto(int Id, DriverDto Driver, IReadOnlyCollection<TruckPositionDto> Route);