using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Audience.Context;
using Service.Audience.Models.DTO;
using Service.Audience.Models.EFModels;
using Service.Audience.Models.Replicates;

namespace Service.Audience.Manager
{

    public class AudienceManager
    {

        public AudienceManager(AudienceAppContext applicationContext)
        {
            AppContext = applicationContext;

            _dbContext = applicationContext.CreateDbContext();

            _audiences.Clear();
            Read();
        }
        protected AudienceAppContext AppContext { get; }
        protected DBContext _dbContext { get; }
        private List<AudienceRepl> _audiences { get; set; } = new List<AudienceRepl>();
        public AudienceRepl[] Audiences { get => _audiences.ToArray(); }
        private void Read()
        {
            foreach (EFAudience item in _dbContext.EFAudiences)
            {
                if (item.IsDeleted != true) _audiences.Add(new AudienceRepl(item));
            }
        }

        public AudienceRepl Get(int id) => _audiences[id];

        public AudienceRepl Create(AudienceDTO model)
        {
            EFAudience entity = new EFAudience();
            AudienceRepl audience = new AudienceRepl(entity);
            model.Map(ref audience);
            EFHousingSummary entityHousing = _dbContext.EFHousingSummary.FirstOrDefault(h => h.Id == model.housing.id);

            if (entityHousing != null)
                audience.Housing = new HousingRepl(entityHousing);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            _audiences.Add(audience);

            return audience;
        }

        public AudienceRepl Update(AudienceDTO model)
        {
            AudienceRepl audience = _audiences.FirstOrDefault(it => it.Id == model.id);
            model.Map(ref audience);
            EntityEntry<EFAudience> entity = _dbContext.Entry(audience.Context);
            if (entity.State != EntityState.Added)
                entity.State = EntityState.Modified;
            _dbContext.SaveChanges();
            return audience;
        }

        public bool Delete(int id)
        {
            AudienceRepl item = _audiences.FirstOrDefault(it => it.Id == id);
            try
            {
                item.Context.IsDeleted = true;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            _audiences.Remove(item);
            return true;
        }


        public AudienceRepl Bind(int housingId, int audienceId)
        {
            AudienceRepl item = _audiences.FirstOrDefault(it => it.Id == audienceId);
            EFHousingSummary entityHousing = _dbContext.EFHousingSummary.FirstOrDefault(it => it.Id == housingId);
            item.Housing = new HousingRepl(entityHousing);
            _dbContext.SaveChanges();
            return item;
        }

        public AudienceRepl Unbind(int audienceId)
        {
            AudienceRepl item = _audiences.FirstOrDefault(it => it.Id == audienceId);
            item.Housing = null;
            _dbContext.SaveChanges();
            return item;
        }
    }

}
