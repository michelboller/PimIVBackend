using AuthAPI.Context;
using AuthAPI.Models;
using AuthAPI.Models.Dto;
using AuthAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Validator;

namespace AuthAPI.Business
{
    public class UserBusiness : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public UserBusiness(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<UserToken> CreateUser(UserInfo model)
        {
            Guard.Validate(validator =>
            {
                var modelName = nameof(model.Fullname);
                var modelEmail = nameof(model.Email);
                var modelPassword = nameof(model.Password);

                validator
                    .NotNullOrEmptyString(model.Fullname, modelName, $"{modelName} deve possuir um valor")
                    .NotNullOrEmptyString(model.Email, modelEmail, $"{modelEmail} deve possuir um valor")
                    .NotNullOrEmptyString(model.Password, modelPassword, $"{modelPassword} deve possuir um valor")
                    .IsValidEmail(model.Email, modelEmail, $"{modelEmail} informado é inválido")
                    .PasswordHasLowerCharac(model.Password, modelPassword, $"{modelPassword} informada não é válida pois está faltando ao menos um caractere minúsculo")
                    .PasswordHasMiniMaxCharac(model.Password, modelPassword, $"{modelPassword} informada não é válida pois contém menos de 8 caracteres ou mais de 64 caracteres")
                    .PasswordHasNumbers(model.Password, modelPassword, $"{modelPassword} informada não é válida pois está faltando ao menos um caractere de tipo numérico")
                    .PasswordHasSymblos(model.Password, modelPassword, $"{modelPassword} informada não é válida pois está faltando ao menos um caractere simbólico Ex: @")
                    .PasswordHasUpper(model.Password, modelPassword, $"{modelPassword} informada não é válida pois está faltando ao menos um caractere maiúsculo");
            });
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Fullname = model.Fullname };
            //TODO: Não deixar cadastrar pessoas com o mesmo email
            var result = await _userManager.CreateAsync(user, model.Password);

            if(result.Errors.Count() != 0)
            {
                var count = 1;
                var messages = string.Empty;

                result.Errors.ToList().ForEach(x =>
                {
                    messages += $"{count}[{x.Code}] - {x.Description}\n";
                    count++;
                });

                throw new Exception(messages);
            }

            return BuildToken(model);
        }

        public async Task<UserToken> LoginUser(UserLoginDto userInfo)
        {
            Guard.Validate(validator =>
                validator
                    .NotNullOrEmptyString(userInfo.Email, nameof(userInfo.Email), $"{nameof(userInfo.Email)} está nulo ou sem conteúdo, por favor preencha o campo e tente novamente")
            );

            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, false, false); //dois ultimos parametros: se é persistente o login e se é para travar o login em caso de falha
            if (result.Succeeded)
                return BuildToken(new UserInfo { Email = userInfo.Email, Password = userInfo.Password });
            else
                throw new Exception("Login inválido");
        }

        private UserToken BuildToken(UserInfo userInfo)
        {
            var config = _configuration.Get<AppSettings>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("CertificadoDaEmpresa", "123abc"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.JWT.Key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationTime = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: config.JWT.Issuer,
                    audience: config.JWT.Audience,
                    claims: claims,
                    expires: expirationTime,
                    signingCredentials: cred
                );

            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expirationTime
            };
        }

        internal class Config
        {
            public string Key { get; set; }
            public string Issuer { get; set; }
            public string Audience { get; set; }
        }

        internal class AppSettings
        {
            public Config JWT { get; set; }
        }
    }
}
