namespace cursoCore2API.DTOs
{
    public class ProductoInsertDto
    {
        public string? nombre { get; set; }
        public int stock { get; set; }
        public string? descripcion { get; set; }
        public decimal precio { get; set; }
        public string? RutaImagen { get; set; }
        public IFormFile Imagen {  get; set; }  
        public int idMarca { get; set; }
        public int categoriaId { get; set; }

    }
}
