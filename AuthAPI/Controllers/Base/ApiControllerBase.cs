using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Validator;

namespace AuthAPI.Controllers.Base
{
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult ThrowException(Exception ex)
        {
            if (ex is GuardException validations)
                return StatusCode(400, string.Join("\n", validations.Errors.Select(x => x.Message).ToArray()));

            if (ex is ApplicationException e)
                return StatusCode(400, e.Message);

            return StatusCode(500, ex.ToString());
        }
    }
}
