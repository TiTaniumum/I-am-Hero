namespace I_am_Hero_API.Services.Interfaces
{
    public interface ILoggerService
    {
        Task LogException(long? userId, string url, Exception ex);
    }
}
