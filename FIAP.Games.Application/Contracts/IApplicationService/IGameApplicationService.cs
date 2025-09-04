using FIAP.Games.Application.DTOs;
using FIAP.Games.Application.Implementations;
using FluentValidation.Results;

namespace FIAP.Games.Application.Contracts.IApplicationService
{
    public interface IGameApplicationService
    {
        Task<ValidationResult> RegisterGame(GameDTO gameDTO);
        Task<ValidationResultDTO<GameDTO>> GetById(Guid id);
        Task<IEnumerable<GameDTO>> GetAll();
        Task<ValidationResult> UpdateGame(GameDTO game);
    }
}
