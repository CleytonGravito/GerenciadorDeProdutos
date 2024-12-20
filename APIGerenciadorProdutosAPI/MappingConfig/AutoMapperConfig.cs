﻿using GerenciadorProdutosAPI.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace APIGerenciadorProdutosAPI.MappingConfig
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException (nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappinProfile), 
                typeof(ViewModelToDomainMappinProfile));
        }
    }
}
