using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using GerenciadorProdutosAPI.Infra.IoC;
using APIGerenciadorProdutosAPI.MappingConfig;
using System.Reflection;
using System.IO;
using System;
using System.Net.Http.Headers;

namespace APIGerenciadorProdutosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método é chamado pelo runtime. Use este método para adicionar serviços ao container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddInfrastructure(Configuration);
            services.AddAutoMapperConfiguration();
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gerenciador de Produtos",
                    Version = "v1",
                    Description = "Este projeto  uma API .NET desenvolvida para gerenciar produtos em um mercado. A aplicao oferece operaes CRUD (Criar, Ler, Atualizar, Excluir) para manipular os produtos no banco de dados. A arquitetura Clean Architecture  usada para garantir uma separao clara de responsabilidades, incluindo camadas de Aplicao, Domnio, Dados e IoC. O cdigo segue princpios de Clean Code para manuteno e legibilidade.",
                }
                );
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Insira o token JWT da solicitao no campo",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                };

                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] {}
                    }
                };

                c.AddSecurityRequirement(securityRequirement);
            });            
        }

        // Este método é chamado pelo runtime. Use este método para configurar o pipeline de requisições HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GerenciadorProdutosAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
