using AuthAPI.Context;
using AuthAPI.Controllers.Base;
using AuthAPI.Models;
using AuthAPI.Models.Dto;
using AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly AppDbContext _context;

        public UserController(IUserServices userServices, AppDbContext context)
        {
            _userServices = userServices;
            _context = context;
        }

        //TODO: tirar o retoro dos usuarios
        [HttpGet("")]
        public async Task<ActionResult<string>> Get()
        {
            //return await _context.Users.ToListAsync();
            return "Controlador de login";
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
        public async Task<ActionResult<UserToken>> LoginUser([FromBody] UserLoginDto userInfo)
        {
            return await _userServices.LoginUser(userInfo);
        }

    }
}
