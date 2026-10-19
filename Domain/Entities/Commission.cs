using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class Commission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCommission { get; set; }

        public string Nin { get; set; }

        public string NomAr { get; set; }

        public string PrenomAr { get; set; }

        public string NomFr { get; set; }

        public string PrenomFr { get; set; }

        public DateTime DateNaissance { get; set; }

        public string LieuNaissance { get; set; }

        public string CodeLieuNaissance { get; set; }

        public string ActN { get; set; }

        public  bool Presume { get; set; } = false;

        public  string Genre { get; set; }

        public  string PrenomPere { get; set; }

        public  string NomMere { get; set; }

        public  string PrenomMere { get; set; }

        public  string CreatedBy { get; set; }

        public  DateTime CreatedAt { get; set; }

        public bool? IsChefCommission {  get; set; }

        [ForeignKey("Circonscription")]
        public string CodeCirconscription { get; set; }

        public virtual CircoAPW? Circonscription { get; set; }

        public bool Validated { get; set; } = false;

        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
