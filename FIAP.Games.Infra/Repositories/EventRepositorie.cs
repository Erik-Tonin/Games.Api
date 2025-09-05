using FIAP.Games.Domain.Contracts.IRepositories;
using FIAP.Games.Domain.Entities;
using FIAP.Games.Infra.Context;

namespace FIAP.Games.Infra.Repositories
{
    public class EventRepositorie : RepositoryBase<EventEntity>, IEventRepository
    {
        public EventRepositorie(MicroServiceContext context) : base(context)
        {
        }
    }
}
