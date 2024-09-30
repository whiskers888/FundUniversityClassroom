using Service.Common.ModelExtensions;
using System.ComponentModel.DataAnnotations;


namespace Service.Housing.Models.EF
{
    public class EFHousing : EFBaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public int? Floor { get; set; }
    }
}
