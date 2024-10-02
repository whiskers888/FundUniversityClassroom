using Service.Audience.Models.EFModels;

namespace Service.Audience.Models.Replicates
{

    public class AudienceRepl(EFAudience context)
    {
        public EFAudience Context = context;
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public HousingRepl Housing
        {
            get => Context.Housing != null ? new HousingRepl(context.Housing) : null;
            set => Context.HousingId = value?.Id;
        }
        public AudienceType AudienceType { get => Context.AudienceType; set => Context.AudienceType = value; }
        public int Capacity { get => Context.Capacity; set => Context.Capacity = value; }
        public int? Floor { get => Context.Floor; set => Context.Floor = value; }
        public int Number { get => Context.Number; set => Context.Number = value; }
        public Dictionary<int, string> FieldValue
        {
            get
            {
                Dictionary<int, string> _fieldValue = new Dictionary<int, string>();
                //if (Context.CustomFieldValues != null)
                foreach (var item in Context.CustomFieldValues.Where(
                    it => it.AudienceId == Context.Id &&
                    it.IsDeleted != true))
                    _fieldValue.Add(item.CustomFieldId, item.Value);
                return _fieldValue;
            }
        }
    }

    public class HousingRepl(EFHousingSummary context)
    {
        public EFHousingSummary Context { get; } = context;

        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; }
    }
}
