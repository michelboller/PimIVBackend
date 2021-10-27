using AuthAPI.Controllers.Base;
using AuthAPI.Models;
using AuthAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        [HttpGet("")]
        public ActionResult<string> Get()
        {
            return "Controlador UserController :: AuthApi";
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserInfo model)
        {
            try
            {
                return Ok(await _userServices.CreateUser(model));
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> LoginUser([FromBody] UserInfo userInfo)
        {
            return await _userServices.LoginUser(userInfo);
        }

    }
}
