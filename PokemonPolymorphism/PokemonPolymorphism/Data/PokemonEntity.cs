using System.Collections.Generic;

namespace PokemonPolymorphism
{
    // We keep persistence entities simple to support EF Core tracking.
    internal sealed class PokemonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Weakness { get; set; }
        public int Health { get; set; }
        public ICollection<PokemonMoveEntity> PokemonMoves { get; set; } = new List<PokemonMoveEntity>();
    }
}
