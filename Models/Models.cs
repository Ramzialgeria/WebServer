using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace ClientMonitoring.Web.Models;
public class ApplicationUser : IdentityUser { }
public class Client 
{ 
    public long Id {get;set;}
    public string ClientCode {get;set;}=null!;
    public string ClientSecretHash {get;set;}=null!;
    public DateTime? LastHeartbeat {get;set;}
    public bool IsEnabled {get;set;}=true;
}

public class HeartbeatRequest
{
    public string ClientCode {get;set;}=null!;
}

public class NetworkReport 
{ 
    public bool Connected {get;set;}
    public string? InterfaceName {get;set;} 
    public string? IpAddress {get;set;} 
    public string? Gateway {get;set;} 
}

public class CommuneClientHeartbeat
{
    public string ClientCode { get; set; }
    public string MachineName { get; set; }
    public string ApplicationVersion { get; set; }
    public DateTime LastHeartbeat { get; set; }
}

public class ClientStatusDto
{
    public long Id {get;set;}
    public string ClientCode {get;set;}=null!;
    public string MachineName {get;set;}=null!;
    public bool Online {get;set;}
    public bool LocalNetworkConnected {get;set;} 
    public bool ServerNetworkConnected {get;set;}
    public bool ApiConnected {get;set;}
    public string? LocalIp {get;set;}
    public string? ServerIp {get;set;}
    public DateTime? LastHeartbeat {get;set;}
    public string? ApplicationVersion {get;set;}
}

public class CommuneClientStatus
{
    public int Id { get; set; }

    public string ClientCode { get; set; }

    public string CommuneCode { get; set; }

    public string MachineName { get; set; }

    public string ApplicationVersion { get; set; }

    public DateTime LastHeartbeat { get; set; }
}

public class CommuneClientStatusDto
{
    public int Id { get; set; }

    public string ClientCode { get; set; }

    public string CommuneCode { get; set; }

    public string MachineName { get; set; }

    public string ApplicationVersion { get; set; }

    public DateTime LastHeartbeat { get; set; }
}

/// <summary>
/// 
/// </summary>
public class Wilaya
{
    public string codeWilaya { get; set; } = "";

    public string Libele { get; set; } = "";

    public virtual ICollection<Commune> Communes { get; set; }
        = new List<Commune>();
}

public class Commune
{
    public string codecommune { get; set; } = "";

    public string libeleCommune { get; set; } = "";

    public string codewilaya { get; set; } = "";

    public virtual Wilaya? Wilaya { get; set; }
}

public class Center_V
{
    public long ID_center { get; set; }

    public string descriptio_ar { get; set; } = "";

    public string adress { get; set; } = "";

    public string id_structure { get; set; } = "";
}

public class BureauV
{
    public long ID_Bureau { get; set; }

    public string CBureau { get; set; } = "";

    public int? Nb_Inscrits { get; set; }

    public long? ID_center { get; set; }
}

public class AppUser
{
    public long Id { get; set; }

    public string Username { get; set; } = "";

    public string PasswordHash { get; set; } = "";

    public string IdStructure { get; set; } = "";

    public string Role { get; set; }
}

