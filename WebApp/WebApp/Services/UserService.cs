using WebApi.Data;
using WebApi.Models;

namespace WebApp.Services
{
    public class UserService
    {
        private readonly WebChatContext _context;
        public UserService(WebChatContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string username)
        {
            var contact = await _context.Users.FindAsync(username);
            return contact;
        }

        public async Task AddUser(string username, string password, string server)
        {
            var user = new User(username, password, server);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
