using cursoCore2.Data;
using cursoCore2.Models;
using cursoCore2API.Models;

namespace cursoCore2API.Repositories
{
    public class UserRepository
    {
        private readonly AplicationDbContext _context;

        public UserRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            User? userToAuthenticate = _context.users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return userToAuthenticate;

        }

        public List<User> GetUsers()
        {
            return _context.users.ToList(); 
        }

    }
}
