using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Models;
using WebApp.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/invitations/")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly WebChatContext _context;
        private readonly IHubContext<ChatHub> hubContext;

        public InvitationsController(WebChatContext context, IHubContext<ChatHub> HubContext)
        {
            _context = context;
            hubContext = HubContext;
        }
        public class invitationsBody
        {
            // contact
            public string? from { get; set; }

            // user
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
                await hubContext.Clients.Group(body.to).SendAsync("refresh");
                return StatusCode(201);
            }
            return BadRequest();
        }
    }
}