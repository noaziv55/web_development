using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        public UsersController(UserService service)
        {
            _service = service;
        }

        public class UserBody
        {
            public string? Username { get; set; }

            public string? Password { get; set; }

            public string? Server { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserBody body)
        {
            var user = await _service.GetUser(body.Username);
            if (user == null)
            {
                await _service.AddUser(body.Username, body.Password, body.Server);
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _service.GetUser(username);
            if (user == null)
            {
                // call to register page to add this new user
                return NotFound();
            }
            if (user.Password == password)
            {
                return Ok(user);
            }
            return BadRequest();
        }


    }
}