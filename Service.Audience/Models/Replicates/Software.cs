using Service.Audience.Context;
using Service.Audience.Models.EF;

namespace Service.Audience.Models.Replicates
{
    public class Software(EFSoftware _context, AudienceAppContext _appContext)
    {
        public EFSoftware Context = _context;
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public AudienceRepl Audience
        {
            get => _appContext.AudienceManager.Get(Context.AudienceId);
            set => Context.AudienceId = value.Id;
        }
        public LicenseType LicenseType { get => Context.LicenseType; set => Context.LicenseType = value; }
        public string LicenseKey { get => Context.LicenseKey; set => Context.LicenseKey = value; }
        public DateTime? LicenseExpirationDate { get => Context.LicenseExpirationDate; set => Context.LicenseExpirationDate = value; }
        public int NumberPC { get => Context.NumberPC; set => Context.NumberPC = value; }
        public bool IsExpired
        {
            get
            {
                if (LicenseExpirationDate.HasValue)
                    return LicenseExpirationDate.Value < DateTime.Now;
                return false;
            }
        }
    }
}
