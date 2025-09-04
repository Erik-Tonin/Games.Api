namespace FIAP.Games.Domain.Contracts.IRepositories
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        Task<Game> GetByName(string name);
    }
}
