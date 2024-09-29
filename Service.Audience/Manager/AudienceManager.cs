
using AccAudienceService.Context;
using AccAudienceService.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Data.EFModels;
using Service.Data.Replicats;

namespace AccAudienceService.Manager
{
    public class AudienceManager
    {
        public AudienceManager(AudienceAppContext applicationContext)
        {
            AppContext = applicationContext;

            DBContext = applicationContext.CreateDbContext();
            _audiences.Clear();
            _housing.Clear();
            Read();
        }
        protected AudienceAppContext AppContext { get; }
        protected DBContext DBContext { get; }
        private List<Audience> _audiences { get; set; } = new List<Audience>();
        private List<Housing> _housing { get; set; } = new List<Housing>();
        public Audience[] Audiences { get => _audiences.ToArray(); }

        private void Read()
        {
            foreach (EFAudience item in DBContext.EFAudiences)
            {
                if (item.IsDeleted != true) _audiences.Add(new Audience(item));
            }
        }

        public Audience Get(int id) => _audiences[id];

        public Audience Create(AudienceDTO model)
        {
            EFAudience entity = new EFAudience();
            Audience audience = new Audience(entity);
            model.Map(ref audience);

            DBContext.Add(entity);
            DBContext.SaveChanges();

            _audiences.Add(audience);

            return audience;
        }

        public Audience Update(AudienceDTO model)
        {
            Audience audience = _audiences.FirstOrDefault(it => it.Id == model.id);
            model.Map(ref audience);
            EntityEntry<EFAudience> entity = DBContext.Entry(audience.Context);
            if (entity.State != EntityState.Added)
                entity.State = EntityState.Modified;
            DBContext.SaveChanges();
            return audience;
        }

        public bool Delete(int id)
        {
            Audience item = _audiences.FirstOrDefault(it => it.Id == id);
            try
            {
                item.Context.IsDeleted = true;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            _audiences.Remove(item);
            return true;
        }
    }

}
