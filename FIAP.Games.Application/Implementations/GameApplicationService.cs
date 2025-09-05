using Elastic.Clients.Elasticsearch;
using FIAP.Games.Application.Contracts.IApplicationService;
using FIAP.Games.Application.DTOs;
using FIAP.Games.Domain.Contracts.IRepositories;
using FIAP.Games.Domain.Entities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FIAP.Games.Application.Implementations
{
    public class GameApplicationService : ApplicationServiceBase, IGameApplicationService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ElasticsearchClient _elasticClient;

        public GameApplicationService(IGameRepository gameRepository, IEventRepository eventRepository, ElasticsearchClient elasticClient)
        {
            _gameRepository = gameRepository;
            _eventRepository = eventRepository;
            _elasticClient = elasticClient;
        }

        public async Task<ValidationResult> RegisterGame(GameDTO gameDTO)
        {
            var existingGame = await GetByName(gameDTO.Name!);

            if (existingGame != null)
            {
                AddValidationError("Jogo já cadastrado.", "Já existe um jogo com este nome");
                return ValidationResult;
            }

            var game = new Game(
                gameDTO.Name!,
                gameDTO.Category!,
                gameDTO.Censorship!,
                gameDTO.Price!,
                gameDTO.DateRelease,
                gameDTO.ImageURL);

            if (!game.IsValid())
                return game.ValidationResult;

            await _gameRepository.Add(game);
            await Event(game);
            await Elastic(game);

            return game.ValidationResult;
        }

        public async Task<ValidationResultDTO<GameDTO>> GetById(Guid id)
        {
            Game game = await _gameRepository.GetById(id);

            if (game == null)
            {
                var validation = new ValidationProblemDetails();
                validation.Errors.Add("Game", new[] { "Não foi encontrado o jogo!." });

                return new ValidationResultDTO<GameDTO>
                {
                    ValidationProblemDetails = validation
                };
            }

            return new ValidationResultDTO<GameDTO>
            {
                Response = new GameDTO
                {
                    Id = game.Id,
                    Name = game.Name,
                    Category = game.Category,
                    Censorship = game.Censorship,
                    Price = game.Price,
                    DateRelease = game.DateRelease
                }
            };
        }

        public async Task<IEnumerable<GameDTO>> GetAll()
        {
            var games = _gameRepository.GetAll();
            return await Task.FromResult(games.Select(x => new GameDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Censorship = x.Censorship,
                Price = x.Price,
                DateRelease = x.DateRelease
            }));
        }

        public async Task<ValidationResult> UpdateGame(GameDTO gameDTO)
        {
            Game game = await _gameRepository.GetById(gameDTO.Id);

            var existingGame = await GetByName(gameDTO.Name!);

            if (existingGame != null && existingGame.Id != gameDTO.Id)
            {
                AddValidationError("Já existe um jogo com este nome.", "Já existe um jogo com este nome");
                return ValidationResult;
            }

            if (game.IsValid())
            {
                game.UpdateGame(
                    gameDTO.Name!,
                    gameDTO.Category,
                    gameDTO.Censorship,
                    gameDTO.Price,
                    gameDTO.DateRelease,
                    gameDTO.ImageURL);

                _gameRepository.Update(game);
                await Event(game);
                await Elastic(game);
            }
            else
            {
                return game.ValidationResult;
            }

            return game.ValidationResult;
        }

        public async Task<Game> GetByName(string name)
        {
            return await _gameRepository.GetByName(name);
        }

        public async Task Event(Game game)
        {
            var eventEntity = new EventEntity
            {
                AggregateId = game.Id,
                EventType = "GameRegistered",
                EventData = JsonSerializer.Serialize(new
                {
                    game.Id,
                    game.Name,
                    game.Category,
                    game.Censorship,
                    game.Price,
                    game.DateRelease
                }),
                OccurredOn = DateTime.UtcNow
            };

            await _eventRepository.Add(eventEntity);
        }

        public async Task Elastic(Game game)
        {
            var elasticGame = new
            {
                id = game.Id,
                name = game.Name,
                category = game.Category,
                censorship = game.Censorship,
                price = game.Price,
                dateRelease = game.DateRelease,
            };

            await _elasticClient.IndexAsync(elasticGame, idx => idx.Index("fiap"));
        }
    }
}
