using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientMonitoring.Web.Domain.Entities;


// Models/Features.cs

public class Features
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(100)]
    public string FeatureName { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [JsonIgnore]
    public ICollection<CirconscriptionFeature> CirconscriptionFeatures { get; set; } = new List<CirconscriptionFeature>();
}

// Models/StructureFeature.cs
public class CirconscriptionFeature
{
    public string CirconscriptionId { get; set; }

    [JsonIgnore]
    public CircoAPW Circonscription { get; set; }

    public Guid FeatureId { get; set; }
    public Features Feature { get; set; }

    public bool IsEnabled { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string UpdatedBy { get; set; }
}
