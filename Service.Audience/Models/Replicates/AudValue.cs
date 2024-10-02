using Service.Audience.Context;
using Service.Audience.Models.EF;

namespace Service.Audience.Models.Replicates
{

    public class AudValue(EFAudValue context, AudienceAppContext _appContext)
    {
        public EFAudValue Context { get; set; } = context;
        public int Id { get => Context.Id; }
        public AudienceRepl Audience
        {
            get => _appContext.AudienceManager.Get(Context.AudienceId);
            set
            {
                Context.AudienceId = value.Id;
                Context.Audience = value.Context;
            }
        }
        public AudField Field { get => _appContext.AudFieldManager.Get(Context.CustomFieldId); set { } }
        public string Value { get => Context.Value; set => Context.Value = value; }
    }
}
