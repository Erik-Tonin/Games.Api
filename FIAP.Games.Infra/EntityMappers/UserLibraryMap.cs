using FIAP.Games.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Games.Infra.EntityMappers
{
    public class UserLibraryMap : IEntityTypeConfiguration<UserLibrary>
    {
        public void Configure(EntityTypeBuilder<UserLibrary> builder)
        {
            builder.ToTable("UserLibrary", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserProfileId).HasMaxLength(65).IsRequired();
            builder.Property(x => x.GameId).HasMaxLength(80).IsRequired();
        }
    }
}
