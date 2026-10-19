using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class Previous4percent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [ForeignKey("Partie")]
        public long IdPartie { get; set; }
        public virtual Partie? Partie { get; set; }

        [ForeignKey("CircoAPW")]
        public string? CodeCircoAPW { get; set; }
        public virtual CircoAPW? CircoAPW { get; set; }

        [ForeignKey("CircoAPC")]
        public string? CodeCircoAPC { get; set; }
        public virtual CircoAPC? CircoAPC { get; set; }

        public int NbSiegeObtenue { get; set; }

        public double Pourcentage { get; set; }
    }
}
