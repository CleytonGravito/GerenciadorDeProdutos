using AutoMapper;
using GerenciadorProdutosAPI.Application.ViewModels;
using GerenciadorProdutosAPI.Domain.Entities;

namespace GerenciadorProdutosAPI.Application.Mappings
{
    public class DomainToViewModelMappinProfile : Profile
    {
        public DomainToViewModelMappinProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}
