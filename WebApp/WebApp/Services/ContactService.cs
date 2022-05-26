using WebApi.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApp.Services
{
    public class ContactService
    {
        private readonly WebChatContext _context;

        public ContactService(WebChatContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Contact>?> GetAllContacts(string username)
        {
            var contacts = await _context.Contacts.Where(contact => contact.ContactOfUser == username).ToListAsync();

            return contacts;
        }

        public async Task<Contact> GetContact(string contactName, string userName)
        {
            var contact = await _context.Contacts.FindAsync(contactName, userName);
            return contact;
        }

        public async Task<Contact> AddContact(string contactName, string username, string contactNickname, string contactServer)
        {
            var contact = await GetContact(contactName, username);
            if (contact == null)
            {
                //if the contact in our server
                var user = await _context.Users.FindAsync(contactName);
                if (user != null)
                {
                    contact = new Contact(contactName, username, contactNickname, contactServer);
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                }
            }
            return contact;
        }

        public async Task<Contact> EditContact(string contactName, string username, string contactNickname, string contactServer)
        {
            var contact = await GetContact(contactName, username);
            if (contact != null)
            {
                contact.NicknameOfContact = contactNickname;
                contact.server = contactServer;
                _context.Entry(contact).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return contact;
        }


        public async Task DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return;
        }

    }
}