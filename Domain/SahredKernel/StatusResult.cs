using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClientMonitoring.Web.Domain.SahredKernel
{
    public enum StatusResult
    {
        [Display(Name = "مقبول")]
        Accepted = 1,

        [Display(Name = "مرفوض")]
        Rejected = 2,

        [Display(Name = "انسحاب")]
        Desiste = 3,

        [Display(Name = "قيد المعالجة")]
        Encour = 4
    }
}
