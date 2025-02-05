namespace I_am_Hero_API.DTO
{
    public class UserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public UserDto() { }
        public UserDto(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }
        public UserDto(AuthDto dto)
        {
            Email = dto.Email;
            Password = dto.Password;
        }
    }
}
