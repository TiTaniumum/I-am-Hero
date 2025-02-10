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
        public IEnumerable<cAttributeType> GetAllcAttributeTypes()
        {
            return context.cAttributeTypes.ToList();
        }
        public IEnumerable<Application> GetAllApplications()
        {
            return context.Applications.ToList();
        }
        public IEnumerable<cRarity> GetAllcRarities()
        {
            return context.cRarities.ToList();
        }
    }
}
