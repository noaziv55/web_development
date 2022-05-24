using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/invitation")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly WebChatContext _context;

        public InvitationsController(WebChatContext context)
        {
            _context = context;
        }
        public class invitationsBody
        {
            public string? from { get; set; }

            public string? to { get; set; }

            public string? server { get; set; }
        }

        // POST api/<InvitationsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] invitationsBody body)
        {
            var contact = await _context.Contacts.FindAsync(body.from, body.to);
            if (contact == null)
            {
                contact = new Contact(body.from, body.to, body.from, body.server);
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return StatusCode(201);
            }
            return BadRequest();
        }
    }
}
