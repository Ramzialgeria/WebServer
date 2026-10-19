using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class JugementCorrespondance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

		public string? TypeCorrespondance { get; set; }
        public string? PathFileName { get; set; }
        
        [ForeignKey("CirconscriptionSent")]
        public string? CodeWilayaSent { get; set; }
        public virtual CircoAPW? CirconscriptionSent { get; set; }
        
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? PathReturn_FileName { get; set; }

        [ForeignKey("CirconscriptionReturn")]
        public string? CodeWilayaReturn { get; set; }
        public virtual CircoAPW? CirconscriptionReturn { get; set; }

        public string? Return_CreatedBy { get; set; }
        public DateTime? Return_CreatedAt { get; set; }

        [ForeignKey("Candidature")]
        public long? IdCandidat { get; set; }
        public virtual Candidature? Candidature { get; set; }

        [ForeignKey("ListCirco")]
        public long? IdListCirco { get; set; }
        public virtual ListeElectoral? ListCirco { get; set; }

       
    }
}
