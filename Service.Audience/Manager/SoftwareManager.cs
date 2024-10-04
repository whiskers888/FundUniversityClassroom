using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Audience.Context;
using Service.Audience.Models.DTO;
using Service.Audience.Models.EF;
using Service.Audience.Models.Replicates;

namespace Service.Audience.Manager
{
    public class SoftwareManager
    {
        public SoftwareManager(AudienceAppContext applicationContext)
        {
            AppContext = applicationContext;

            _dbContext = applicationContext.CreateDbContext();

            _software.Clear();
            Read();
        }
        protected AudienceAppContext AppContext { get; }
        protected DBContext _dbContext { get; }
        private List<Software> _software { get; set; } = new List<Software>();
        public Software[] Software { get => _software.ToArray(); }
        private void Read()
        {
            foreach (EFSoftware item in _dbContext.EFSoftware)
            {
                if (item.IsDeleted != true) _software.Add(new Software(item, AppContext));
            }
        }

        public Software Get(int id) => _software.FirstOrDefault(it => it.Id == id);

        public Software Create(SoftwareInputDTO model)
        {
            EFSoftware entity = new EFSoftware();
            Software software = new Software(entity, AppContext);
            model.Map(ref software);
            software.Audience = AppContext.AudienceManager.Get(model.audienceId);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            _software.Add(software);
            return software;
        }

        public Software Update(SoftwareInputDTO model)
        {
            Software software = _software.FirstOrDefault(it => it.Id == model.id);
            model.Map(ref software);
            EntityEntry<EFSoftware> entity = _dbContext.Entry(software.Context);
            if (entity.State != EntityState.Added)
                entity.State = EntityState.Modified;
            _dbContext.SaveChanges();
            return software;
        }

        public bool Delete(int id)
        {
            Software item = _software.FirstOrDefault(it => it.Id == id);

            item.Context.IsDeleted = true;
            _dbContext.SaveChanges();
            _software.Remove(item);
            return true;
        }
    }
}

