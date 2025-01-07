using cursoCore2API.Services.IServices;

namespace cursoCore2API.Services
{
    public class MessageService : IMessageService
    {
        private readonly Dictionary<string, string> _message; 
        public MessageService()
        {
            _message = new Dictionary<string, string>
            {
                { "CategoriaNoEncontrada", "Categoria no encontrada" },
                { "ErrorInesperado", "Se ha producido un error inesperado" }
            };
        }

        public string GetMessage(string key)
        {
            return _message.TryGetValue(key, out var message) ? message : "Mensaje no encontrado";
        }
    }
}
