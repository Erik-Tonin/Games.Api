using FIAP.Games.Domain.Entities;

namespace FIAP.Games.Domain.Contracts.IRepositories
{
    public interface IUserLibraryRepository : IRepositoryBase<UserLibrary>
    {
        Task<UserLibrary> FindLibraryEntryForUser(Guid userProfileId, Guid gameId);
        Task<IEnumerable<UserLibrary>> GetByUserProfileId(Guid userProfileId);
    }
}
