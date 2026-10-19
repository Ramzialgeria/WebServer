using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class Souscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdSouscription { get; set; }

        public long? IdElecteur { get; set; }
        public string NInscription { get; set; }
        public string CodeCommuneVote { get; set; }
        public string Nin { get; set; }
        public string NomAr { get; set; }
        public string PrenomAr { get; set; }
        public string NomFr { get; set; }
        public string PrenomFr { get; set; }
        public string? DateNaissance { get; set; }
        public string? LieuNaissance { get; set; }
        public string? CodeLieuNaissance { get; set; }
        public string? ActN { get; set; }
        public bool? Presume { get; set; } = false;
        public string? Genre { get; set; }
        public string? PrenomPere { get; set; }
        public string? NomMere { get; set; }
        public string? PrenomMere { get; set; } 
        public string? Adress { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; } = null;
        public DateTime? UpdatedAt { get; set; } = null;
        public string? ValidatedBy { get; set; } = null;
        public DateTime? ValidatedAt { get; set; } = null;

        public string? NumSouscription { get; set; }
        public string? NCarte { get; set; }
        public string? DateCarte { get; set; }
        public string? AutoriteCarte { get; set; }
        public string? TypeCarte { get; set; }

        public bool? Valide { get; set; } = null;

        [ForeignKey("TypeSouscription")]
        public long IdTypeSouscription { get; set; }
        public virtual TypeSouscription TypeSouscription { get; set; }

        [ForeignKey("ListeElectoral")]
        public long IdListeElectoral { get; set; }
        public virtual ListeElectoral ListeElectoral { get; set; }
    }
}
