using Service.Audience.Models.EFModels;

namespace Service.Audience.Models.Replicates
{

    public class AudienceRepl(EFAudience context)
    {
        public EFAudience Context { get; set; } = context;
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public HousingRepl Housing
        {
            get => new HousingRepl(context.Housing);
            set
            {
                Context.HousingId = value?.Id;
                Context.Housing = value?.Context;
            }
        }
        public AudienceType AudienceType { get => Context.AudienceType; set => Context.AudienceType = value; }
        public int Capacity { get => Context.Capacity; set => Context.Capacity = value; }
        public int? Floor { get => Context.Floor; set => Context.Floor = value; }
        public int Number { get => Context.Number; set => Context.Number = value; }
    }

    public class HousingRepl(EFHousingSummary context)
    {
        public EFHousingSummary Context { get; } = context;

        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; }
    }
}
