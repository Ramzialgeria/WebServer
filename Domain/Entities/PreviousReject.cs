using ClientMonitoring.Web.Domain.SahredKernel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class PreviousReject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Nin { get; set; }
        
        public long? IdElecteur { get; set; }

        public  string FullNameAr { get; set; }

        public  string FullNameFr { get; set; }

        [ForeignKey("MotifRejet")]
        public long? IdMotifRejet { get; set; }
        public virtual MotifRejet? MotifRejet { get; set; }

        [ForeignKey("Scrutin")]
        public long IdScrutin { get; set; }
        public virtual Scrutin? Scrutin { get; set; }
    }


}
