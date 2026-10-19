using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class Representant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdRepresentant { get; set; }

        public string? Nin { get; set; }

        public string NomAr { get; set; }

        public  string PrenomAr { get; set; }

        public  string NomFr { get; set; }

        public  string PrenomFr { get; set; }

        public  DateTime     DateNaissance { get; set; }

        public  string? LieuNaissance { get; set; }

        public  string? CodeLieuNaissance { get; set; }

        public  string? ActN { get; set; }

        public  bool? Presume { get; set; } = false;

        public  string? Genre { get; set; }

        public  string? PrenomPere { get; set; }

        public  string? NomMere { get; set; }

        public  string? PrenomMere { get; set; }

        public  string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;
    }
}
