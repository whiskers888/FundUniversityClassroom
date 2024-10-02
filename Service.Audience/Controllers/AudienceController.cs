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
        public string Initialize()
        {
            dynamic result = GetCommon();

            result.avaliableFields = context.AudFieldManager.Fields.Select(it => new AudFieldDTO(it)).ToArray();
            return Send(true, result);
        }

        [HttpGet("[controller]/[action]")]
        public string GetAll()
        {
            dynamic result = GetCommon();

            result.audiences = context.AudienceManager.Audiences.Select(it => new AudienceDTO(it)).ToArray();
            return Send(true, result);
        }
        /// <summary>
        /// Добавляет аудиторию
        /// </summary>
        /// <remarks>
        /// Обратите внимание, что этот метод принимает HousingDTO, но он не обязателен, 
        /// есть возможность позже привязаться через метод Bind.
        /// </remarks>
        /// <returns>Список аудиторий.</returns>
        [HttpPost("[controller]/[action]")]
        public string Add(AudienceDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                var item = context.AudienceManager.Create(dto);
                result.audiences = context.AudienceManager.Audiences.Select(it => new AudienceDTO(it)).ToArray();
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
                result.audiences = new AudienceDTO(context.AudienceManager.Update(dto));
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
            result.audiences = context.AudienceManager.Audiences.Select(it => new AudienceDTO(it));

            return Send(true, result);

        }


        /// <summary>
        /// Добавляет аудиторию
        /// </summary>
        /// <remarks>
        /// В случае если нужно список аудиторий привязать за зданием.
        /// </remarks>
        /// <returns>Список аудиторий.</returns>
        [HttpPost("[controller]/[action]")]
        public string Bind(int housingId, int audienceId)
        {
            dynamic result = GetCommon();
            var item = context.AudienceManager.Bind(housingId, audienceId);

            result.audience = new AudienceDTO(item);

            return Send(true, result);

        }
        [HttpPost("[controller]/[action]")]
        public string Unbind(int audienceId)
        {
            dynamic result = GetCommon();
            var item = context.AudienceManager.Unbind(audienceId);

            return GetAll();

        }
    }
}
