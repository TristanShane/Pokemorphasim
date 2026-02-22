namespace PokemonPolymorphism
{
    internal sealed class Thunderbolt : IMove
    {
        public string Name => "Thunderbolt";

        public string Execute(Pokemon pokemon)
        {
            // We pass the Pokemon in to keep the move reusable and independent.
            return $"{pokemon.Name} uses {Name}!";
        }
    }
}
