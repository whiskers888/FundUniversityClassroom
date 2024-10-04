using Service.Audience.Models.EF;
using Service.Audience.Models.Replicates;

namespace Service.Audience.Models.DTO
{

    public class SoftwareInputDTO : SoftwareDTO
    {
        public SoftwareInputDTO() { }
        public int audienceId { get; set; }

        public void Map(ref Software context)
        {
            context.Name = name;
            context.LicenseType = licenseType;
            context.LicenseKey = licenseKey;
            context.LicenseExpirationDate = licenseExpirationDate.Value.ToUniversalTime();
            context.NumberPC = numberPC;
        }
    }

    public class SoftwareOutputDTO : SoftwareDTO
    {
        public SoftwareOutputDTO(Software context)
        {
            id = context.Id;
            name = context.Name;
            licenseType = context.LicenseType;
            licenseKey = context.LicenseKey;
            licenseExpirationDate = context.LicenseExpirationDate;
            numberPC = context.NumberPC;

        }
        public AudienceDTO audience { get; set; }
        public bool isExpired { get; set; }
    }

    public class SoftwareDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public LicenseType licenseType { get; set; }
        public string licenseKey { get; set; }
        public int numberPC { get; set; }
        public DateTime? licenseExpirationDate { get; set; }

    }
}
