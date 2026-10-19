using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class DesisteListe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdDesisteListe { get; set; }
        public int NBSouscriptionRecuperer { get; set; }
        public required string Remarque { get; set; } = string.Empty;
        public required string CreatedBy { get; set; }
        public DateTime DateDesiste { get; set; }
        public DateTime DateDesisteSystem { get; set; }
        public string? UpdatedBy { get; set; } = null;
        public DateTime? UpdatedAt { get; set; } = null;
        [ForeignKey("ListeElectoralCirconscription")]
        public long IdListeCirconscription { get; set; }
        public virtual ListeElectoral? ListeElectoralCirconscription { get; set; }

    }
}
