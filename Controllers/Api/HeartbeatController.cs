using ClientMonitoring.Web.Data;
using ClientMonitoring.Web.Hubs;
using ClientMonitoring.Web.Models;
using ClientMonitoring.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ClientMonitoring.Web.Controllers.Api;

[ApiController]
[Route("api")]
public class HeartbeatController : ControllerBase
{
    readonly ApplicationDbContext1 _db;
    readonly IHubContext<MonitoringHub> _hub;
    public HeartbeatController(ApplicationDbContext1 d, IHubContext<MonitoringHub> h)
    {
        _db = d;
        _hub = h;
    }

    //[HttpGet("clients")]
    //[Authorize]
    //public async Task<IActionResult> Clients() => Ok(await new ClientMonitoringService(_db).GetAsync());

    [HttpGet("clients")]
    [Authorize]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _db.Clients
            .AsNoTracking()
            .Where(x => x.IsEnabled)
            .Select(x => new
            {
                x.Id,
                x.ClientCode,
                x.LastHeartbeat
            })
            .OrderBy(x => x.ClientCode)
            .ToListAsync();

        return Ok(clients);
    }

    [HttpPost("heartbeat")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Heartbeat(
     [FromBody] HeartbeatRequest r)
    {
        System.Diagnostics.Debug.WriteLine(
            ">>> HEARTBEAT ACTION CALLED");

        System.Diagnostics.Debug.WriteLine(
            $"ClientCode: {r?.ClientCode}");

        if (r == null ||
            string.IsNullOrWhiteSpace(r.ClientCode))
        {
            return BadRequest("ClientCode is required.");
        }

        var code =
            User.FindFirst("client_code")?.Value;

        System.Diagnostics.Debug.WriteLine(
            $"JWT client_code: {code}");

        if (!string.Equals(
            code,
            r.ClientCode,
            StringComparison.OrdinalIgnoreCase))
        {
            return Forbid();
        }

        var commune = await _db.Clients
            .FirstOrDefaultAsync(x =>
                x.ClientCode == code &&
                x.IsEnabled);

        if (commune == null)
            return Unauthorized();

        commune.LastHeartbeat =
            DateTime.UtcNow;

        await _db.SaveChangesAsync();

        System.Diagnostics.Debug.WriteLine(
            ">>> HEARTBEAT SAVED");

        return Ok();
    }


    [HttpGet("commune-clients")]
    [Authorize]
    public async Task<IActionResult> CommuneClients()
    {
        var clients = await _db.CommuneClientStatuses
            .AsNoTracking()
            .OrderBy(x => x.CommuneCode)
            .ThenBy(x => x.ClientCode)
            .ToListAsync();

        return Ok(clients);
    }
}
