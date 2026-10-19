using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities;

public class CircoAPW
{
    [Key]
    public required string CodeCircoAPW { get; set; }

    //[ForeignKey("Wilaya")]
    public required string CodeWilaya { get; set; }

    //public virtual required Wilaya Wilaya { get; set; }

    public required string LibelleAr { get; set; }

    public required string LibelleFr { get; set; }
    public bool ISEtranger { get; set; } = false;
    public int NbSiege { get; set; }

    public int NbPlace { get; set; }

    public int NbListe { get; set; }

    public int NbSouscription { get; set; }

    public int Supleants { get; set; }


    public virtual ICollection<CirconscriptionFeature> CirconscriptionFeatures { get; set; } = new List<CirconscriptionFeature>();

    
    public required string TrubunalRegional { get; set; }

    public string? CodeCorrespondant { get; set; }

    [InverseProperty("CirconscriptionSent")]
    public virtual ICollection<JugementCorrespondance>? JugementCorrespondanceSent { get; set; }

    [InverseProperty("CirconscriptionReturn")]
    public virtual ICollection<JugementCorrespondance>? JugementCorrespondanceReturn { get; set; }

    public ICollection<ListeElectoralAPW> Listes { get; set; } = new List<ListeElectoralAPW>();

}

public class CircoAPC
{
    [Key]
    public required string CodeCircoAPC { get; set; }

    public required string LIBELLEARABE { get; set; }
    public string? LIBELLELATIN { get; set; }


    public int NbSiege { get; set; }

    public int NbPlace { get; set; }

    public int NbListe { get; set; }

    public int NbSouscription { get; set; }

    public int Supleants { get; set; }

    public virtual ICollection<CirconscriptionFeature> CirconscriptionFeatures { get; set; } = new List<CirconscriptionFeature>();


    [ForeignKey("CircoAPW")]
    public string CodeCircoAPW { get; set; }
    public virtual required CircoAPW CircoAPW { get; set; }

    public ICollection<ListeElectoralAPC> Listes { get; set; } = new List<ListeElectoralAPC>();
}



