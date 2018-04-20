using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevHubWeb.Domains
{
    public class UserForRegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "You must specify a password between 8 and 25 characters.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        public byte UserRole { get; set; }
    }

    public class UserForLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class UserCreatedModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public byte UserRole { get; set; }
    }

    public class UserDetailModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public byte UserRole { get; set; }
    }

    public class UserForRefreshModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public byte UserRole { get; set; }
    }
}
