using I_am_Hero_API.Data;
using I_am_Hero_API.Services.Interfaces;
using I_am_Hero_API.Models;
namespace I_am_Hero_API.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ApplicationDbContext context;

        public LoggerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task LogException(long? userId, string url, Exception ex)
        {
            await context.ExceptionLogs.AddAsync(
                new ExceptionLog {
                    UserId = userId,
                    Url = url,
                    ExceptionMessage = ex.Message,
                    StackTrace = ex.StackTrace
                }
            );
            await context.SaveChangesAsync();
        }
    }
}
