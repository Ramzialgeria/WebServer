using ClientMonitoring.Web.Domain.SahredKernel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class Candidature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCandidat { get; set; }

        [ForeignKey("CandidatInfo")]
        public long IdCandidatInfo { get; set; }

        public virtual CandidatInfo? CandidatInfo { get; set; }

        [ForeignKey("ListeElectoral")]
        public long IdListeElectoral { get; set; }
        [JsonIgnore]
        public virtual ListeElectoral? ListeElectoral { get; set; }

        public bool IsRepresent {  get; set; }

        public  string CreatedBy { get; set; }

        public  DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;
        public StatusResult ResultFinale { get; set; }

        [InverseProperty("Candidature")]
        public ICollection<ResultCandidatHisto> ResultsCandidat { get; set; }

        [InverseProperty("Candidature")]
        public ICollection<EnqueteResult> EnqueteResults { get; set; }

        [ForeignKey("TypeMutted")]
        public long? IdTypeMutted { get; set; }
        public virtual TypeMutted? TypeMutted { get; set; }

        //order unique avec IdListe
        public int? Order {  get; set; }

        //public bool? IsRemplacant { get; set; }

        // ============================================
        // Replacement
        // ============================================

        // The candidature that this candidature replaces
        public long? IdCandidatureRemplace { get; set; }

        [ForeignKey(nameof(IdCandidatureRemplace))]
        public virtual Candidature? CandidatureRemplace { get; set; }

        [ForeignKey("PreviousReject")]
        public long? IdPreviousReject { get; set; }
        public virtual PreviousReject? PreviousReject { get; set; }

        public long? DuplicateGroupId { get; set; }
    }

    public class DossierCandidat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdDossier { get; set; }

        [ForeignKey("Candidature")]
        public long IdCandidat { get; set; }

        public virtual Candidature? Candidature { get; set; }

        [ForeignKey("TypeDossier")]
        public long IdType { get; set; }

        public virtual TypeDossier? TypeDossier { get; set; }
        public bool IsValide { get; set; }

        public required string Path { get; set; }

        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;
    }
}
