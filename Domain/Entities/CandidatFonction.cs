using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    
    public class CandidatFonction
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long IdFonction { get; set; }

            public required string Description { get; set; }
        
    }
}
