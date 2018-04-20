using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DevHubWeb.Domains;
using DevHubWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevHubWeb.API.Controllers
{
    [Produces("application/json")]
    [Route("/account")]
    public class AuthController : Controller
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            this._service = service;
        }

        [HttpPost("register"), Authorize(Roles = "Super User")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterModel userForRegister)
        {
            if (!string.IsNullOrEmpty(userForRegister.UserName))
                userForRegister.UserName = userForRegister.UserName.ToLower();

            if (await _service.UserExists(userForRegister.UserName))
                return StatusCode((int)HttpStatusCode.Conflict, new { Result = "Username is already taken" });

            // validate request
            if (!ModelState.IsValid)
                return StatusCode((int)HttpStatusCode.InternalServerError, ModelState);

            var CreatedUser = await _service.Register(userForRegister, userForRegister.Password);

            if (CreatedUser != null)
                return StatusCode((int)HttpStatusCode.Created, CreatedUser);

            return StatusCode((int)HttpStatusCode.InternalServerError, new { Result = "Could not create user." });
        }

        [HttpGet("GetUserRoles")]
        public IActionResult GetUserRoles()
        {
            return Ok(_service.GetUserRoles());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginModel userForLogin)
        {
            var user = await _service.Login(userForLogin.Username, userForLogin.Password);

            if (user == null)
                return Unauthorized();

            var tokenString = _service.AuthTokenHandler(user.UserID.ToString(), user.Username, user.UserRole);

            return Ok(new { Jwt = tokenString, user, userForLogin.RememberMe });
        }

        [HttpPost("RefreshToken"), Authorize(Roles = "Admin, User, Super User")]
        public IActionResult RefreshToken([FromBody] UserForRefreshModel model)
        {
            var tokenString = _service.AuthTokenHandler(model.UserID.ToString(), model.Username, model.UserRole);
            return Ok(new { Jwt = tokenString  });
        }
    }
}