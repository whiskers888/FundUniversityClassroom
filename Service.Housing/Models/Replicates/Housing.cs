using Service.Housing.Models.EF;

namespace Service.Housing.Models.Replicates
{
    public class HousingRepl(EFHousing context)
    {
        public EFHousing Context { get; set; } = context;
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public string Address { get => Context.Address; set => Context.Address = value; }
        public int? Floor { get => Context.Floor; set => Context.Floor = value; }
    }
}
