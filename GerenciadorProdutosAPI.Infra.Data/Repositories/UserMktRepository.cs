using GerenciadorProdutosAPI.Domain.Entities;
using GerenciadorProdutosAPI.Domain.Interfaces;
using GerenciadorProdutosAPI.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GerenciadorProdutosAPI.Infra.Data.Repositories
{
    public class UserMktRepository : IUserMktRepository
    {
        protected private ApplicationDbContext _context;

        public UserMktRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UserMkt> GetById(int? id)
        {
            return await _context.UserMkts.SingleOrDefaultAsync(x => x.Id == id);
        }        
    }
}
