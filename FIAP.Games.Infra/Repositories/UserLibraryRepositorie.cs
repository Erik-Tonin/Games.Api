using FIAP.Games.Domain.Contracts.IRepositories;
using FIAP.Games.Domain.Entities;
using FIAP.Games.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Games.Infra.Repositories
{
    public class UserLibraryRepositorie : RepositoryBase<UserLibrary>, IUserLibraryRepository
    {
        public UserLibraryRepositorie(MicroServiceContext context) : base(context)
        {
        }

        public async Task<UserLibrary> FindLibraryEntryForUser(Guid userProfileId, Guid gameId)
        {
            return await _context.UserLibrary.Where(x => x.UserProfileId == userProfileId && x.GameId == gameId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserLibrary>> GetByUserProfileId(Guid userProfileId)
        {
            return await _context.UserLibrary
                .Include(x => x.Game)
                .Where(x => x.UserProfileId == userProfileId)
                .ToListAsync();
        }
    }
}
