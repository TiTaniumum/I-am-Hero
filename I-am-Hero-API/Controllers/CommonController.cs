using I_am_Hero_API.Models;
using I_am_Hero_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
    }
}
