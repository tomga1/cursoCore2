namespace cursoCoreMVC.DTOs
{
    public class ProductoDTO
    {
        public int idProducto { get; set; }
        public string? nombre { get; set; }
        public int stock { get; set; }
        public string? descripcion { get; set; }
        public decimal precio { get; set; }
        public string? imagen { get; set; }

    }
}
