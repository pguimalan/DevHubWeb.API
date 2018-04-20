using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevHubWeb.Data.Entities;
using DevHubWeb.Domains;
using DevHubWeb.Domains.Enumerations;
using DevHubWeb.Libraries.Common;
using DevHubWeb.Libraries.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DevHubWeb.Data.Repo.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DevHubContext _context;
        private readonly IMapper _mapper;

        public AuthRepository(DevHubContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<UserCreatedModel> Register(UserForRegisterModel user, string password)
        {
            var userToCreate = _mapper.Map<Users>(user);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            userToCreate.PasswordHash = passwordHash;
            userToCreate.PasswordSalt = passwordSalt;

            await _context.AddAsync(userToCreate);
            await _context.SaveChangesAsync();

            var userToReturn = _mapper.Map<UserCreatedModel>(userToCreate);

            return userToReturn;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username))
                return true;
            return false;
        }

        public async Task<Users> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // auth successful
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

        public List<KeyValuePair<byte, string>> GetUserRoles()
        {
            var userRoles = new List<KeyValuePair<byte, string>>();
            userRoles = EnumHelper.GetEnumList<UserRolesEnum>().Select(s => new
            {
                key = (byte)s,
                value = s.StringValue()
            }).AsEnumerable().Select(obj => new KeyValuePair<byte, string>(obj.key, obj.value)).ToList();

            return userRoles;
        }
    }
}
