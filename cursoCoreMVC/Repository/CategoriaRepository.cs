﻿using cursoCoreMVC.Models;
using cursoCoreMVC.Repository.IRepository;

namespace cursoCoreMVC.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        //injeccion de dependencias se debe importar el ihttpclientfactory
        private readonly IHttpClientFactory _clientFactory;

        public CategoriaRepository(IHttpClientFactory clientFactory) : base (clientFactory)   
        {
            _clientFactory = clientFactory; 
        }
    }
}
