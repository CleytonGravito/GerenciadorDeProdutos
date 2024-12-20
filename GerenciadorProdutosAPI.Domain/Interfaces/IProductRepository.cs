﻿using GerenciadorProdutosAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorProdutosAPI.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Product GetById(int? id);
        Product Add(Product product); 
        void Update(Product product);
        void Delete(Product product);

    }
}
