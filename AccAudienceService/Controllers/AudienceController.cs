using AccHousingService.DTO;
using AccHousingService.Manager;
using Helper;
using Microsoft.AspNetCore.Mvc;

namespace AccHousingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AudienceController(AppContext context) : BaseController(context)
    {

        [HttpGet("[controller]/[action]")]
        public string GetAll()
        {
            dynamic result = GetCommon();

            result.housings = context.AudienceManager.Audiences.Select(it => new AudienceDTO(it)).ToArray();
            return Send(true, result);
        }

        [HttpPost("[controller]/[action]")]
        public string Add(AudienceDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                var item = context.AudienceManager.Create(dto);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка добавления здания";
                result.error = ex.Message;
                return Send(false, result);
            }


            result.housings = context.AudienceManager.Audiences.Select(it => new AudienceDTO(it)).ToArray();

            return Send(true, result);
        }

        [HttpPut("[controller]/[action]")]
        public string Update(AudienceDTO dto)
        {
            dynamic result = GetCommon();
            Audience item;
            try
            {
                item = context.AudienceManager.Update(dto);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка изменения информации о здании";
                result.error = ex.Message;
                return Send(false, result);
            }
            result.housing = new AudienceDTO(item);
            return Send(true, result);
        }
        [HttpDelete("[controller]/[action]")]
        public string Delete(int[] dtoIds)
        {
            dynamic result = GetCommon();
            List<int> NoDeleted = new List<int>();
            foreach (int id in dtoIds)
            {
                bool res = context.AudienceManager.Delete(id);
                if (!res) NoDeleted.Add(id);
            }
            result.noDeleted = NoDeleted.ToArray();
            result.housings = context.AudienceManager.Audiences.Select(it => new AudienceDTO(it));

            return Send(true, result);

        }
    }
}
