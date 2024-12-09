using GerenciadorProdutosAPI.Application.Interfaces;
using GerenciadorProdutosAPI.Domain.Entities;
using GerenciadorProdutosAPI.Domain.Interfaces;
using System.Threading.Tasks;

namespace GerenciadorProdutosAPI.Application.Services
{
    public class UserMktService : IUserMktService
    {
        private readonly IUserMktRepository _userRepository;

        public UserMktService(IUserMktRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserMkt> GetEmployeeByIdAsync(int? id)
        {
           return await _userRepository.GetById(id);
        }
    }
}
