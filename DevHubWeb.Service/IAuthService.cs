using DevHubWeb.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHubWeb.Service
{
    public interface IAuthService
    {
        Task<UserCreatedModel> Register(UserForRegisterModel user, string password);
        Task<bool> UserExists(string username);
        Task<UserDetailModel> Login(string username, string password);
        string AuthTokenHandler(string Id, string userName, byte role);
        List<KeyValuePair<byte, string>> GetUserRoles();
    }
}
