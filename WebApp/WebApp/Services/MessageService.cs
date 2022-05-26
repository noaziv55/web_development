using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApp.Services
{
    public class MessageService
    {
        private readonly WebChatContext _context;

        public MessageService(WebChatContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Message>?> GetMessages(string id, string username)
        {
            return await _context.Messages.Where(message => (message.from == id && message.to == username)
            || (message.to == id && message.from == username)).ToListAsync();

        }


        public async Task<Message> GetMessage(int id2)
        {
            return await _context.Messages.FindAsync(id2);

        }

        public async Task<Message> AddMessage(string id, string username, string content)
        {
            var contact = await _context.Contacts.FindAsync(id, username);
            if (contact != null)
            {
                contact.lastdate = DateTime.Now;
                contact.lastMessage = content;

                var message = new Message(username, id, content);
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();
                return message;
            }
            return null;
        }

        public async Task EditMessage(Message message, string content)
        {
            message.content = content;
            await _context.SaveChangesAsync();
            return;
        }
        public async Task DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
