using FIAP.Games.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
