using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class CircoAPW
{
    [Key]
    public required string CodeCircoAPW { get; set; }

    public required string CodeWilaya { get; set; }

 
    public required string LibelleAr { get; set; }

    public required string LibelleFr { get; set; }

    public ICollection<ListeElectoralAPW> Listes { get; set; } = new List<ListeElectoralAPW>();

}

public class CircoAPC
{
    [Key]
    public required string CodeCircoAPC { get; set; }

    public required string LIBELLEARABE { get; set; }
   

    [ForeignKey("CircoAPW")]
    public string CodeCircoAPW { get; set; }
    public virtual required CircoAPW CircoAPW { get; set; }

    public ICollection<ListeElectoralAPC> Listes { get; set; } = new List<ListeElectoralAPC>();
}



