using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ClientMonitoring.Web.Domain.Identity;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class Condition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCondition { get; set; } 

        public required string Description { get; set; }

        //public required string Type { get; set; }
        [ForeignKey(nameof(TypeConditionCandidature))]
        public long IdTypeCondition { get; set; }
        public TypeConditionCandidature? TypeConditionCandidature { get; set; } 

        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;
    }

    public class ConditionListe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdConditionListe { get; set; }

        [ForeignKey("Condition")]
        public long IdCondition { get; set; }
        public virtual Condition? Condition { get; set; }

        [ForeignKey("ListeElectoral")]
        public long IdListeElectoral { get; set; }

        public virtual ListeElectoral? ListeElectoral { get; set; }

        public bool IsValide { get; set; }

        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;
    }

    public class ConditionCandidat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdConditionCandidat { get; set; }

        [ForeignKey("Condition")]
        public long IdCondition { get; set; }
        public virtual Condition? Condition { get; set; }

        [ForeignKey("Candidature")]
        public long IdCandidat { get; set; }

        public virtual Candidature? Candidature { get; set; }

        public bool IsValide { get; set; }

        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;
    }
}
