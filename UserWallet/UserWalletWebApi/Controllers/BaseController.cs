using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace UserWalletWebApi.Controllers
{
    public class BaseController : Controller
    {
        protected JsonResult ErrorResponse(string message = "Bad response")
        {
            return Json(new
            {
                Success = false,
                Result = message
            });
        }

        protected JsonResult MessageResult(object result, bool success = true)
        {
            return Json(new
            {
                Success = success,
                Result = result
            });
        }

        protected JsonResult MultipleResult(IEnumerable<object> result, bool success = true)
        {
            return Json(new
            {
                Success = success,
                Result = result
            });
        }
    }
}