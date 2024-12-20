﻿using GerenciadorProdutosAPI.Application.InputModes;
using GerenciadorProdutosAPI.Application.Interfaces;
using GerenciadorProdutosAPI.Application.Services;
using GerenciadorProdutosAPI.Application.ViewModels;
using GerenciadorProdutosAPI.Domain.Interfaces;
using GerenciadorProdutosAPI.Infra.Data.Context;
using GerenciadorProdutosAPI.Infra.Data.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using GerenciadorProdutosAPI.Application.Authentication;
using Microsoft.IdentityModel.Tokens;
using GerenciadorProdutosAPI.Domain.Entities;

namespace GerenciadorProdutosAPI.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                      IConfiguration Configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DataBase"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserMktRepository, UserMktRepository>();
            services.AddScoped<IUserMktService, UserMktService>();
            services.AddScoped<IValidator<ProductViewModel>, Validator>();
            services.AddScoped<IValidator<UserMkt>, ValidatorUser>();

            //AuthenticationJwt
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            return services;
        }

}
}
