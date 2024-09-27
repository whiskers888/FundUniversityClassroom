using AccHousingService.Models;

namespace AccHousingService
{
    public class HousingManager(AppContext applicationContext)
    {

        protected AppContext AppContext { get; set; } = applicationContext;
        protected DBContext DBContext { get; set; } = applicationContext.CreateDbContext();
        private List<Housing> _housings { get; set; } = new List<Housing>();

        public Housing[] Housings { get => _housings.ToArray(); }
    }
    public class Housing(EFHousing context)
    {
        private EFHousing Context { get; set; } = context;
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
    }

}
