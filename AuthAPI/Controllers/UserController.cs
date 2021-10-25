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
    public class UserController
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
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            return await _userServices.CreateUser(model);
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> LoginUser([FromBody] UserInfo userInfo)
        {
            return await _userServices.LoginUser(userInfo);
        }

    }
}
