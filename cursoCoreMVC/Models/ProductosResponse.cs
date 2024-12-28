namespace cursoCoreMVC.Models
{
    public class ProductosResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public List<Productos> Items { get; set; }
    }
}
