using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class Navbar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdNavBar { get; set; }

        public required string Title { get; set; }

        public required string Controller { get; set; }

        public required string View { get; set; }

        public required string Structure { get; set; }

        public bool IsActive { get; set; }

        public bool ControlDate { get; set; }

        public required DateTime DateDebut { get; set; }

        public required DateTime DateFin { get; set; }

        public long IdPere { get; set; }

        public int Order { get; set; }


        [ForeignKey("NavbarType")]
        public long IdType { get; set; }

        public virtual NavbarType? Type { get; set; }


        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;

    }

    public class NavbarType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdType { get; set; }

        public required string Description { get; set; }

        public required string CreatedBy { get; set; }

        public required DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;
    }
}
