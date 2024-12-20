﻿using GerenciadorProdutosAPI.Application.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorProdutosAPI.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProducts();
        ProductViewModel GetById(int? id);
        ProductViewModel Add(ProductViewModel product);
        void Update(ProductViewModel product);
        void Delete(int id);
    }
}
