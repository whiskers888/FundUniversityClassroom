using Microsoft.AspNetCore.Mvc;
using Service.Audience.Context;
using Service.Audience.Models.DTO;
using Service.Common;

namespace Service.Audience.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AudienceFieldController(AudienceAppContext context) : BaseController(context)
    {

        [HttpGet("[controller]/[action]")]
        public string GetAll()
        {
            dynamic result = GetCommon();

            result.existFields = context.AudFieldManager.Fields.Select(it => new AudFieldDTO(it)).ToArray();
            return Send(true, result);
        }

        [HttpPost("[controller]/[action]")]
        public string Add(AudFieldDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                var item = context.AudFieldManager.Create(dto);
                result.fields = context.AudFieldManager.Fields.Select(it => new AudFieldDTO(it)).ToArray();
                return Send(true, result);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка добавления дополнительного поля";
                result.error = ex.Message;
                return Send(false, result);
            }
        }

        [HttpPut("[controller]/[action]")]
        public string Update(AudFieldDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                result.fields = new AudFieldDTO(context.AudFieldManager.Update(dto));
                return Send(true, result);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка изменения дополнительного поля";
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
                bool res = context.AudFieldManager.Delete(id);
                if (!res) NoDeleted.Add(id);
            }
            result.noDeleted = NoDeleted.ToArray();
            result.fields = context.AudFieldManager.Fields.Select(it => new AudFieldDTO(it));

            return Send(true, result);

        }
    }
}
