using Microsoft.AspNetCore.Mvc;
using Service.Audience.Context;
using Service.Audience.Models.DTO;
using Service.Common;

namespace Service.Audience.Controllers
{
    public class AudienceController(AudienceAppContext context) : BaseController(context)
    {
        /// <summary>
        /// Инициализирует приложение и возвращает доступные поля.
        /// </summary>
        /// <returns>Объект с доступными полями.</returns>
        [HttpGet("[controller]/[action]")]
        public string Initialize()
        {
            dynamic result = GetCommon();

            result.avaliableFields = context.AudFieldManager.Fields.Select(it => new AudFieldDTO(it)).ToArray();
            return Send(true, result);
        }
        /// <summary>
        /// Возвращает список всех аудиторий.
        /// </summary>
        /// <returns>Список аудиторий.</returns>
        [HttpGet("[controller]/[action]")]
        public string GetAll()
        {
            dynamic result = GetCommon();

            result.audiences = context.AudienceManager.Audiences.Select(it => new AudienceDTO(it)).ToArray();
            return Send(true, result);
        }
        /// <summary>
        /// Добавляет новую аудиторию.
        /// </summary>
        /// <remarks>
        /// Обратите внимание, что этот метод принимает HousingDTO, но он не обязателен, 
        /// есть возможность позже привязаться через метод Bind.
        /// </remarks>
        /// <param name="dto">DTO с информацией о новой аудитории.</param>
        /// <returns>Список всех аудиторий с учетом добавления новой.</returns>
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
        /// <summary>
        /// Обновляет информацию о существующей аудитории.
        /// </summary>
        /// <param name="dto">DTO с обновленной информацией о аудитории.</param>
        /// <returns>Обновленная информация о аудитории.</returns>
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
        /// <summary>
        /// Удаляет аудитории по их идентификаторам.
        /// </summary>
        /// <param name="dtoIds">Массив идентификаторов аудиторий для удаления.</param>
        /// <returns>Список аудиторий после удаления.</returns>
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
        /// Привязывает аудиторию к зданию.
        /// </summary>
        /// <param name="housingId">Идентификатор здания.</param>
        /// <param name="audienceId">Идентификатор аудитории.</param>
        /// <returns>Информация о привязанной аудитории.</returns>
        [HttpPost("[controller]/[action]")]
        public string Bind(int housingId, int audienceId)
        {
            dynamic result = GetCommon();
            var item = context.AudienceManager.Bind(housingId, audienceId);

            result.audience = new AudienceDTO(item);

            return Send(true, result);

        }
        /// <summary>
        /// Отвязывает аудиторию от здания.
        /// </summary>
        /// <param name="audienceId">Идентификатор аудитории.</param>
        /// <returns>Список всех аудиторий.</returns>
        [HttpPost("[controller]/[action]")]
        public string Unbind(int audienceId)
        {
            dynamic result = GetCommon();
            var item = context.AudienceManager.Unbind(audienceId);

            return GetAll();

        }
    }
}
