using cursoCore2.Models;
using cursoCore2API.Models;

namespace cursoCore2API.Repositories
{
    public class UserRepository
    {
        private readonly StoreContext _context;

        public UserRepository(StoreContext context)
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
