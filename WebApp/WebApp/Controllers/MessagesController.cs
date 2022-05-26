using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/contacts/{id}/messages")]
    //[Route("api/[controller]/")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _service;

        public MessagesController (MessageService service)
        {
            _service = service;
        }


        // GET: api/contacts/{id}/<MessagesController>
        [HttpGet]
        public async Task<IActionResult> Get(string id, string username)
        {
            var messages = await _service.GetMessages(id, username);

            var result = messages.Select(message => new
            {
                id = message.id,
                content = message.content,
                created = message.created,
                sent = message.from == username
            }).OrderBy(m => m.created).ToList();
            return Ok(result);
        }

        // GET api/<MessagesController>/5
        [HttpGet("{id2}")]
        public async Task<IActionResult> Get(string id, string username, int id2)
        {
            var message = await _service.GetMessage(id2);
            if (message == null)
            {
                return NotFound();
            }
            if ((message.from == id && message.to == username) || (message.to == id && message.from == username))
            {
                return Ok(message);
            }
            return BadRequest();
        }

        public class messagesBody
        {
            public string? username { get; set; }

            public string? content { get; set; }
        }

        // POST api/<MessagesController>
        [HttpPost]
        public async Task<IActionResult> Post(string id, [FromBody] messagesBody body)
        {
            var message = await _service.AddMessage(id, body.username, body.content);
            if (message == null)
            {
                return NotFound();
            }
            return StatusCode(201);
        }

        // PUT api/<MessagesController>/5
        [HttpPut("{id2}")]
        public async Task<IActionResult> Put(string id, int id2, [FromBody] messagesBody body)
        {
            var message = await _service.GetMessage(id2);
            if (message == null)
            {
                return NotFound();
            }
            if ((message.from == id && message.to == body.username) || (message.to == id && message.from == body.username))
            {
                await _service.EditMessage(message, body.content);
                return StatusCode(204);
            }
            return BadRequest();
        }

        // DELETE api/<MessagesController>/5
        [HttpDelete("{id2}")]
        public async Task<IActionResult> Delete(string id, int id2, [FromBody] string username)
        {
            var message = await _service.GetMessage(id2);
            if (message == null)
            {
                return NotFound();
            }
            if ((message.from == id && message.to == username) || (message.to == id && message.from == username))
            {
                await _service.DeleteMessage(message);
                return StatusCode(204);
            }
            return BadRequest();
        }
    }
}