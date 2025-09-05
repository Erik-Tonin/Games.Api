using Elastic.Clients.Elasticsearch;
using FIAP.Games.Application.Contracts.IApplicationService;
using FIAP.Games.Application.DTOs;
using FIAP.Games.Domain.Contracts.IRepositories;
using FIAP.Games.Domain.Entities;
using System.Text.Json;

namespace FIAP.Games.Application.Implementations
{
    public class UserLibraryApplicationService : ApplicationServiceBase, IUserLibraryApplicationService
    {
        private readonly IUserLibraryRepository _userLibraryRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ElasticsearchClient _elasticClient;

        public UserLibraryApplicationService(IUserLibraryRepository userLibraryRepository, IEventRepository eventRepository, ElasticsearchClient elasticClient)
        {
            _userLibraryRepository = userLibraryRepository;
            _eventRepository = eventRepository;
            _elasticClient = elasticClient;
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
            await Event(userLibrary);

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

        public async Task Event(UserLibrary userLibrary)
        {
            var eventEntity = new EventEntity
            {
                AggregateId = userLibrary.Id,
                EventType = "UserLibrary",
                EventData = JsonSerializer.Serialize(new
                {
                    userLibrary.Id,
                    userLibrary.UserProfileId,
                    userLibrary.GameId
                }),
                OccurredOn = DateTime.UtcNow
            };

            await _eventRepository.Add(eventEntity);
        }
    }
}
