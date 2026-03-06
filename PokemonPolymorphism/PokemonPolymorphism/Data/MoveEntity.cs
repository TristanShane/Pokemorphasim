using System.Collections.Generic;

namespace PokemonPolymorphism
{
    internal sealed class MoveEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Power { get; set; }
        public ICollection<PokemonMoveEntity> PokemonMoves { get; set; } = new List<PokemonMoveEntity>();
    }
}
