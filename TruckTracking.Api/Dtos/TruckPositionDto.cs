namespace TruckTracking.Dtos;

public record TruckPositionDto(double Latitude, double Longitude, string Country, DateTimeOffset Timestamp);