﻿namespace FIAP.Games.Infra.Context
{
    public class MicroServiceContext : DbContext
    {
        public MicroServiceContext(DbContextOptions<MicroServiceContext> options) : base(options)
        {

        }

        public DbSet<Game> Game { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<UserLibrary> UserLibrary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MicroServiceContext).Assembly);
        }
    }
}
