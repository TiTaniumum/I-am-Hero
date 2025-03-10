using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using I_am_Hero_API.Models;
using I_am_Hero_API.DTO;
using I_am_Hero_API.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;

namespace I_am_Hero_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IHeroService heroService;
        private readonly ILoggerService loggerService;
        public HeroController(IAuthService authService, IHeroService heroService, ILoggerService loggerService)
        {
            this.authService = authService;
            this.heroService = heroService;
            this.loggerService = loggerService;
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
                return await heroService.CreateHero(heroName);
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
        
        // api/Hero/edit
        [Authorize]
        [HttpPut("edit")]
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
        [HttpPut("edit-heroName")]
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
        [HttpPut("edit-heroLevelCalculationType")]
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
        [HttpPut("edit-heroExperience")]
        public async Task<ActionResult<TokenDto>> EditHeroExperience(long exp)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroExperience(exp);
                return new TokenDto();
            });
        }
        
        // api/Hero/create/HeroAtrribute
        [Authorize]
        [HttpPost("create/HeroAtrribute")]
        public async Task<ActionResult<TokenDto>> CreateHeroAttribute(HeroAttributeDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateHeroAttribute(dto);
            });
        }
        
        // api/Hero/get/HeroAttributes
        [Authorize]
        [HttpGet("get/HeroAttributes")]
        public async Task<ActionResult<TokenDto>> GetHeroAttributes(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetHeroAttributes(id);
            });
        }
        
        // api/Hero/edit/HeroAttribute
        [Authorize]
        [HttpPut("edit/HeroAttribute")]
        public async Task<ActionResult<TokenDto>> EditHeroAttribute(HeroAttributeDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroAttribute(dto);
                return new TokenDto();
            });
        }
        
        // api/Hero/delete/HeroAttribute
        [Authorize]
        [HttpDelete("delete/HeroAttribute")]
        public async Task<ActionResult<TokenDto>> DeleteHeroAttribute(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteHeroAttribute(id);
                return new TokenDto();
            });
        }
        
        // api/Hero/create/HeroAttributeStates
        [Authorize]
        [HttpPost("create/HeroAttributeState")]
        public async Task<ActionResult<TokenDto>> CreateHeroAttributeState(HeroAttributeStateDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateHeroAttributeState(dto);
            });
        }
        
        // api/Hero/create/HeroAttributeStates
        [Authorize]
        [HttpPost("create/HeroAttributeStates")]
        public async Task<ActionResult<TokenDto>> CreateHeroAttributeStates(HeroAttributeStatesDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateHeroAttributeStates(dto);
            });
        }
        
        // api/Hero/get/HeroAttributeStates
        [Authorize]
        [HttpGet("get/HeroAttributeStates")]
        public async Task<ActionResult<TokenDto>> GetHeroAttributeStates(long? heroAttributeId)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetHeroAttributeStates(heroAttributeId);
            });
        }
        
        // api/Hero/delete/HeroAttributeState
        [Authorize]
        [HttpDelete("delete/HeroAttributeState")]
        public async Task<ActionResult<TokenDto>> DeleteHeroAttributeState(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteHeroAttributeState(id);
                return new TokenDto();
            });
        }

        // api/Hero/create/HeroSkill
        [Authorize]
        [HttpPost("create/HeroSkill")]
        public async Task<ActionResult<TokenDto>> CreateHeroSkill(HeroSkillDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateHeroSkill(dto);
            });
        }

        // api/Hero/get/HeroSkills
        [Authorize]
        [HttpGet("get/HeroSkills")]
        public async Task<ActionResult<TokenDto>> GetHeroSkills(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetHeroSkills(id);
            });
        }

        // api/Hero/edit/HeroSkill
        [Authorize]
        [HttpPut("edit/HeroSkill")]
        public async Task<ActionResult<TokenDto>> EditHeroSkill(HeroSkillDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroSkill(dto);
                return new TokenDto();
            });
        }

        // api/Hero/delete/HeroSkill
        [Authorize]
        [HttpDelete("delete/HeroSkill")]
        public async Task<ActionResult<TokenDto>> DeleteHeroSkill(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteHeroSkill(id);
                return new TokenDto();
            });
        }

        // api/Hero/create/HeroStatusEffect
        [Authorize]
        [HttpPost("create/HeroStatusEffect")]
        public async Task<ActionResult<TokenDto>> CreateHeroStatusEffect(HeroStatusEffectDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateHeroStatusEffect(dto);
            });
        }

        // api/Hero/get/HeroStatusEffects
        [Authorize]
        [HttpGet("get/HeroStatusEffects")]
        public async Task<ActionResult<TokenDto>> GetHeroStatusEffects(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetHeroStatusEffects(id);
            });
        }

        // api/Hero/edit/HeroStatusEffect
        [Authorize]
        [HttpPut("edit/HeroStatusEffect")]
        public async Task<ActionResult<TokenDto>> EditHeroStatusEffect(HeroStatusEffectDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroStatusEffect(dto);
                return new TokenDto();
            });
        }

        // api/Hero/delete/HeroStatusEffect
        [Authorize]
        [HttpDelete("delete/HeroStatusEffect")]
        public async Task<ActionResult<TokenDto>> DeleteHeroStatusEffect(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteHeroStatusEffect(id);
                return new TokenDto();
            });
        }

        // api/Hero/create/HeroBioPiece
        [Authorize]
        [HttpPost("create/HeroBioPiece")]
        public async Task<ActionResult<TokenDto>> CreateHeroBioPiece(HeroBioPieceDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateHeroBioPiece(dto);
            });
        }

        // api/Hero/get/HeroBioPieces
        [Authorize]
        [HttpGet("get/HeroBioPieces")]
        public async Task<ActionResult<TokenDto>> GetHeroBioPieces(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetHeroBioPieces(id);
            });
        }

        // api/Hero/edit/HeroBioPiece
        [Authorize]
        [HttpPut("edit/HeroBioPiece")]
        public async Task<ActionResult<TokenDto>> EditHeroBioPiece(HeroBioPieceDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroBioPiece(dto);
                return new TokenDto();
            });
        }

        // api/Hero/delete/HeroBioPiece
        [Authorize]
        [HttpDelete("delete/HeroBioPiece")]
        public async Task<ActionResult<TokenDto>> DeleteHeroBioPiece(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteHeroBioPiece(id);
                return new TokenDto();
            });
        }

        // api/Hero/create/HeroAchievement
        [Authorize]
        [HttpPost("create/HeroAchievement")]
        public async Task<ActionResult<TokenDto>> CreateHeroAchievement(HeroAchievementDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateHeroAchievement(dto);
            });
        }

        // api/Hero/get/HeroAchievements
        [Authorize]
        [HttpGet("get/HeroAchievements")]
        public async Task<ActionResult<TokenDto>> GetHeroAchievements(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetHeroAchievements(id);
            });
        }

        // api/Hero/edit/HeroAchievement
        [Authorize]
        [HttpPut("edit/HeroAchievement")]
        public async Task<ActionResult<TokenDto>> EditHeroAchievement(HeroAchievementDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroAchievement(dto);
                return new TokenDto();
            });
        }

        // api/Hero/delete/HeroAchievement
        [Authorize]
        [HttpDelete("delete/HeroAchievement")]
        public async Task<ActionResult<TokenDto>> DeleteHeroAchievement(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteHeroAchievement(id);
                return new TokenDto();
            });
        }

        // api/Hero/create/Quest
        [Authorize]
        [HttpPost("create/Quest")]
        public async Task<ActionResult<TokenDto>> CreateQuest(QuestDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateQuest(dto);
            });
        }

        // api/Hero/get/Quests
        [Authorize]
        [HttpGet("get/Quests")]
        public async Task<ActionResult<TokenDto>> GetQuests(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetQuests(id);
            });
        }

        // api/Hero/edit/Quest
        [Authorize]
        [HttpPut("edit/Quest")]
        public async Task<ActionResult<TokenDto>> EditQuest(QuestDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditQuest(dto);
                return new TokenDto();
            });
        }

        // api/Hero/delete/Quest
        [Authorize]
        [HttpDelete("delete/Quest")]
        public async Task<ActionResult<TokenDto>> DeleteQuest(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteQuest(id);
                return new TokenDto();
            });
        }

        // api/Hero/delete/Behaviour
        [Authorize]
        [HttpDelete("delete/Behaviour")]
        public async Task<ActionResult<TokenDto>> DeleteBehaviour(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteBehaviour(id);
                return new TokenDto();
            });
        }

        // api/Hero/create/QuestLine
        [Authorize]
        [HttpPost("create/QuestLine")]
        public async Task<ActionResult<TokenDto>> CreateQuestLine(QuestLineDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateQuestLine(dto);
            });
        }

        // api/Hero/get/QuestLines
        [Authorize]
        [HttpGet("get/QuestLines")]
        public async Task<ActionResult<TokenDto>> GetQuestLines(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetQuestLines(id);
            });
        }

        // api/Hero/edit/QuestLine
        [Authorize]
        [HttpPut("edit/QuestLine")]
        public async Task<ActionResult<TokenDto>> EditQuestLine(QuestLineDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditQuestLine(dto);
                return new TokenDto();
            });
        }

        // api/Hero/delete/QuestLine
        [Authorize]
        [HttpDelete("delete/QuestLine")]
        public async Task<ActionResult<TokenDto>> DeleteQuestLine(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteQuestLine(id);
                return new TokenDto();
            });
        }

        // api/Hero/create/Calendar
        [Authorize]
        [HttpPost("create/Calendar")]
        public async Task<ActionResult<TokenDto>> CreateCalendar(CalendarDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateCalendar(dto);
            });
        }

        // api/Hero/get/Calendars
        [Authorize]
        [HttpGet("get/Calendars")]
        public async Task<ActionResult<TokenDto>> GetCalendars(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetCalendars(id);
            });
        }

        // api/Hero/edit/Calendar
        [Authorize]
        [HttpPut("edit/Calendar")]
        public async Task<ActionResult<TokenDto>> EditCalendar(CalendarDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditCalendar(dto);
                return new TokenDto();
            });
        }

        // api/Hero/delete/Calendar
        [Authorize]
        [HttpDelete("delete/Calendar")]
        public async Task<ActionResult<TokenDto>> DeleteCalendar(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteCalendar(id);
                return new TokenDto();
            });
        }
        
        /// <summary>
        /// Метод для отметки участия в календаре.
        /// Он создает новую запись в таблице CalendarAttendance или изменяет статус существующей записи.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // api/Hero/attend/CalendarAttendance
        [Authorize]
        [HttpPost("attend/CalendarAttendance")]
        public async Task<ActionResult<TokenDto>> AttendCalendar(CalendarAttendanceDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.AttendCalendar(dto);
            });
        }
        
        /// <summary>
        /// Метод для получения всех записей участия в календаре.
        /// Параметры dateFrom и dateTo необязательные.
        /// </summary>
        /// <param name="calendarId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        // api/Hero/get/CalendarAttendances
        [Authorize]
        [HttpGet("get/CalendarAttendances")]
        public async Task<ActionResult<TokenDto>> GetCalendarAttendances(long calendarId, DateOnly? dateFrom, DateOnly? dateTo)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetCalendarAttendances(calendarId, dateFrom, dateTo);
            });
        }
        
        // api/Hero/create/Habbit
        [Authorize]
        [HttpPost("create/Habbit")]
        public async Task<ActionResult<TokenDto>> CreateHeroHabbit(HeroHabbitDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.CreateHeroHabbit(dto);
            });
        }

        // api/Hero/get/Habbits
        [Authorize]
        [HttpGet("get/Habbits")]
        public async Task<ActionResult<TokenDto>> GetHeroHabbits(long? id)
        {
            return await HandleEndpoint(async () =>
            {
                return await heroService.GetHeroHabbits(id);
            });
        }

        // api/Hero/edit/Habbit
        [Authorize]
        [HttpPut("edit/Habbit")]
        public async Task<ActionResult<TokenDto>> EditHeroHabbit(HeroHabbitDto dto)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.EditHeroHabbit(dto);
                return new TokenDto();
            });
        }

        // api/Hero/delete/Habbit
        [Authorize]
        [HttpDelete("delete/Habbit")]
        public async Task<ActionResult<TokenDto>> DeleteHeroHabbit(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.DeleteHeroHabbit(id);
                return new TokenDto();
            });
        }

        // api/Hero/checkin/Habbit
        [Authorize]
        [HttpPut("checkin/Habbit")]
        public async Task<ActionResult<TokenDto>> CheckinHeroHabbit(long id)
        {
            return await HandleEndpoint(async () =>
            {
                await heroService.CheckinHeroHabbit(id);
                return new TokenDto();
            });
        }

        //TODO: Social related endpoints

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
#pragma warning disable CS4014 // Этот метод не нуждается в ожидании
                loggerService.LogException(heroService.GetUser().Id, HttpContext.Request.GetDisplayUrl(), ex);
#pragma warning restore CS4014
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
#pragma warning disable CS4014 // Этот метод не нуждается в ожидании
                loggerService.LogException(heroService.GetUser().Id, HttpContext.Request.GetDisplayUrl(), ex);
#pragma warning restore CS4014
                return BadRequest("Exception: " + ex.Message);
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
