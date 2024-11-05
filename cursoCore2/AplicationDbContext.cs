﻿using cursoCore2.Models;
using Microsoft.EntityFrameworkCore;

namespace cursoCore2
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Producto> productos {  get; set; } 
        
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) 
        {
            
        }   
    }
}
