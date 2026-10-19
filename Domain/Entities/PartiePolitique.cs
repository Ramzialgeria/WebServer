using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ClientMonitoring.Web.Domain.Identity;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class PartiePolitique
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public  string LibelleAr { get; set; }

        public string? LibelleFr { get; set; }

        public string? CodeList { get; set; }

        public  DateTime DateCreation { get; set; }

        public  string CreatedBy { get; set; }

        public  DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } 

        public DateTime? UpdatedAt { get; set; }
        public bool IsEnabled { get; set; } = true;

        public string? NumIdentifiant { get; set; }
        [InverseProperty("PartiePolitique")]
        public ICollection<ListeElectoral> listeElectorals { get; set; }
    }

    public class Partie : PartiePolitique
    {
        public  string NomChef { get; set; }

        public  string TitreChef { get; set; }
    }

    public class Libre : PartiePolitique
    {
        public  string CodeWilaya { get; set; }
    }

    public class Union : PartiePolitique
    {
        public virtual IList<string> Parties { get; set; } = [];
    }
}
