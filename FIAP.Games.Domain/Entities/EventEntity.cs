using FIAP.Games.Domain.Core.Models;

namespace FIAP.Games.Domain.Entities
{
    public class EventEntity : BaseEntity
    {
        public Guid AggregateId { get; set; }
        public string EventType { get; set; }
        public string EventData { get; set; }
        public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
    }
}
