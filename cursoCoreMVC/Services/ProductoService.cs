using cursoCoreMVC.Models;
using System.Text.Json;

namespace cursoCoreMVC.Services
{
    public class ProductoService : IproductoService
    {

        private readonly HttpClient _httpClient;
        private static string _baseurl;
        //private static string? _username;
        //private static string? _password;
        private static string? _token;

        public ProductoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseurl = configuration["ApiSettings:baseUrl"]; 
        }


        public async Task<List<Productos>> Lista()
        {
            var response = await _httpClient.GetAsync($"{_baseurl}/api/productos");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var productos = JsonSerializer.Deserialize<List<Productos>>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return productos ?? new List<Productos>();
            }

            // Manejo de errores
            return new List<Productos>();
        }
    }
}
