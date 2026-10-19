using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace ClientMonitoring.Web.Domain.Identity;


public class AppUser : IdentityUser
{
    [ForeignKey("Structure")]
    public required string IdStructure { get; set; }

    public virtual Structure? Structure { get; set; }

    public required string TypeStructure { get; set; }

    public required string Nin { get; set; }

    public required string NomAr { get; set; }

    public required string PrenomAr { get; set; }

    public required string NomFr { get; set; }

    public required string PrenomFr { get; set; }

    public required DateTime DateNaissance { get; set; }

    public required string LieuNaissance { get; set; }

    public required string CodeLieuNaissance { get; set; }

    public required string ActN { get; set; }

    public required bool Presume { get; set; } = false;

    public required string Genre { get; set; }

    public required string PrenomPere { get; set; }

    public required string NomMere { get; set; }

    public required string PrenomMere { get; set; }

    public string? Photo { get; set; } = null;

    [ForeignKey("Fonction")]
    public long IdFonction { get; set; }

    public virtual Fonction? Fonction { get; set; }

    public bool Etat { get; set; } = false;

    public bool Delete { get; set; } = false;

    public DateTime? LastLoginDate { get; set; } = null;

    public DateTime? LastChangePasswordDate { get; set; } = null;

    public virtual ICollection<LoginHistory> LoginHistories { get; set; } = [];

    public virtual ICollection<AppRole> Roles { get; set; } = [];

    public required string CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public string? DeletedBy { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;
}
public class Fonction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdFonction { get; set; }

    public required string Description { get; set; }

    public required string Type { get; set; }

    public required string CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }
}

public class LoginHistory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required string UserId { get; set; }
    [JsonIgnore]
    public AppUser? User { get; set; }
    public required DateTime LoginDate { get; set; }
}

public class AppRole : IdentityRole
{
    public required string Description { get; set; }

    public required string TypeRole { get; set; }

    public required string CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;


    // Parameterless constructor for EF
    public AppRole()
    {

    }

    // Constructor with parameters for application logic
    public AppRole(string roleName, string description, string typeRole) : base(roleName)
    {
        Description = description;
        TypeRole = typeRole;
    }

    public ICollection<AppUser> Users { get; set; } = [];
}