using GerenciadorProdutosAPI.Domain.Entities;
using System.Threading.Tasks;

namespace GerenciadorProdutosAPI.Domain.Interfaces
{
    public interface IUserMktRepository
    {
        Task<UserMkt> GetById(int? id);
    }
}
