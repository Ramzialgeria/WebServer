using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ClientMonitoring.Web.Domain.SahredKernel
{
    public enum StatusResultEnquete
    {
        [Display(Name = "إيجابي")]
        Accepted = 1,

        [Display(Name = "سلبي")]
        Rejected = 2,

        [Display(Name = "غير محدد")]
        Encour = 3
    }
}
