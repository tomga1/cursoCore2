using cursoCore2API.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace cursoCore2API.Repository
{
    public class UserRepository
    {
        private readonly StoreContext _context; 
        public UserRepository(StoreContext context)
        {
            _context = context;
        }

        public User? Authenticate(string username, string password)
        {
            User? userToAuthenticate = _context.users.FirstOrDefault(u => u.Username == username && u.Password == password);
            
            return userToAuthenticate;
        }

        public IEnumerable<User> GetUsers() 
        {
            try
            {
                return _context.users.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los usuarios" + ex.Message, ex);
            }
        }




    }
}
