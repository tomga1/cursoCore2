﻿using Azure.Identity;
using cursoCore2API.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cursoCore2.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int idProducto { get; set; }

        [Required]
        public string? nombre { get; set; }

        [Required]  
        public int stock { get; set; }
        public string? descripcion { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal precio { get; set; }

        public string? imagen { get; set; }

        public int idMarca { get; set; }    

        [ForeignKey("idMarca")]
        public virtual Marcas? Marcas { get; set; }
    }
}
