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
            housing = context.Housing != null ? new HousingDTO(context.Housing) : null;
            capacity = context.Capacity;
            number = context.Number;
            floor = context.Floor;
            fieldValue = context.FieldValue;

        }
        public int id { get; set; }
        public string name { get; set; }
        public HousingDTO? housing { get; set; }
        public int capacity { get; set; }
        public int number { get; set; }
        public int? floor { get; set; }
        public Dictionary<int, string> fieldValue { get; set; }
        public void Map(ref AudienceRepl context)
        {
            context.Name = name;
            context.Capacity = capacity;
            context.Number = number;
            context.Floor = floor;
        }
    }

    public class HousingDTO
    {
        public HousingDTO() { }
        public HousingDTO(HousingRepl context)
        {
            id = context.Id;
            name = context.Name;
        }
        public int id { get; set; }
        public string name { get; set; }

    }
}
