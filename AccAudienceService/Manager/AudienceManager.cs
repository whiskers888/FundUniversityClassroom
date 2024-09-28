
using AccAudienceService.Models;
using AccHousingService.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AccHousingService.Manager
{
    public class AudienceManager
    {
        public AudienceManager(AppContext applicationContext)
        {
            AppContext = applicationContext;

            DBContext = applicationContext.CreateDbContext();
            _audiences.Clear();
            Read();
        }
        protected AppContext AppContext { get; }
        protected DBContext DBContext { get; }
        private List<Audience> _audiences { get; set; } = new List<Audience>();
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
                DBContext.Remove(item.Context);
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
    public class Audience(EFAudience context)
    {
        internal EFAudience Context { get; set; } = context;
        public int Id { get => Context.Id; }
        public string Name { get => Context.Name; set => Context.Name = value; }
        public int HousingId { get => Context.HousingId; set => Context.HousingId = value; }
        public AudienceType AudienceType { get => Context.AudienceType; set => Context.AudienceType = value; }
        public int Capacity { get => Context.Capacity; set => Context.Capacity = value; }
        public int? Floor { get => Context.Floor; set => Context.Floor = value; }
        public int Number { get => Context.Number; set => Context.Number = value; }
    }

}
