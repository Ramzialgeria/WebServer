using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class NiveauEtude
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
        public bool isActive { get; set; } = true;
    }
}
