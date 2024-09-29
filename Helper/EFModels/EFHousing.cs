using Helper.EFModels;
using System.ComponentModel.DataAnnotations;

namespace Helper.Models
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
