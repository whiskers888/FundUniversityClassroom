using AccHousingService.Context;
using AccHousingService.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Data.EFModels;
using Service.Data.Replicats;

namespace AccHousingService.Manager
{
    public class HousingManager
    {
        public HousingManager(HousingAppContext applicationContext)
        {
            AppContext = applicationContext;

            DBContext = applicationContext.CreateDbContext();
            _housings.Clear();
            Read();
        }
        protected HousingAppContext AppContext { get; }
        protected DBContext DBContext { get; }
        private List<Housing> _housings { get; set; } = new List<Housing>();
        public Housing[] Housings { get => _housings.ToArray(); }

        private void Read()
        {
            foreach (EFHousing item in DBContext.EFHousing)
            {
                if (item.IsDeleted != true) _housings.Add(new Housing(item));
            }
        }

        public Housing Get(int id) => _housings[id];

        public Housing Create(HousingDTO model)
        {
            EFHousing entity = new EFHousing();
            Housing housing = new Housing(entity);
            model.Map(ref housing);

            DBContext.Add(entity);
            DBContext.SaveChanges();

            _housings.Add(housing);

            return housing;
        }

        public Housing Update(HousingDTO model)
        {
            Housing housing = _housings.FirstOrDefault(it => it.Id == model.id);
            model.Map(ref housing);
            EntityEntry<EFHousing> entity = DBContext.Entry(housing.Context);
            if (entity.State != EntityState.Added)
                entity.State = EntityState.Modified;
            DBContext.SaveChanges();
            return housing;
        }

        public bool Delete(int id)
        {
            Housing item = _housings.FirstOrDefault(it => it.Id == id);
            try
            {
                item.Context.IsDeleted = true;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            _housings.Remove(item);
            return true;
        }
    }

}
