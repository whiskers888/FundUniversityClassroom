using System.Dynamic;
using System.Web.Mvc;

namespace Helper
{
    public class BaseController : ControllerBase
    {
        public BaseAppContext Context { get; }
        public BaseController(BaseAppContext context)
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

        protected override void ExecuteCore()
        {
            throw new NotImplementedException();
        }
    }
}

