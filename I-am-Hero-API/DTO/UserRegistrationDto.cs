﻿namespace I_am_Hero_API.DTO
{
    public class UserRegistrationDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public long ApplicationId { get; set; }
    }
}
