using GerenciadorProdutosAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorProdutosAPI.Infra.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Price).HasPrecision(10, 2);

            //VAI VERIFCAR SE A TABELA PRODUCT POSSUI DADOS. SE NÃO, ELE VAI CRIAR COM ESSAS INFORMAÇÕES.
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Lápis",
                    Description = "Lápis preto",
                    Price = 2.50M
                },
                new Product
                {
                    Id = 2,
                    Name = "Caneta",
                    Description = "Caneta azul ponta fina",
                    Price = 5.50M
                },
                new Product
                {
                    Id = 3,
                    Name = "Régua",
                    Description = "Regua de 30cm",
                    Price = 6.50M
                }
                );

        }
    }
}

