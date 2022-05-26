using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApi.Controllers
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {

        private readonly ContactService _service;

        public ContactsController(ContactService service)
        {
            _service = service;
        }

        public class contactBody
        {
            public string? username { get; set; }
            public string? contactName { get; set; }
            public string? contactNickname { get; set; }
            public string? contactServer { get; set; }
        }

        public class updateContact
        {
            public string? username { get; set; }
            public string? contactNickname { get; set; }
            public string? contactServer { get; set; }
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<IActionResult> Get(string username)
        {
            var result = await _service.GetAllContacts(username);

            return Ok(result);
        }


        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string username, string id)
        {
            var contact = await _service.GetContact(id, username);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);

        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] contactBody newContact)
        {
            if (newContact.username == newContact.contactName)
            {
                return BadRequest();
            }

            var contact = await _service.AddContact(newContact.contactName, newContact.username, newContact.contactNickname, newContact.contactServer);
            if (contact != null)
            {
                return StatusCode(201);
            }

            return NotFound();
        }


        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] updateContact body)
        {
            var contact = await _service.EditContact(id, body.username, body.contactNickname, body.contactServer);
            if (contact == null)
            {
                return NotFound();
            }
          
            return StatusCode(204);
        }


        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromBody] string username)
        {
            var contact = await _service.GetContact(id, username);
            if (contact == null)
            {
                return NotFound();
            }

            await _service.DeleteContact(contact);
            return StatusCode(204);
        }
    }
}