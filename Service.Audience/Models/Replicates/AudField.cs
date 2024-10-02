using Service.Audience.Models.EF;

namespace Service.Audience.Models.Replicates
{

    public class AudField(EFAudField context)
    {
        public EFAudField Context { get; set; } = context;
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public CustomFieldType Type { get => Context.Type; set => Context.Type = value; }
    }
}
