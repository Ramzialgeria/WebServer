
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    public abstract class ListeElectoral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdListe { get; set; }

    public string LibelleAr { get; set; }

    public string? NumIdentifiant { get; set; }

    [InverseProperty("ListeElectoral")]
        public ICollection<Candidature> Candidatures { get; set; }


    }

    public class ListeElectoralAPW : ListeElectoral
    {

        [ForeignKey("CircoAPW")]
        public required string CodeCircoAPW { get; set; }

        public virtual CircoAPW? CircoAPW { get; set; }

    }

    public class ListeElectoralAPC : ListeElectoral
    {

        [ForeignKey("CircoAPC")]
        public required string CodeCircoAPC { get; set; }

        public virtual CircoAPC? CircoAPC { get; set; }

    }


