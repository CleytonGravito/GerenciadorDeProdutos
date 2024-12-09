using GerenciadorProdutosAPI.Domain.Entities;
using System.Threading.Tasks;

namespace GerenciadorProdutosAPI.Application.Interfaces
{
    public interface IUserMktService
    {
        Task<UserMkt> GetEmployeeByIdAsync(int? id);
    }
}
