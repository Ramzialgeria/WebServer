using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class RetraitDossier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdRetraitDossier { get; set; }
        public int NbRetraitDossier {  get; set; }
        public  int NBretraitSouscription { get; set; } // souscription ou dossier
        public DateTime DateRetrait { get; set; } 
        public DateTime DateRetraitSystem { get; set; }// system

        [ForeignKey("ListeElectoral")]
        public long IdListeElectoral { get; set; }
        public virtual ListeElectoral? ListeElectoral { get; set; }

        [ForeignKey("Representant")]
        public long IdRepresentant { get; set; }
        public virtual Representant? Representant { get; set; }
    }
}
