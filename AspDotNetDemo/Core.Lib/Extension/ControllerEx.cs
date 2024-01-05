using Clio.Demo.Core.Lib.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Clio.Demo.Core.Lib.Extension
{
    public static class ControllerEx
    {
        public static OkObjectResult Success(this ControllerBase controller, object data, string context)
        {
            OkObjectResult result = controller.Ok(data);

            string count = null;

            if (data is IList list)
            {
                count = $"{list.Count} items";
            }
            Log.Info(controller, $"{context} {count}, code '{result.StatusCode}'");
            return result;
        }
        public static BadRequestResult Failed(this ControllerBase controller, string context, string error)
        {
            BadRequestResult result = controller.BadRequest();

            Log.Info(controller, $"{context} failed, code '{result.StatusCode}' ({error})");
            return result;
        }
    }
}
