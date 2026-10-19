using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class CircoInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("Circonscription")]
        public string CodeCirconscription { get; set; }
        public virtual CircoAPW Circonscription { get; set; }

        [ForeignKey("InfoValidation")]
        public long IdInfoValidation { get; set; }
        public virtual InfoValidation InfoValidation { get; set; }

         public string FilePath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } 
        public DateTime? UpdatedAt { get; set; }

        public string? ValidatedBy { get; set; }
        public DateTime? ValidatedAt { get; set; }
    }
}
