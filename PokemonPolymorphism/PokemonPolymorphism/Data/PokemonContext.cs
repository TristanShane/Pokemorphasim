using Microsoft.EntityFrameworkCore;

namespace PokemonPolymorphism
{
    // We keep the EF Core context focused on persistence concerns only.
    internal sealed class PokemonContext : DbContext
    {
        public DbSet<PokemonEntity> Pokemon { get; set; }
        public DbSet<MoveEntity> Moves { get; set; }
        public DbSet<PokemonMoveEntity> PokemonMoves { get; set; }

        public PokemonContext(DbContextOptions<PokemonContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // We configure the schema explicitly to keep migrations predictable.
            modelBuilder.Entity<PokemonEntity>(entity =>
            {
                entity.HasKey(pokemon => pokemon.Id);
                entity.Property(pokemon => pokemon.Name).IsRequired();
                entity.Property(pokemon => pokemon.Type).IsRequired();
                entity.Property(pokemon => pokemon.Weakness).IsRequired();
            });

            modelBuilder.Entity<MoveEntity>(entity =>
            {
                entity.HasKey(move => move.Id);
                entity.Property(move => move.Name).IsRequired();
                entity.Property(move => move.Type).IsRequired();
            });

            modelBuilder.Entity<PokemonMoveEntity>(entity =>
            {
                entity.HasKey(link => new { link.PokemonId, link.MoveId });
                entity.HasOne(link => link.Pokemon)
                    .WithMany(pokemon => pokemon.PokemonMoves)
                    .HasForeignKey(link => link.PokemonId);
                entity.HasOne(link => link.Move)
                    .WithMany(move => move.PokemonMoves)
                    .HasForeignKey(link => link.MoveId);
            });
        }
    }
}
