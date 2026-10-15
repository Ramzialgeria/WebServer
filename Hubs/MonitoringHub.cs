using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
namespace ClientMonitoring.Web.Hubs;
[Authorize] public class MonitoringHub:Hub{}
