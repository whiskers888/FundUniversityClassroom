using Helper.Replicats;

namespace AccHousingService.DTO
{
    public class HousingDTO
    {
        public HousingDTO() { }
        public HousingDTO(Housing context)
        {
            id = context.Id;
            name = context.Name;
            address = context.Address;
            floor = context.Floor;
        }
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int? floor { get; set; }
        public void Map(ref Housing context)
        {
            context.Name = name;
            context.Address = address;
            context.Floor = floor;
        }
    }
}
