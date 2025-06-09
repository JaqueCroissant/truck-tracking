namespace TruckTracking;

public class GetKilometersDrivenRequest
{
    public int? DriverAge { get; set; }
    public string? CountryCode { get; set; }
    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }
}