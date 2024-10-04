using Microsoft.AspNetCore.Mvc;
using Serivice.Context;
using Service.Common;
using Service.Housing.Models.DTO;

namespace Service.Housing.Controllers
{
    public class HousingController(HousingAppContext context) : BaseController(context)
    {

        [HttpGet("[controller]/[action]")]
        public string GetAll()
        {
            dynamic result = GetCommon();

            result.housings = context.HousingManager.Housings.Select(it => new HousingDTO(it)).ToArray();
            return Send(true, result);
        }

        [HttpPost("[controller]/[action]")]
        public string Add(HousingDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                var item = context.HousingManager.Create(dto);
                result.housings = context.HousingManager.Housings.Select(it => new HousingDTO(it)).ToArray();
                return Send(true, result);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка добавления здания";
                result.error = ex.Message;
                return Send(false, result);
            }
        }

        [HttpPut("[controller]/[action]")]
        public string Update(HousingDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                result.housing = new HousingDTO(context.HousingManager.Update(dto));
                return Send(true, result);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка изменения информации о здании";
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
                bool res = context.HousingManager.Delete(id);
                if (!res) NoDeleted.Add(id);
            }
            result.noDeleted = NoDeleted.ToArray();
            result.housings = context.HousingManager.Housings.Select(it => new HousingDTO(it));

            return Send(true, result);

        }
    }
}
