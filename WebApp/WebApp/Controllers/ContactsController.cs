using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {

        private readonly WebChatContext _context;

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

        public ContactsController(WebChatContext context)
        {
            _context = context;
        }
        // GET: api/<ContactsController>

        
        [HttpGet]
        public async Task<IActionResult> Get(string username)
        {
            var result = await _context.Contacts.Where(contact => contact.ContactOfUser == username).ToListAsync();

            return Ok(result);
        }


        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string username, string id)
        {
            var contact = await _context.Contacts.FindAsync(id, username);
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
            var contact = await _context.Contacts.FindAsync(newContact.contactName, newContact.username);
            if (contact == null)
            {
                //if the contact in our server
                var user = await _context.Users.FindAsync(newContact.contactName);
                if (user == null)
                {
                    return NotFound();
                }
                contact = new Contact(newContact.contactName, newContact.username, newContact.contactNickname, newContact.contactServer);
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return StatusCode(201);

            }

            return NotFound();
        }


        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] updateContact body)
        {
            var contact = await _context.Contacts.FindAsync(id, body.username);
            if (contact == null)
            {
                return NotFound();
            }
            contact.NicknameOfContact = body.contactNickname;
            contact.server = body.contactServer;
            _context.Entry(contact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }


        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromBody] string username)
        {
            var contact = await _context.Contacts.FindAsync(id, username);
            if (contact == null)
            {
                return NotFound();
            }
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }
    }
}