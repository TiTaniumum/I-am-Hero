using I_am_Hero_API.DTO;
using I_am_Hero_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace I_am_Hero_API.Services.Interfaces
{
    public interface IHeroService
    {
        void SetResult(ActionResult<TokenDto> result);
        ActionResult<TokenDto>? GetResultOrDefault();
        void SetUser(User user);
        Task CreateHero(string heroName);
        Task EditHeroName(string newHeroName);
        Task EditHeroLevelCalculationType(long levelCalculationTypeId);
        Task EditHeroExperience(long exp);
        Task<bool> IsHeroExist();
    }
}