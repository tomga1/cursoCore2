using cursoCoreMVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text; 

namespace cursoCoreMVC.Services
{
    public class Service_API : IService_API
    {
        private static string? _baseurl; 

        public Service_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value; 


        }

        public async Task<List<Productos>> Lista()
        {
            List<Productos> lista = new List<Productos>();

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/Producto");
                    if (response.IsSuccessStatusCode)
                    {
                        var json_respuesta = await response.Content.ReadAsStringAsync();
                        lista = JsonConvert.DeserializeObject<List<Productos>>(json_respuesta);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return lista;
        }

        public Task<Productos> Obtener(int idProducto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(Productos objeto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Editar(Productos objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Eliminar(int idProducto)
        {
            bool respuesta = false;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.DeleteAsync($"api/Producto/{idProducto}");

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return respuesta;
        }
    }
}
