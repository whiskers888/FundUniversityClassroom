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

            DBContext = applicationContext.CreateDbContext();

            _audiences.Clear();
            Read();
        }
        protected AudienceAppContext AppContext { get; }
        protected DBContext DBContext { get; }
        private List<AudienceRepl> _audiences { get; set; } = new List<AudienceRepl>();
        public AudienceRepl[] Audiences { get => _audiences.ToArray(); }
        private void Read()
        {
            foreach (EFAudience item in DBContext.EFAudiences)
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

            DBContext.Add(entity);
            DBContext.SaveChanges();

            _audiences.Add(audience);

            return audience;
        }

        public AudienceRepl Update(AudienceDTO model)
        {
            AudienceRepl audience = _audiences.FirstOrDefault(it => it.Id == model.id);
            model.Map(ref audience);
            EntityEntry<EFAudience> entity = DBContext.Entry(audience.Context);
            if (entity.State != EntityState.Added)
                entity.State = EntityState.Modified;
            DBContext.SaveChanges();
            return audience;
        }

        public bool Delete(int id)
        {
            AudienceRepl item = _audiences.FirstOrDefault(it => it.Id == id);
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
