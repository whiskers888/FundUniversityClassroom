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
        public int HousingId { get; set; }
        public EFHousingSummary Housing { get; set; }
    }


    public enum AudienceType
    {
        Lecture,
        Practical,
        Gym,
        Other
    }
}
