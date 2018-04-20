using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Data.Repo
{
    public interface IAuthRepository
    {
        Task<UserCreatedModel> Register(UserForRegisterModel user, string password);
        Task<bool> UserExists(string username);
        Task<Users> Login(string username, string password);
        List<KeyValuePair<byte, string>> GetUserRoles();
    }
}
