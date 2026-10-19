using ClientMonitoring.Web.Domain.Identity;
using System.Text.Json.Serialization;
using ClientMonitoring.Web.Domain.SahredKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public abstract class ListeElectoral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdListe { get; set; }

        [ForeignKey("PartiePolitique")]
        public long IdPartiePolitique { get; set; }

        public virtual PartiePolitique? PartiePolitique { get; set; }


        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;

        [InverseProperty("ListeElectoral")]
        public ICollection<Candidature> Candidatures { get; set; }

        [InverseProperty("ListeElectoral")]
        public ICollection<DossierListeElectoral> dossiersListe {  get; set; }

        [InverseProperty("ListeElectoral")]
        public ICollection<RetraitDossier> retraits { get; set; }
        public bool IsDossierValid { get; set; }
        public DateTime DateFirstRetrait { get; set; } // نية الترشح
        public DateTime DateFirstRetraitSystem { get; set; } // system
        public DateTime? DateDepotDossier { get; set; } // تاريخ إيداع الملف
        public DateTime? DateDepotDossierSystem { get; set; } // system تاريخ إيداع الملف
        public StatusResult Status { get; set; } = StatusResult.Encour;//

        [InverseProperty("ListeElectoral")]
        public ICollection<ResultatListHisto> resultsList { get; set; }

        [InverseProperty("ListeElectoral")]
        public ICollection<Souscription> Souscriptions { get; set; }

        public string? PathPv_Souscription { get; set; }
        public DateTime? DatePv_Souscription { get; set; }
        public DateTime? DatePv_SouscriptionSystem { get; set; }

        // etat souscription liste
        //public ICollection<ConditionListe> ConditionsListe { get; set; }

        [InverseProperty("ListeElectoral")]
        public ICollection<ConditionListe> ConditionsListe { get; set; }

        public virtual DesisteListe? DesisteListe { get; set; }

        public virtual PvSouscription? PvSouscription { get; set; }

        public string? PathDossierListe { get; set; }

        public string? PathBAT { get; set; }
        public DateTime? ImpremerieDownloadAt { get; set; }

        public required string TypeElection { get; set; }

    }

    public class ListeElectoralAPW : ListeElectoral
    {

        [ForeignKey("CircoAPW")]
        public required string CodeCircoAPW { get; set; }

        public virtual CircoAPW? CircoAPW { get; set; }


        [ForeignKey("Scrutin")]
        public long IdScrutin { get; set; }

        public virtual Scrutin? Scrutin { get; set; }
    }

    public class ListeElectoralAPC : ListeElectoral
    {

        [ForeignKey("CircoAPC")]
        public required string CodeCircoAPC { get; set; }

        public virtual CircoAPC? CircoAPC { get; set; }


        [ForeignKey("Scrutin")]
        public long IdScrutin { get; set; }

        public virtual Scrutin? Scrutin { get; set; }
    }

    public class DossierListeElectoral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdDossier { get; set; }

        [ForeignKey("ListeElectoral")]
        public long IdListeElectoral { get; set; }
        [JsonIgnore]
        public virtual ListeElectoral? ListeElectoral { get; set; }

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

    public class TypeDossier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdType { get; set; }

        public required string Description { get; set; }

        public required string Type { get; set; } //1 liste, 2 candidat

        public required IList< string> Extensions { get; set; }

        public bool IsObligatoire { get; set; }

        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;
    }
}
