using cursoCoreMVC.Repository.IRepository;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http;
using System.Text;

namespace cursoCoreMVC.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        //Injeccion  de dependencias se debe importar el IHttpClientFactory
        private readonly IHttpClientFactory _httpClientFactory;



        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory; 
        }



        public async Task<bool> ActualizarAsync(string url, T itemActualizar)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Patch, url);   
            if (itemActualizar != null) 
            {
                peticion.Content = new StringContent(JsonConvert.SerializeObject(itemActualizar),
                    Encoding.UTF8, "application/json");
            }
            else
            {
                return false; 
            }

            var cliente = _httpClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);  

            //Validar si se actualizo y retorna boleano

            if(respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true; 
            }
            else
            {
                return false; 
            }
        }

        public async Task<bool> ActualizarProductoAsync(string url, T productoAActualizar)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Patch, url);
            var multipartContent = new MultipartFormDataContent();

            if (productoAActualizar != null)
            {
                //serializar cada propiedad de producoActualizar
                foreach (var property in typeof(T).GetProperties())
                {
                    var value = property.GetValue(productoAActualizar);
                    if(value != null)
                    {
                        if(property.PropertyType == typeof(IFormFile))
                        {
                            var file = value as IFormFile;
                            if(file != null)
                            {
                                var streamContent = new StreamContent(file.OpenReadStream());
                                streamContent.Headers.ContentType =
                                    new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                                multipartContent.Add(streamContent, property.Name, file.FileName);
                            }
                        }
                        else
                        {
                            var stringContent = new StringContent(value.ToString());
                            multipartContent.Add(stringContent, property.Name);
                        }
                    }

                }
            }
            else
            {
                return false;
            }

            peticion.Content = multipartContent;
            var cliente = _httpClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano

            if (respuesta.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> BorrarAsync(string url, int Id)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Delete, url);
           

            var cliente = _httpClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable> Buscar(string url, string nombre)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url + nombre);

            var cliente = _httpClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CrearAsync(string url, T itemCrear)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url);
            if (itemCrear != null)
            {
                peticion.Content = new StringContent(JsonConvert.SerializeObject(itemCrear),
                    Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var cliente = _httpClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CrearProductoAsync(string url, T productoACrear)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url);
            var multipartContent = new MultipartFormDataContent();

            if (productoACrear != null)
            {
                //serializar cada propiedad de producoActualizar
                foreach (var property in typeof(T).GetProperties())
                {
                    var value = property.GetValue(productoACrear);
                    if (value != null)
                    {
                        if (property.PropertyType == typeof(IFormFile))
                        {
                            var file = value as IFormFile;
                            if (file != null)
                            {
                                var streamContent = new StreamContent(file.OpenReadStream());
                                streamContent.Headers.ContentType =
                                    new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                                multipartContent.Add(streamContent, property.Name, file.FileName);
                            }
                        }
                        else
                        {
                            var stringContent = new StringContent(value.ToString());
                            multipartContent.Add(stringContent, property.Name);
                        }
                    }

                }
            }
            else
            {
                return false;
            }

            peticion.Content = multipartContent;
            var cliente = _httpClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable> GetAllAsync(string url)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url);

            var cliente = _httpClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else
            {
                return null;
            }
        }



        public async Task<T> GetAsync(string url, int Id)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url + Id);

            var cliente = _httpClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable> GetProductosEnCategoriaAsync(string url, int categoriaId)
        {
            var peticion = new HttpRequestMessage(HttpMethod.Post, url + categoriaId);

            var cliente = _httpClientFactory.CreateClient();

            HttpResponseMessage respuesta = await cliente.SendAsync(peticion);

            //Validar si se actualizo y retorna boleano

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else
            {
                return null;
            }

        }
    }

}
