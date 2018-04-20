using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public byte? UserRole { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public bool? PasswordReset { get; set; }
        public string PasswordResetReason { get; set; }
    }
}
