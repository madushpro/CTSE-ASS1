using Microsoft.AspNetCore.Mvc;
using VehicleInventory.Application.Vehical.Command.UtilizedVehicleCommand;
using VehicleInventory.Application.Vehical.Query.GetAllVehicles;
using VehicleInventory.Application.Vehical.Query.GetAvailableVehicles;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly GetAllVehiclesHandler _getAllHandler;

    public VehicleController(GetAllVehiclesHandler getAll)
    {
        _getAllHandler = getAll;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllVehiclesQuery();
        var result = await _getAllHandler.Handle(query);
        return Ok(result);
    }

    [HttpPost("utilized")]
    public async Task<IActionResult> UtilizeVehicle(
    [FromBody] UtilizeVehicleCommand command,
    [FromServices] UtilizeVehicleHandler handler)
    {
        var result = await handler.Handle(command);
        return Ok(result);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable(
    [FromQuery] DateOnly fromDate,
    [FromQuery] DateOnly toDate,
    [FromServices] GetAvailableVehiclesHandler handler)
    {
        var query = new GetAvailableVehiclesQuery(fromDate, toDate);
        var result = await handler.Handle(query);

        return Ok(result);
    }

}