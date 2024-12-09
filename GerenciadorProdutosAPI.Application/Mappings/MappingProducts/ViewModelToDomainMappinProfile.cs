using AutoMapper;
using GerenciadorProdutosAPI.Application.ViewModels;
using GerenciadorProdutosAPI.Domain.Entities;

namespace GerenciadorProdutosAPI.Application.Mappings
{
    public class ViewModelToDomainMappinProfile :Profile
    {
        public ViewModelToDomainMappinProfile()
        {
            CreateMap<ProductViewModel, Product>();
        }
    }
}
