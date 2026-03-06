namespace PokemonPolymorphism
{
    // We model the many-to-many relationship explicitly to keep configuration clear.
    internal sealed class PokemonMoveEntity
    {
        public int PokemonId { get; set; }
        public PokemonEntity Pokemon { get; set; }
        public int MoveId { get; set; }
        public MoveEntity Move { get; set; }
    }
}
