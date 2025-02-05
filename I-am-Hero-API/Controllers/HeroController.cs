using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using I_am_Hero_API.Models;
using I_am_Hero_API.DTO;
using I_am_Hero_API.Services.Interfaces;

namespace I_am_Hero_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IHeroService heroService;
        public HeroController(IAuthService authService, IHeroService heroService)
        {
            this.authService = authService;
            this.heroService = heroService;
        }

        // api/Hero/create
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<TokenDto>> CreateHero(string heroName)
        {
            return await HandleEndpoint(async () =>
            {
                if (await heroService.IsHeroExist())
                {
                    heroService.SetResult(BadRequest(new { error = "Hero exists", message = "User already has Hero!" }));
                    return new TokenDto();
                }
                await heroService.CreateHero(heroName);
                return new TokenDto();
            });
        }
        // api/Hero/get
        [Authorize]
        [HttpGet("get")]
        public async Task<ActionResult<TokenDto>> GetHero()
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetHero();
            });
        }

        [Authorize]
        [HttpPost("edit")]
        public async Task<ActionResult<TokenDto>> EditHero([FromBody] HeroDto heroDto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHero(heroDto);
                return new TokenDto();
            });
        }

        // api/Hero/edit-heroName
        [Authorize]
        [HttpPost("edit-heroName")]
        public async Task<ActionResult<TokenDto>> EditHeroName(string newHerName)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroName(newHerName);
                return new TokenDto();
            });
        }

        // api/Hero/edit-heroLevelCalculationType
        [Authorize]
        [HttpPost("edit-heroLevelCalculationType")]
        public async Task<ActionResult<TokenDto>> EditHeroLevelCalculationType(long levelCalculationTypeId)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroLevelCalculationType(levelCalculationTypeId);
                return new TokenDto();
            });
        }

        // api/Hero/edit-heroExperience
        [Authorize]
        [HttpPost("edit-heroExperience")]
        public async Task<ActionResult<TokenDto>> EditHeroExperience(long exp)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroExperience(exp);
                return new TokenDto();
            });
        }
        [Authorize]
        [HttpPost("create/HeroAtrribute")]
        public async Task<ActionResult<TokenDto>> CreateHeroAttribute(HeroAttributeDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateHeroAttribute(dto);
            });
        }
        [Authorize]
        [HttpGet("get/HeroAttributes")]
        public async Task<ActionResult<TokenDto>> GetHeroAttributes(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetHeroAttributes(id);
            });
        }
        [Authorize]
        [HttpPost("edit/HeroAttriute")]
        public async Task<ActionResult<TokenDto>> EditHeroAttribute(HeroAttributeDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroAttribute(dto);
                return new TokenDto();
            });
        }
        [Authorize]
        [HttpDelete("delete/HeroAttribute")]
        public async Task<ActionResult<TokenDto>> DeleteHeroAttribute(long id)
        {
            return await HandleEndpoint(async () =>
            {
                //await heroService.DeleteHeroAttribute(id);
                return new TokenDto();
            });
        }
        //public async Task<ActionResult<TokenDto>> CreateHeroAttributeStates()
        //public async Task<ActionResult<TokenDto>> GetHeroAttributeStates()
        //public async Task<ActionResult<TokenDto>> DeleteHeroAttributeStates()
        // TODO:
        //public async Task<ActionResult<TokenDto>> CreateHeroSkill()
        //public async Task<ActionResult<TokenDto>> GetHeroSkills()
        //public async Task<ActionResult<TokenDto>> EditHeroSkill()
        //public async Task<ActionResult<TokenDto>> DeleteHeroSkill()
        //public async Task<ActionResult<TokenDto>> CreateHeroStatusEffect()
        //public async Task<ActionResult<TokenDto>> GetHeroStatusEffects()
        //public async Task<ActionResult<TokenDto>> EditHeroStatusEffect()
        //public async Task<ActionResult<TokenDto>> DeleteHeroStatusEffect()
        //public async Task<ActionResult<TokenDto>> CreateHeroBioPiece()
        //public async Task<ActionResult<TokenDto>> GetHeroBioPieces()
        //public async Task<ActionResult<TokenDto>> EditHeroBioPiece()
        //public async Task<ActionResult<TokenDto>> DeleteHeroBioPiece()
        //public async Task<ActionResult<TokenDto>> CreateHeroAchievement()
        //public async Task<ActionResult<TokenDto>> GetHeroAchievements()
        //public async Task<ActionResult<TokenDto>> EditHeroAchievement()
        //public async Task<ActionResult<TokenDto>> DeleteHeroAchievement()

        #region PrivateMethods
        /// <summary>
        /// Метод предназначен для задания контекста. 
        /// Использовать только в методах где есть атрибут [Authorize].
        /// А так же метод ловит эксепшены.
        /// </summary>
        /// <param name="func">Асинхронная лямбда. Для преждевременного ответа использовать heroService.SetResult().</param>
        /// <returns name=TokenDto>Любое DTO которое наследует TokenDto</returns>
        private async Task<ActionResult<TokenDto>> HandleEndpoint(Func<Task<TokenDto>> func)
        {
            if (!await HandleEndpointEnter())
                return BadRequest(new { error = "User not found", message = "Token had not existing user!" });
            try
            {
                TokenDto tokenDto = await func();
                await HandleTokenRegeneration(tokenDto);
                ActionResult<TokenDto>? result = heroService.GetResultOrDefault();
                if (result != null) return result;
                return Ok(tokenDto);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Exception: "+ex.Message);
            }
        }

        private async Task<bool> HandleEndpointEnter()
        {
            User? user = await authService.GetUser(HttpContext);
            if (user == null)
            {
                return false;
            }
            heroService.SetUser(user);
            return true;
        }

        private async Task HandleTokenRegeneration(TokenDto dto)
        {
            Token? token = await authService.RegenerateToken(HttpContext);
            if (token == null)
                return;
            dto.Token = token.token;
            return;
        }
        #endregion PrivateMethods
    }
}
