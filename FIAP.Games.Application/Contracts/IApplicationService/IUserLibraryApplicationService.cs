using FIAP.Games.Application.DTOs;
using FIAP.Games.Application.Implementations;
using FIAP.Games.Domain.Entities;

namespace FIAP.Games.Application.Contracts.IApplicationService
{
    public interface IUserLibraryApplicationService
    {
        Task<ValidationResultDTO<UserLibrary>> AddToLibrary(UserLibraryDTO userLibraryDTO);
        Task<IEnumerable<UserLibraryDTO>> GetByUserProfileId(Guid userProfileId);
    }
}
