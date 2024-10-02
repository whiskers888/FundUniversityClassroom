using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Audience.Context;
using Service.Audience.Models.DTO;
using Service.Audience.Models.EF;
using Service.Audience.Models.Replicates;

namespace Service.Audience.Manager
{
    public class AudFieldManager
    {
        public AudFieldManager(AudienceAppContext applicationContext)
        {
            AppContext = applicationContext;
            _dbContext = applicationContext.CreateDbContext();
            _fields.Clear();

            Read();
        }

        protected AudienceAppContext AppContext { get; }
        protected DBContext _dbContext { get; }
        private List<AudField> _fields { get; set; } = new List<AudField>();
        public AudField[] Fields { get => _fields.ToArray(); }

        private void Read()
        {
            foreach (EFAudField item in _dbContext.EFAudCustomFields)
            {
                if (item.IsDeleted != true) _fields.Add(new AudField(item));
            }
        }

        public AudField Get(int id) => _fields.FirstOrDefault(it => it.Id == id);

        public AudField Create(AudFieldDTO model)
        {
            EFAudField entity = new EFAudField();
            AudField field = new AudField(entity);

            model.Map(ref field);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            _fields.Add(field);
            return field;
        }

        public AudField Update(AudFieldDTO model)
        {
            AudField field = _fields.FirstOrDefault(it => it.Id == model.id);

            model.Map(ref field);

            EntityEntry<EFAudField> entity = _dbContext.Entry(field.Context);
            if (entity.State != EntityState.Added)
                entity.State = EntityState.Modified;
            _dbContext.SaveChanges();

            return field;
        }

        public bool Delete(int id)
        {
            AudField item = _fields.FirstOrDefault(it => it.Id == id);
            try
            {
                item.Context.IsDeleted = true;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            _fields.Remove(item);
            return true;
        }
    }
}
