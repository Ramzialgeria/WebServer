using ClientMonitoring.Web.Domain.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ClientMonitoring.Web.Domain.Entities;

//public class Structure
//{
//    [Key]
//    public required string IdStructure { get; set; }

//    public required string LIBELLEARABE { get; set; }

//    public string? LIBELLELATIN { get; set; }
//    [InverseProperty("Structure")]
//    public ICollection<AppUser> Users { get; set; }
//}

//public class Wilaya : Structure
//{
//    public long IdWilaya { get; set; }

//    public virtual ICollection<Commune> Communes { get; set; } = [];
//}

//public class Commune : Structure
//{
//    [ForeignKey("Wilaya")]
//    public required string CodeWilaya { get; set; }

//    public virtual required Wilaya Wilaya { get; set; }

//    public long IdCommune { get; set; }
//}

//public class Central : Structure
//{

//}
