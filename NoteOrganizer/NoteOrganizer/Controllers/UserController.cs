using Microsoft.AspNetCore.Mvc;
using NoteOrganizer.Core.DTO;
using NoteOrganizer.Core.Interface;

namespace NoteOrganizer.Controllers
{
    [ApiController]
    [Route("api/User/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto model)
        {
            var result = await _authService.Register(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDto model)
        {
            var response = await _authService.Login(model);
            return StatusCode(response.StatusCode, response);
        }
    };
}