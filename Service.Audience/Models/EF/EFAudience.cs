using Service.Audience.Models.EF;
using Service.Common.ModelExtensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Audience.Models.EFModels
{
    public class EFAudience : EFBaseModel
    {
        public string Name { get; set; }
        public AudienceType AudienceType { get; set; }
        public int Capacity { get; set; }
        public int? Floor { get; set; }
        public int Number { get; set; }


        [ForeignKey("Housing")]
        public int? HousingId { get; set; }
        public virtual EFHousingSummary? Housing { get; set; }


        public virtual List<EFAudValue> CustomFieldValues { get; set; } = new List<EFAudValue>();
    }


    public enum AudienceType
    {
        Lecture,
        Practical,
        Gym,
        Other
    }
}
