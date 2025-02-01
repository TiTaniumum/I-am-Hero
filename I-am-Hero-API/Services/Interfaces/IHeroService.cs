using I_am_Hero_API.Models;

namespace I_am_Hero_API.Services.Interfaces
{
    internal interface IHeroService
    {
        void SetUser(User user);
        Task CreateHero(string heroName);
    }
}