namespace I_am_Hero_API.Services
{
    public class JwtService
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration)
        {
            this.configuration=configuration;
        }

    }
}
