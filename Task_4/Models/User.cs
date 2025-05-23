using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public enum UserStatus
    {
        ACTIVE,
        BLOCKED
    }
    public enum LoginResult
    {
        Success,
        UserNotFound,
        UserBlocked,
        WrongPassword,
        Failed
    }

    public class User : IdentityUser
    {
        [Required]
        [EmailAddress]
        public override string Email { get; set; } 

        public DateTime? LastSeen { get; set; }

        public UserStatus Status { get; set; } = UserStatus.ACTIVE;

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
