using AccHousingService.DTO;
using AccHousingService.Manager;
using Helper;
using Microsoft.AspNetCore.Mvc;

namespace AccHousingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HousingController(AppContext context) : BaseController(context)
    {

        [HttpGet("[controller]/[action]")]
        public string GetAll()
        {
            dynamic result = GetCommon();
            Housing[] housings = context.HousingManager.Housings;

            result.housings = housings.Select(it => new HousingDTO(it)).ToArray();
            return Send(true, result);
        }

        [HttpPost("[controller]/[action]")]
        public string Add(HousingDTO dto)
        {
            dynamic result = GetCommon();
            try
            {
                var item = context.HousingManager.Create(dto);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка добавления здания";
                result.error = ex.Message;
                return Send(false, result);
            }


            result.housings = context.HousingManager.Housings.Select(it => new HousingDTO(it)).ToArray();

            return Send(true, result);
        }

        [HttpPut("[controller]/[action]")]
        public string Update(HousingDTO dto)
        {
            dynamic result = GetCommon();
            Housing item;
            try
            {
                item = context.HousingManager.Update(dto);
            }
            catch (Exception ex)
            {
                result.msg = "Ошибка изменения информации о здании";
                result.error = ex.Message;
                return Send(false, result);
            }
            result.housing = new HousingDTO(item);
            return Send(true, result);
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
