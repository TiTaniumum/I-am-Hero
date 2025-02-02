using I_am_Hero_API.Data;
using I_am_Hero_API.Models;
using I_am_Hero_API.Services.Interfaces;

namespace I_am_Hero_API.Services
{
    public class CommonService : ICommonService
    {
        private readonly ApplicationDbContext context;
        public CommonService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<cLevelCalculationType> GetAllcLevelCalculationTypes()
        {
            return context.cLevelCalculationTypes.ToList();
        }
    }
}
