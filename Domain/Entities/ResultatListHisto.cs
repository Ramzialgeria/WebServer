using ClientMonitoring.Web.Domain.SahredKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class ResultatListHisto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int NumResult {  get; set; }

        [ForeignKey("ListeElectoral")]
        public long IdListeElectoral { get; set; }
        public virtual ListeElectoral? ListeElectoral { get; set; }

        public StatusResult StatusResult { get; set; }
        public string PathDocResult { get; set; } // القرار
        public DateTime DateResult { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateResultSystem { get; set; }
        public string? UpdatedBy { get; set; } = null;
        public DateTime? UpdatedAt { get; set; } = null;

        [ForeignKey("typeRejet")]
        public long? IdTypeRejet { get; set; }
        public virtual MotifRejet? typeRejet { get; set; }

        public string? MotifRejetDetail { get; set; }

        [InverseProperty("ResultatListHisto")]
        public ICollection<RecoursListe> RecoursListe { get; set; }

    }
}
