using Helper.Models;

namespace Helper.Replicats
{

    public class Audience(EFAudience context)
    {
        public EFAudience Context { get; set; } = context;
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public int HousingId { get => Context.HousingId; set => Context.HousingId = value; }
        public AudienceType AudienceType { get => Context.AudienceType; set => Context.AudienceType = value; }
        public int Capacity { get => Context.Capacity; set => Context.Capacity = value; }
        public int? Floor { get => Context.Floor; set => Context.Floor = value; }
        public int Number { get => Context.Number; set => Context.Number = value; }
    }
}
