using Service.Audience.Models.EFModels;
using Service.Common.ModelExtensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Audience.Models.EF
{
    public class EFAudValue : EFBaseModel
    {

        [ForeignKey("Audience")]
        public int AudienceId { get; set; }
        public virtual EFAudience Audience { get; set; }

        [ForeignKey("AudField")]
        public int CustomFieldId { get; set; }
        public virtual EFAudField CustomField { get; set; }
        public string? Value { get; set; }
    }
}
