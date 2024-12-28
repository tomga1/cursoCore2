using cursoCoreMVC.Models;
using cursoCoreMVC.Repository.IRepository;
using Newtonsoft.Json;
using System.Net.Http;

namespace cursoCoreMVC.Repository
{
    public class ProductoRepository : Repository<Productos>, IProductoRepository
    {

        private readonly IHttpClientFactory _clientFactory;

        public ProductoRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        //version paginacion
        public async Task<IEnumerable<Productos>> GetProductosAsync(string url)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url);

            var cliente = _clientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                var productoResponse = JsonConvert.DeserializeObject<ProductosResponse>(jsonString);


                return productoResponse?.Items ?? new List<Productos>();    
            }
            else
            {
                return new List<Productos>();
            }
        }
    }
}
