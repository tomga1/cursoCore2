using cursoCoreMVC.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text; 

namespace cursoCoreMVC.Services
{
    public class Service_API : IService_API
    {
        private static string? _baseurl; 
        private static string? _username;
        private static string? _password;
        private static string? _token;

        public Service_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;
            _username = builder.GetSection("ApiSettings:Username").Value;
            _password = builder.GetSection("ApiSettings:Password").Value;


        }

        public async Task Authenticate()
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var credenciales = new Credencial() { Username = _username, Password = _password };
            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("api/Authenticate", content);

            if (response.IsSuccessStatusCode)
            {
                // Obtiene directamente el token como string
                var json_respuesta = await response.Content.ReadAsStringAsync();
                _token = json_respuesta.Trim('"'); // Remueve comillas si es necesario
            }
            else
            {
                Console.WriteLine("Error en la autenticación.");
            }
        }




        public async Task<List<Productos>> Lista()
        {
            List<Productos> lista = new List<Productos>();

            await Authenticate();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseurl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/Producto");
                    if (response.IsSuccessStatusCode)
                    {
                        var json_respuesta = await response.Content.ReadAsStringAsync();
                        var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                        lista = resultado.lista; 
                    }

                    return lista;
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
