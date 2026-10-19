using ClientMonitoring.Web.Data; 
using ClientMonitoring.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientMonitoring.Web.Services;
public class ClientMonitoringService
{
    readonly ApplicationDbContext1 _db;
    public ClientMonitoringService(ApplicationDbContext1 db)=>_db=db;
    public async Task<List<ClientStatusDto>> GetAsync()=> (await _db.Clients.AsNoTracking().OrderBy(x=>x.ClientCode).ToListAsync()).Select(ToDto).ToList();
    public static ClientStatusDto ToDto(Client c)=>new()
    {
        Id=c.Id,
        ClientCode=c.ClientCode,
        //MachineName=c.MachineName,
        Online=c.LastHeartbeat.HasValue&&DateTime.UtcNow-c.LastHeartbeat.Value<=TimeSpan.FromSeconds(30),
        //LocalNetworkConnected=c.LocalNetworkConnected,
        //ServerNetworkConnected=c.ServerNetworkConnected,
        //ApiConnected=c.ApiConnected,
        //LocalIp=c.LocalIp,
        //ServerIp=c.ServerIp,
        LastHeartbeat=c.LastHeartbeat,
        //ApplicationVersion=c.ApplicationVersion
    };
}
