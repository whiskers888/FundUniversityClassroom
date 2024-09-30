using Microsoft.AspNetCore.Mvc;
using Service.Audience.Context;
using Service.Audience.Models.DTO;
using Service.Common;

namespace Service.Audience.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AudienceController(AudienceAppContext context) : BaseController(context)
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
                result.housings = context.AudienceManager.Audiences.Select(it => new AudienceDTO(it)).ToArray();
                return Send(true, result);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка добавления аудитории";
                result.error = ex.Message;
                return Send(false, result);
            }
        }

        [HttpPut("[controller]/[action]")]
        public string Update(AudienceDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                result.housing = new AudienceDTO(context.AudienceManager.Update(dto));
                return Send(true, result);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка изменения информации о аудитории";
                result.error = ex.Message;
                return Send(false, result);
            }
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
