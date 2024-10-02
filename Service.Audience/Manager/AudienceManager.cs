using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Audience.Context;
using Service.Audience.Models.DTO;
using Service.Audience.Models.EF;
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
                if (item.IsDeleted != true) _audiences.Add(new AudienceRepl(item, AppContext));
            }
        }

        public AudienceRepl Get(int id) => _audiences.FirstOrDefault(it => it.Id == id);

        public AudienceRepl Create(AudienceDTO model)
        {
            EFAudience entity = new EFAudience();
            AudienceRepl audience = new AudienceRepl(entity, AppContext);
            model.Map(ref audience);
            EFHousingSummary entityHousing = _dbContext.EFHousingSummary.FirstOrDefault(h => h.Id == model.housing.id);

            if (entityHousing != null)
                audience.Housing = new HousingRepl(entityHousing);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            CreateFieldValue(model.fieldValue, entity.Id);

            _audiences.Add(audience);

            return audience;
        }
        private void CreateFieldValue(Dictionary<int, string> keyValue, int audId)
        {
            foreach (var item in keyValue)
            {
                if (!_dbContext.EFAudCustomFieldsValues.Any(f => f.AudienceId == audId && f.CustomFieldId == item.Key))
                {
                    EFAudValue entity = new EFAudValue()
                    {
                        AudienceId = audId,
                        CustomFieldId = item.Key,
                        Value = item.Value
                    };
                    _dbContext.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }
        public AudienceRepl Update(AudienceDTO model)
        {
            AudienceRepl audience = _audiences.FirstOrDefault(it => it.Id == model.id);
            model.Map(ref audience);
            EntityEntry<EFAudience> entity = _dbContext.Entry(audience.Context);
            if (entity.State != EntityState.Added)
                entity.State = EntityState.Modified;
            UpdateFieldValue(model.fieldValue, audience.Id);
            _dbContext.SaveChanges();
            return audience;
        }
        private void UpdateFieldValue(Dictionary<int, string> keyValue, int audId)
        {
            foreach (var item in keyValue)
            {
                EFAudValue entity = _dbContext.EFAudCustomFieldsValues.FirstOrDefault(it => it.CustomFieldId == item.Key && it.AudienceId == audId);
                if (entity != null)
                {
                    if (item.Value == "")
                        ;
                    entity.Value = item.Value;
                    EntityEntry<EFAudValue> entityEntity = _dbContext.Entry(entity);
                    if (entityEntity.State != EntityState.Added)
                        entityEntity.State = EntityState.Modified;
                }
            }
        }

        public bool Delete(int id)
        {
            AudienceRepl item = _audiences.FirstOrDefault(it => it.Id == id);
            try
            {
                item.Context.IsDeleted = true;
                foreach (var fieldvalue in item.FieldValue)
                {
                    DeleteFieldValue(fieldvalue.Key, item.Id);
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            _audiences.Remove(item);
            return true;
        }

        private bool DeleteFieldValue(int fieldId, int audId)
        {
            _dbContext.Remove(_dbContext.EFAudCustomFieldsValues.FirstOrDefault(it => it.CustomFieldId == fieldId && it.AudienceId == audId));
            _dbContext.SaveChanges();
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
