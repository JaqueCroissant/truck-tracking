using Microsoft.AspNetCore.Mvc;
using TruckTracking.Dtos;
using TruckTracking.Extensions;
using TruckTracking.Service.Commands;

namespace TruckTracking.Controllers;

[ApiController]
[Route("truck-plan")]
public class TruckPlanController(
    IGetTruckPlansCommand getTruckPlansCommand,
    IGetTruckPlansKilometersDrivenCommand getTruckPlansKilometersDrivenCommand) 
    : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<TruckPlanDto>> GetTruckPlans([FromQuery] GetTruckPlansRequest request)
    {
        var truckPlans = await getTruckPlansCommand.ExecuteAsync(request.DriverAge, request.CountryCode);

        return truckPlans.ToDtos();
    }

    [HttpGet("distances-driven")]
    public async Task<DistanceDto> GetTruckPlanDistance([FromQuery] GetKilometersDrivenRequest request)
    {
        var truckPlans = await getTruckPlansCommand.ExecuteAsync(
            request.DriverAge, 
            request.CountryCode, 
            request.FromDate, 
            request.ToDate);
        
        var distancesDriven = getTruckPlansKilometersDrivenCommand.Execute(truckPlans.ToArray());

        return new DistanceDto(distancesDriven, "kilometers");
    }
}