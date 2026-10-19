using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class EnqueteResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("Candidature")]
        public long IdCandidat { get; set; }
        public virtual Candidature? Candidature { get; set; }

        [ForeignKey("enqueteStructure")]
        public long IdEnqueteStructure { get; set; }
        public virtual EnqueteStructure? enqueteStructure { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PathDocResult { get; set; } // وثيقة الرد
        public DateTime DateResult { get; set; }  // تاريخ الرد

        public bool? IsPositif {  get; set; } // null = غير معروف , true = إيجابي , false = سلبي 
    }
}
