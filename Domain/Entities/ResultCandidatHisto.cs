using ClientMonitoring.Web.Domain.SahredKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class ResultCandidatHisto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int NumResult { get; set; }
        public StatusResult StatusResult { get; set; }
        public string PathDocResult { get; set; } // القرار
        public DateTime DateResult { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateResultSystem { get; set; }
        public string? UpdatedBy { get; set; } = null;
        public DateTime? UpdatedAt { get; set; } = null;

        [ForeignKey("Candidature")]
        public long IdCandidat { get; set; }
        public virtual Candidature? Candidature { get; set; }

        [ForeignKey("MotifRejet")]
        public long? IdMotifRejet { get; set; }
        public virtual MotifRejet? MotifRejet { get; set; }

        public string? MotifRejetDetail { get; set; }

        [InverseProperty("ResultCandidatHisto")]
        public ICollection<Recours> Recours { get; set; }
    }
}
