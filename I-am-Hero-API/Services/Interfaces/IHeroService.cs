using I_am_Hero_API.Models;

namespace I_am_Hero_API.Services.Interfaces
{
    public interface IHeroService
    {
        void SetUser(User user);
        Task CreateHero(string heroName);
        Task<bool> IsHeroExist();
    }
}