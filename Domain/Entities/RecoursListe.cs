using ClientMonitoring.Web.Domain.SahredKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class RecoursListe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdRecours { get; set; }

        public TypeRecours TypeRecours { get; set; }
        public DateTime DateRecour { get; set; }
        public DateTime DateRecourSystem { get; set; }
        public string? PathDocRecour { get; set; }
        public string CreatedRecourBy { get; set; }
        public string? UpdatedRecourBy { get; set; }
        public DateTime? UpdatedRecourAt { get; set; }


        public StatusResult? StatusReponse { get; set; }
        public DateTime? DateReponse { get; set; }
        public DateTime? DateReponseSystem { get; set; }
        public string? PathDocReponse { get; set; }
        public string? CreatedReponseBy { get; set; }
        public string? UpdatedReponseBy { get; set; }
        public DateTime? UpdatedReponseAt { get; set; }


        [ForeignKey("ResultatListHisto")]
        public long IdResultatListHisto { get; set; }
        public virtual ResultatListHisto? ResultatListHisto { get; set; }

        [ForeignKey("MotifRejet")]
        public long? IdMotifRejet { get; set; }
        public virtual MotifRejet? MotifRejet { get; set; }
    }
}
