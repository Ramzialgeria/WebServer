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

    //[HttpPost("heartbeat")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //public async Task<IActionResult> Heartbeat(HeartbeatRequest r)
    //{
    //    var code = User.FindFirst("client_code")?.Value;

    //    if (!string.Equals(
    //        code,
    //        r.ClientCode,
    //        StringComparison.OrdinalIgnoreCase))
    //        return Forbid();

    //    var c = await _db.Clients
    //        .FirstOrDefaultAsync(x =>
    //            x.ClientCode == code &&
    //            x.IsEnabled);

    //    if (c == null)
    //        return Unauthorized();


    //    // --------------------------------------------------
    //    // CommuneServer
    //    // --------------------------------------------------

    //    c.MachineName = r.MachineName;
    //    c.ApplicationVersion = r.ApplicationVersion;
    //    c.LastHeartbeat = DateTime.UtcNow;
    //    c.ApiConnected = r.ApiConnected;

    //    c.LocalNetworkConnected =
    //        r.LocalNetwork?.Connected ?? false;

    //    c.LocalInterface =
    //        r.LocalNetwork?.InterfaceName;

    //    c.LocalIp =
    //        r.LocalNetwork?.IpAddress;

    //    c.LocalGateway =
    //        r.LocalNetwork?.Gateway;

    //    c.ServerNetworkConnected =
    //        r.ServerNetwork?.Connected ?? false;

    //    c.ServerInterface =
    //        r.ServerNetwork?.InterfaceName;

    //    c.ServerIp =
    //        r.ServerNetwork?.IpAddress;

    //    c.ServerGateway =
    //        r.ServerNetwork?.Gateway;


    //    // --------------------------------------------------
    //    // CommuneClients
    //    // --------------------------------------------------

    //    var clientCodes = r.Clients
    //.Where(x => !string.IsNullOrWhiteSpace(x.ClientCode))
    //.Select(x => x.ClientCode)
    //.ToList();

    //    var existingClients = await _db.CommuneClientStatuses
    //        .Where(x =>
    //            x.CommuneCode == code &&
    //            clientCodes.Contains(x.ClientCode))
    //        .ToListAsync();

    //    var clientsByCode = existingClients
    //.ToDictionary(x => x.ClientCode);



    //    var updatedClients =
    //        new List<CommuneClientStatus>();

    //    foreach (var clientRequest in r.Clients)
    //    {
    //        if (string.IsNullOrWhiteSpace(clientRequest.ClientCode))
    //            continue;

    //        if (!clientsByCode.TryGetValue(
    //            clientRequest.ClientCode,
    //            out var client))
    //        {
    //            client = new CommuneClientStatus
    //            {
    //                ClientCode = clientRequest.ClientCode,
    //                CommuneCode = code
    //            };

    //            _db.CommuneClientStatuses.Add(client);

    //            clientsByCode.Add(
    //                clientRequest.ClientCode,
    //                client);
    //        }

    //        client.CommuneCode = code;

    //        client.MachineName =
    //            clientRequest.MachineName;

    //        client.ApplicationVersion =
    //            clientRequest.ApplicationVersion;

    //        client.LastHeartbeat =
    //            clientRequest.LastHeartbeat.ToUniversalTime();

    //        updatedClients.Add(client);
    //    }

    //    //foreach (var clientRequest in r.Clients)
    //    //{
    //    //    if (string.IsNullOrWhiteSpace(
    //    //        clientRequest.ClientCode))
    //    //        continue;

    //    //    var client =
    //    //        await _db.CommuneClientStatuses
    //    //            .FirstOrDefaultAsync(x =>
    //    //                x.ClientCode ==
    //    //                clientRequest.ClientCode);

    //    //    if (client == null)
    //    //    {
    //    //        client = new CommuneClientStatus
    //    //        {
    //    //            ClientCode =
    //    //                clientRequest.ClientCode,

    //    //            CommuneCode = code
    //    //        };

    //    //        _db.CommuneClientStatuses.Add(client);
    //    //    }

    //    //    client.CommuneCode = code;

    //    //    client.MachineName =
    //    //        clientRequest.MachineName;

    //    //    client.ApplicationVersion =
    //    //        clientRequest.ApplicationVersion;

    //    //    client.LastHeartbeat =
    //    //        clientRequest.LastHeartbeat.ToUniversalTime().AddHours(1);

    //    //    updatedClients.Add(client);
    //    //}


    //    // --------------------------------------------------
    //    // Save everything
    //    // --------------------------------------------------

    //    await _db.SaveChangesAsync();


    //    // --------------------------------------------------
    //    // Notify CommuneServer change
    //    // --------------------------------------------------

    //    await _hub.Clients.All.SendAsync(
    //        "clientStatusChanged",
    //        ClientMonitoringService.ToDto(c));


    //    // --------------------------------------------------
    //    // Notify CommuneClient changes
    //    // --------------------------------------------------

    //    //foreach (var client in updatedClients)
    //    //{
    //    //    await _hub.Clients.All.SendAsync(
    //    //        "communeClientStatusChanged",
    //    //        client);
    //    //}
    //    var clientDtos = updatedClients
    //                .Select(x => new CommuneClientStatusDto
    //                {
    //                    Id = x.Id,
    //                    ClientCode = x.ClientCode,
    //                    CommuneCode = x.CommuneCode,
    //                    MachineName = x.MachineName,
    //                    ApplicationVersion = x.ApplicationVersion,
    //                    LastHeartbeat = x.LastHeartbeat
    //                })
    //                .ToList();

    //    if (clientDtos.Count > 0)
    //    {
    //        await _hub.Clients.All.SendAsync(
    //            "communeClientsStatusChanged",
    //            clientDtos);
    //    }

    //    return Ok();
    //}

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
