using FIAP.Games.Domain.Core.Models;

namespace FIAP.Games.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Category() { }

        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
