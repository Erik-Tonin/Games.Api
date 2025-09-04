using FIAP.Games.Domain.Core.Models;

namespace FIAP.Games.Domain.Entities
{
    public class UserLibrary : BaseEntity
    {
        public Guid UserProfileId { get; private set; }
        public Guid GameId { get; private set; }
        public virtual Game Game { get; private set; }

        protected UserLibrary() { }

        public UserLibrary(Guid userProfileId, Guid gameId)
        {
            UserProfileId = userProfileId;
            GameId = gameId;
            DateCreated = DateTime.Now;
        }
    }
}
