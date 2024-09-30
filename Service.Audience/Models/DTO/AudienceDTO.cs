using Service.Audience.Models.Replicates;

namespace Service.Audience.Models.DTO
{
    public class AudienceDTO
    {
        public AudienceDTO() { }
        public AudienceDTO(AudienceRepl context)
        {
            id = context.Id;
            name = context.Name;
            housingId = context.HousingId;
            capacity = context.Capacity;
            number = context.Number;
            floor = context.Floor;

        }
        public int id { get; set; }
        public string name { get; set; }
        public int housingId { get; set; }
        public int capacity { get; set; }
        public int number { get; set; }
        public int? floor { get; set; }
        public void Map(ref AudienceRepl context)
        {
            context.Name = name;
            context.HousingId = housingId;
            context.Capacity = capacity;
            context.Number = number;
            context.Floor = floor;
        }
    }
}
