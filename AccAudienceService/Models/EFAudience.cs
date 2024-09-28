using Helper;

namespace AccAudienceService.Models
{
    public class EFAudience : EFBaseModel
    {
        public int HousingId { get; set; }
        public string Name { get; set; }
        public AudienceType AudienceType { get; set; }
        public int Capacity { get; set; }
        public int? Floor { get; set; }
        public int Number { get; set; }
    }
}
