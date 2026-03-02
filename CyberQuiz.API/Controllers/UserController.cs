using CyberQuiz.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CyberQuiz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserResultService _userResultService;

        public UserController(IUserResultService userResultService)
        {
            _userResultService = userResultService;
        }

        [HttpGet("progression")]
        public async Task<IActionResult> GetProgression([FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest("userId krävs");

            var progression = await _userResultService.GetProgressionByUserAsync(userId);
            return Ok(progression);
        }
    }
}