using I_am_Hero_API.Models;

namespace I_am_Hero_API.Services.Interfaces
{
    public interface ICommonService
    {
        IEnumerable<cLevelCalculationType> GetAllcLevelCalculationTypes();
        IEnumerable<cAttributeType> GetAllcAttributeTypes();
    }
}
