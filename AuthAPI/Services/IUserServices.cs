using AuthAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Services
{
    public interface IUserServices
    {
        Task<UserToken> CreateUser(UserInfo model);
        Task<UserToken> LoginUser(UserInfo userInfo);
    }
}
