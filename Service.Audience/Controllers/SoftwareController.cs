using Microsoft.AspNetCore.Mvc;
using Service.Audience.Context;
using Service.Audience.Models.DTO;
using Service.Common;

namespace Service.Audience.Controllers
{
    public class SoftwareController(AudienceAppContext context) : BaseController(context)
    {

        /// <summary>
        /// Возвращает список всего списка ПО.
        /// </summary>
        [HttpGet("[controller]/[action]")]
        public string GetAll()
        {
            dynamic result = GetCommon();

            result.softwares = context.SoftwareManager.Software.Select(it => new SoftwareOutputDTO(it)).ToArray();
            return Send(true, result);
        }

        [HttpPost("[controller]/[action]")]
        public string Add(SoftwareInputDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                var item = context.SoftwareManager.Create(dto);
                result.softwares = context.SoftwareManager.Software.Select(it => new SoftwareOutputDTO(it)).ToArray();
                return Send(true, result);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка добавления ПО";
                result.error = ex.InnerException;
                return Send(false, result);
            }
        }

        [HttpPut("[controller]/[action]")]
        public string Update(SoftwareInputDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                result.softwares = new SoftwareOutputDTO(context.SoftwareManager.Update(dto));
                return Send(true, result);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка изменения информации о ПО";
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
                bool res = context.SoftwareManager.Delete(id);
                if (!res) NoDeleted.Add(id);
            }
            result.noDeleted = NoDeleted.ToArray();
            result.softwares = context.SoftwareManager.Software.Select(it => new SoftwareOutputDTO(it));

            return Send(true, result);

        }
    }
}
