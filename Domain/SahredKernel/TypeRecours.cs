using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClientMonitoring.Web.Domain.SahredKernel
{
    public enum TypeRecours
    {
        [Display(Name = "الطعن")]
        Recour = 1,

        [Display(Name = "الاستئناف")]
        Re_Recour = 2,

        [Display(Name = "استئناف السلطة")]
        Re_Recour_Anie = 3,

    }
}
