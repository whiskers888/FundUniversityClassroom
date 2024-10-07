using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Serivice.Context;
using Service.Common.ModelExtensions;
using Service.Housing.Context;
using Service.Housing.Models.DTO;
using Service.Housing.Models.EF;
using Service.Housing.Models.Replicates;

namespace Service.Housing.Manager
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
        private List<HousingRepl> _housings { get; set; } = new List<HousingRepl>();
        public HousingRepl[] Housings { get => _housings.ToArray(); }
        private void Read()
        {
            foreach (EFHousing item in DBContext.EFHousing)
            {
                if (item.IsDeleted != true) _housings.Add(new HousingRepl(item));
            }
        }

        public HousingRepl Get(int id) => _housings[id];

        public HousingRepl Create(HousingDTO model)
        {
            EFHousing entity = new EFHousing();
            HousingRepl housing = new HousingRepl(entity);
            model.Map(ref housing);

            DBContext.Add(entity);
            DBContext.SaveChanges();

            _housings.Add(housing);

            AppContext.HousingPublisher.PublishHousingMessage("housing.add", new HousingMessage() { Id = housing.Id, Name = housing.Name });

            return housing;
        }

        public HousingRepl Update(HousingDTO model)
        {
            HousingRepl? housing = _housings.FirstOrDefault(it => it.Id == model.id);
            if (housing == null) throw new ArgumentException("Такой модели не существует!"); ;
            model.Map(ref housing);
            EntityEntry<EFHousing> entity = DBContext.Entry(housing.Context);
            if (entity.State != EntityState.Added)
                entity.State = EntityState.Modified;
            DBContext.SaveChanges();

            AppContext.HousingPublisher.PublishHousingMessage("housing.update", new HousingMessage() { Id = housing.Id, Name = housing.Name });
            return housing;
        }

        public bool Delete(int id)
        {
            HousingRepl? housing = _housings.FirstOrDefault(it => it.Id == id);
            if (housing == null) throw new ArgumentException("Такой модели не существует!"); ;
            try
            {
                housing.Context.IsDeleted = true;
                DBContext.SaveChanges();

                AppContext.HousingPublisher.PublishHousingMessage("housing.delete", new HousingMessage() { Id = housing.Id });
            }
            catch (Exception ex)
            {

                AppContext.Logger.LogError(ex.InnerException?.Message);
                return false;
            }
            _housings.Remove(housing);
            return true;
        }
    }

}
