using Microsoft.AspNetCore.Mvc;
using Service.Common.Extensions;
using Service.Common.ModelExtensions;
using System.Dynamic;

namespace Service.Common
{
    public class BaseController : ControllerBase
    {
        public IAppContext Context { get; }
        public BaseController(IAppContext context)
        {
            Context = context;
        }

        protected dynamic GetCommon()
        {
            dynamic o = new ExpandoObject();
            o.Title = Context.Title;
            return o;
        }

        protected string Send(bool res, object result = null)
        {
            return JsonHelper.Serialize(new AnswerDTO()
            {
                result = res,
                data = result,
            });
        }
    }
}

