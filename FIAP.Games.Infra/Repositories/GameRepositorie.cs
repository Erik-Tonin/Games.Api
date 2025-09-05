using FIAP.Games.Domain.Contracts.IRepositories;
using FIAP.Games.Domain.Entities;
using FIAP.Games.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Games.Infra.Repositories
{
    public class GameRepositorie : RepositoryBase<Game>, IGameRepository
    {
        public GameRepositorie(MicroServiceContext context) : base(context)
        {
        }

        public async Task<Game> GetByName(string name) => await _context.Game.FirstOrDefaultAsync(x => x.Name == name);
    }
}
