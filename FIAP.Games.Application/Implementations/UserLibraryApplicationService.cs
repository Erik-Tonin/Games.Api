﻿using FIAP.Games.Application.Contracts.IApplicationService;
using FIAP.Games.Application.DTOs;

namespace FIAP.Games.Application.Implementations
{
    public class UserLibraryApplicationService : ApplicationServiceBase, IUserLibraryApplicationService
    {
        private readonly IUserLibraryRepository _userLibraryRepository;

        public UserLibraryApplicationService(IUserLibraryRepository userLibraryRepository)
        {
            _userLibraryRepository = userLibraryRepository;
        }

        public async Task<ValidationResultDTO<UserLibrary>> AddToLibrary(UserLibraryDTO userLibraryDTO)
        {
            UserLibrary userLibrary = await FindLibraryEntryForUser(userLibraryDTO.UserProfileId!, userLibraryDTO.GameId);

            if (userLibrary != null)
                throw new Exception("Já existe um jogo cadastrado em sua biblioteca");

            userLibrary = new UserLibrary(
                userLibraryDTO.UserProfileId!,
                userLibraryDTO.GameId!);

            await _userLibraryRepository.Add(userLibrary);

            return CustomValidationDataResponse<UserLibrary>(userLibrary);
        }

        public async Task<IEnumerable<UserLibraryDTO>> GetByUserProfileId(Guid userProfileId)
        {
            var library = await _userLibraryRepository.GetByUserProfileId(userProfileId);
            return await Task.FromResult(library.Select(x => new UserLibraryDTO()
            {
                Id = x.Id,
                GameId = x.GameId,
                UserProfileId = x.UserProfileId,
                Game = x.Game,
            }));
        }

        public async Task<UserLibrary> FindLibraryEntryForUser(Guid userProfileId, Guid gameId)
        {
            return await _userLibraryRepository.FindLibraryEntryForUser(userProfileId, gameId);
        }
    }
}
