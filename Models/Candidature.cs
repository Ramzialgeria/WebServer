using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;


    public class Candidature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdCandidat { get; set; }

        public required string NomAr { get; set; }

        public required string PrenomAr { get; set; }

    public required string NomFr { get; set; }

    public required string PrenomFr { get; set; }


    [ForeignKey("ListeElectoral")]
        public long IdListeElectoral { get; set; }
        [JsonIgnore]
        public virtual ListeElectoral? ListeElectoral { get; set; }

       
        public int? Order {  get; set; }

       
    }


