using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class TypeConditionCandidature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdTypeCondition { get; set; }
        public  string DescriptionAr { get; set; }
        public ICollection<Condition> ConditionCandidatures { get; set; } = new List<Condition> ();
    }
}
