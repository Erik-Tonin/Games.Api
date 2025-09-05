using FIAP.Games.Domain.Entities;
using FIAP.Games.Domain.Statics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Games.Infra.EntityMappers
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(65).IsRequired();

            builder.HasData(CategoryStactic.GetAll().Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name
            }));
        }
    }
}
