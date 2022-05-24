using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WebChatContext _context;
        public UsersController(WebChatContext context)
        {
            _context = context;
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
            var user = await _context.Users.FindAsync(body.Username);
            if (user == null)
            {
                user = new User(body.Username, body.Password, body.Server);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.FindAsync(username);
            if (user == null)
            {
                // call to register page to add this new user
                return NotFound();
            }
            if (user.Password == password) {
                return Ok(user); }
            return BadRequest();
        }


    }
}