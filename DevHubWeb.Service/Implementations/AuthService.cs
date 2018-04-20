using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevHubWeb.Data.Repo;
using DevHubWeb.Domains;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DevHubWeb.Libraries;
using DevHubWeb.Domains.Enumerations;
using DevHubWeb.Libraries.Extensions;

namespace DevHubWeb.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettingsModel> _options;

        public AuthService(IAuthRepository repo, IMapper mapper, IOptions<AppSettingsModel> options)
        {
            this._repo = repo;
            this._mapper = mapper;
            this._options = options;
        }
        
        public string AuthTokenHandler(string Id, string userName, byte role)
        {
            var userRole = ((UserRolesEnum)role).StringValue();
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.JwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, Id),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, userRole)
                }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);            
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public List<KeyValuePair<byte, string>> GetUserRoles()
        {
            return _repo.GetUserRoles();
        }

        public async Task<UserDetailModel> Login(string username, string password)
        {
            var users = await _repo.Login(username, password);
            var userDetail = _mapper.Map<UserDetailModel>(users);

            return userDetail;
        }

        public Task<UserCreatedModel> Register(UserForRegisterModel user, string password)
        {
            return _repo.Register(user, password);
        }

        public Task<bool> UserExists(string username)
        {
            return _repo.UserExists(username);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
