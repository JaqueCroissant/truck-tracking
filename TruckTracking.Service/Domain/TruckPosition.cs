namespace TruckTracking.Service.Domain;

public record TruckPosition(double Latitude, double Longitude, string Country, DateTimeOffset Timestamp);