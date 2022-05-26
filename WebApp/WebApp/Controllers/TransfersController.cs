using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Data;
using WebApi.Models;
using WebApp.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly WebChatContext _context;
        private readonly IHubContext<ChatHub> hubContext;

        public TransfersController(WebChatContext context, IHubContext<ChatHub> HubContext)
        {
            _context = context;
            hubContext = HubContext;
        }
        public class TransfersBody
        {
            public string? from { get; set; }

            public string? to { get; set; }

            public string? content { get; set; }
        }

        // POST api/<TransfersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransfersBody body)
        {
            var contact = await _context.Contacts.FindAsync(body.from, body.to);
            if (contact == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(body.to);
            if (contact.server == user.ServerAddress)
            {
                contact.lastdate = DateTime.Now;
                contact.lastMessage = body.content;
            }
            else
            {
                var message = new Message(body.to, body.from, body.content);
                _context.Messages.Add(message);
            }
            //var message = new Message(body.from, body.to, body.content);
            //_context.Messages.Add(message);
            await _context.SaveChangesAsync();
            await hubContext.Clients.Group(body.to).SendAsync("refresh");
            return StatusCode(201);
        }
    }
}