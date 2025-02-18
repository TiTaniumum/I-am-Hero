using I_am_Hero_API.Models;
using I_am_Hero_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace I_am_Hero_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService commonService;

        public CommonController(ICommonService commonService)
        {
            this.commonService = commonService;
        }

        // api/Common/all-cLevelCalculationType
        [HttpGet("all-cLevelCalculationType")]
        public ActionResult<IEnumerable<cLevelCalculationType>> GetAllcLevelCalculationTypes()
        {
            return Ok(commonService.GetAllcLevelCalculationTypes());
        }
        // api/Common/all-cAttributeType
        [HttpGet("all-cAttributeType")]
        public ActionResult<IEnumerable<cAttributeType>> GetAllcAttributeTypes()
        {
            return Ok(commonService.GetAllcAttributeTypes());
        }
        // api/Common/all-cSkillType
        [HttpGet("all-Application")]
        public ActionResult<IEnumerable<Application>> GetAllApplications()
        {
            return Ok(commonService.GetAllApplications());
        }
        // api/Common/all-cRarity
        [HttpGet("all-cRarity")]
        public ActionResult<IEnumerable<cRarity>> GetAllcRarities()
        {
            return Ok(commonService.GetAllcRarities());
        }
        // api/Common/all-cDifficulty
        [HttpGet("all-cDifficulty")]
        public ActionResult<IEnumerable<cDifficulty>> GetAllcDifficulties()
        {
            return Ok(commonService.GetAllcDifficulties());
        }
        // api/Common/all-cQuestStatus
        [HttpGet("all-cQuestStatus")]
        public ActionResult<IEnumerable<cQuestStatus>> GetAllcQuestStatuses()
        {
            return Ok(commonService.GetAllcQuestStatuses());
        }
        // api/Common/all-cCalendarBehaviour
        [HttpGet("all-cCalendarBehaviour")]
        public ActionResult<IEnumerable<cCalendarBehaviour>> GetAllcCalendarBehaviours()
        {
            return Ok(commonService.GetAllcCalendarBehaviours());
        }
        // api/Common/all-cCalendarStatus
        [HttpGet("all-cCalendarStatus")]
        public ActionResult<IEnumerable<cCalendarStatus>> GetAllcCalendarStatuses()
        {
            return Ok(commonService.GetAllcCalendarStatuses());
        }
        // api/Common/all-cPopupInterval
        [HttpGet("all-cPopupInterval")]
        public ActionResult<IEnumerable<cPopupInterval>> GetAllcPopupIntervals()
        {
            return Ok(commonService.GetAllcPopupIntervals());
        }
    }
}
