using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class CandidatInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCandidatInfo { get; set; }

        public long? IdElecteur { get; set; }

        public string? NInscription { get; set; }

       // [ForeignKey("CommuneVote")]
        public string? CodeCommuneVote { get; set; }

       // public virtual Commune CommuneVote { get; set; }

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

        [ForeignKey("Fonction")]
        public long IdFonction { get; set; }
        public virtual CandidatFonction Fonction { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Adress { get; set; }   //العنوان الشخصي

        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;

        //niveau etudes relations
         [ForeignKey("NiveauEtude")]
        public int NiveauEtudeId { get; set; }
        public virtual NiveauEtude NiveauEtude { get; set; }

        public string Photo {  get; set; } = string.Empty;

        public string? ServiceNationale { get; set; } = string.Empty; // الوضعية إزاء الخدمة الوطنية
        public string? EtatFammiliale { get; set; }  //الحالة العائلية
        public  string? Email { get; set; }
        public string? Societe { get; set; }

    }
}
