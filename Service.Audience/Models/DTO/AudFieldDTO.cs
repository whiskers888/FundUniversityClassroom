using Service.Audience.Models.EF;
using Service.Audience.Models.Replicates;

namespace Service.Audience.Models.DTO
{
    public class AudFieldDTO
    {
        public AudFieldDTO() { }
        public AudFieldDTO(AudField context)
        {
            id = context.Id;
            name = context.Name;
            type = context.Type;

        }
        public int id { get; set; }
        public string name { get; set; }
        public CustomFieldType type { get; set; }
        public void Map(ref AudField context)
        {
            context.Name = name;
            context.Type = type;
        }
    }
}
