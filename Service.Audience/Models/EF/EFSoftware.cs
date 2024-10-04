using Service.Audience.Models.EFModels;
using Service.Common.ModelExtensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Audience.Models.EF
{
    public class EFSoftware : EFBaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public LicenseType LicenseType { get; set; }
        public string LicenseKey { get; set; }

        [Required]
        public int NumberPC { get; set; }
        public DateTime? LicenseExpirationDate { get; set; }

        [ForeignKey("Audience")]
        public int AudienceId { get; set; }
        public virtual EFAudience Audience { get; set; }
    }

    public enum LicenseType
    {
        Freeware,
        Shareware,
        Commercial
    }
}
