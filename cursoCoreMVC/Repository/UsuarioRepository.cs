using cursoCoreMVC.Models;
using cursoCoreMVC.Repository.IRepository;

namespace cursoCoreMVC.Repository
{
    public class UsuarioRepository : Repository<User>, IUsuarioRepository
    {
        //injeccion de dependencias se debe importar el ihttpclientfactory
        private readonly IHttpClientFactory _clientFactory;

        public UsuarioRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory; 
        }
    }
}
