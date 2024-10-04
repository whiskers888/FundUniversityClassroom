using Service.Common.ModelExtensions;

namespace Service.Audience.Models.EF
{
    public class EFAudField : EFBaseModel
    {
        public string Name { get; set; }

        public CustomFieldType Type { get; set; }
    }

    public enum CustomFieldType
    {
        Text,
        Number,
        Date,
        Boolean
    }
}
