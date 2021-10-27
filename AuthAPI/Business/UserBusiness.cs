using AuthAPI.Context;
using AuthAPI.Models;
using AuthAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
                var modelName = nameof(model.Name);
                var modelEmail = nameof(model.Email);
                var modelPassword = nameof(model.Password);

                validator
                    .NotNullOrEmptyString(model.Name, modelName, $"{modelName} deve possuir um valor")
                    .HasSpaces(model.Name, modelName, $"{modelName} informado não deve possuir espaços em branco")
                    .NotNullOrEmptyString(model.Email, modelEmail, $"{modelEmail} deve possuir um valor")
                    .NotNullOrEmptyString(model.Password, modelPassword, $"{modelPassword} deve possuir um valor")
                    .IsValidEmail(model.Email, modelEmail, $"{modelEmail} informado é inválido")
                    .PasswordHasLowerCharac(model.Password, modelPassword, $"{modelPassword} informada não é válida pois está faltando ao menos um caractere minúsculo")
                    .PasswordHasMiniMaxCharac(model.Password, modelPassword, $"{modelPassword} informada não é válida pois contém menos de 8 caracteres ou mais de 64 caracteres")
                    .PasswordHasNumbers(model.Password, modelPassword, $"{modelPassword} informada não é válida pois está faltando ao menos um caractere de tipo numérico")
                    .PasswordHasSymblos(model.Password, modelPassword, $"{modelPassword} informada não é válida pois está faltando ao menos um caractere simbólico Ex: @")
                    .PasswordHasUpper(model.Password, modelPassword, $"{modelPassword} informada não é válida pois está faltando ao menos um caractere maiúsculo");
            });
            var user = new ApplicationUser { UserName = model.Name, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            //if (result.Succeeded)
            return BuildToken(model);
            //else
            //{
            //    var message = string.Empty;
            //    var errorCount = 1;
            //    message += "Falha na criação do usuário. Erro(s):\n";

            //    foreach (var erro in result.Errors)
            //    {
            //        message += $"{errorCount} - {erro.Code}: {erro.Description}\n";
            //        errorCount++;
            //    }

            //    throw new Exception(message);
            //}
        }

        public async Task<UserToken> LoginUser(UserInfo userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Name, userInfo.Password, false, false); //dois ultimos parametros: se é persistente o login e se é para travar o login em caso de falha

            if (result.Succeeded)
                return BuildToken(userInfo);
            else
                throw new Exception("Login inválido");
        }

        private UserToken BuildToken(UserInfo userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("CertificadoDaEmpresa", "123abc"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationTime = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
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
    }
}
