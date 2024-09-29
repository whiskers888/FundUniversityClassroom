using Service.Data.EFModels;

namespace Service.Data.Replicats
{
    public class Housing(EFHousing context)
    {
        public EFHousing Context { get; set; } = context;
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public string Address { get => Context.Address; set => Context.Address = value; }
        public int? Floor { get => Context.Floor; set => Context.Floor = value; }
    }
}
