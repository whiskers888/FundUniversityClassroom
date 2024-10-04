using Microsoft.AspNetCore.Mvc;
using Service.Audience.Context;
using Service.Audience.Models.DTO;
using Service.Common;

namespace Service.Audience.Controllers
{
    public class AudienceFieldController(AudienceAppContext context) : BaseController(context)
    {
        /// <summary>
        /// Возвращает список всех дополнительных полей.
        /// </summary>
        /// <returns>Список дополнительных полей.</returns>
        [HttpGet("[controller]/[action]")]
        public string GetAll()
        {
            dynamic result = GetCommon();

            result.existFields = context.AudFieldManager.Fields.Select(it => new AudFieldDTO(it)).ToArray();
            return Send(true, result);
        }
        /// <summary>
        /// Добавляет новое дополнительное поле.
        /// </summary>
        /// <param name="dto">DTO с информацией о новом дополнительном поле.</param>
        /// <returns>Список дополнительных полей после добавления нового.</returns>
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
        /// <summary>
        /// Обновляет информацию о существующем дополнительном поле.
        /// </summary>
        /// <param name="dto">DTO с обновленной информацией о дополнительном поле.</param>
        /// <returns>Обновленная информация о дополнительном поле.</returns>
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
        /// <summary>
        /// Удаляет дополнительные поля по их идентификаторам.
        /// </summary>
        /// <param name="dtoIds">Массив идентификаторов дополнительных полей для удаления.</param>
        /// <returns>Список дополнительных полей после удаления.</returns>
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
