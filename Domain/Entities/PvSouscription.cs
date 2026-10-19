using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientMonitoring.Web.Domain.Entities
{
    public class PvSouscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdPv { get; set; }

        public int NbTotal { get; set; }                //           عدد استمارات التوقيعات الفردية المقدمة
        public int NbTotalNotValide { get; set; }       //           عدد استمارات التوقيع غير المستوفية للشروط القانونية
        public int NbNotOK { get; set; }                //           استمارة غير نظامية، مشطوبة و/أو ممزقة كليا
        public int NbNotSignedByOfficer { get; set; }   //           استمارة غير مصادق عليها من قبل ضابط عمومي
        public int NbNotSigned { get; set; }            //           استمارة لا تحمل توقيع و/أو بصمة الموقع
        public int NbNoInfos { get; set; }              //           استمارة خالية من البيانات
        public int NbHorsWilaya { get; set; }           //           غير مسجلة في الدائرة الانتخابية
        public int NbDoublons { get; set; }             //           متكررة 
        public int NbOther { get; set; }                //           أخرى        
        public int NbValid { get; set; }                //           الاستمارات المقبولة 

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; } = null;
        public DateTime? UpdatedAt { get; set; } = null;
        public string? ValidatedInfoBy { get; set; } = null;
        public DateTime? ValidatedInfoAt { get; set; } = null;
        public string? ValidatedPvBy { get; set; } = null;
        public DateTime? ValidatedPvAt { get; set; } = null;

        [ForeignKey("ListeElectoralCirconscription")]
        public long IdListeElectoralCirconscription { get; set; }
        public virtual ListeElectoral ListeElectoralCirconscription { get; set; }
    }
}
