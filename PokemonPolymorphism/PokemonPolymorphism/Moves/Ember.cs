namespace PokemonPolymorphism
{
    internal sealed class Ember : IMove
    {
        public string Name => "Ember";

        public string Execute(Pokemon pokemon)
        {
            return $"{pokemon.Name} uses {Name}!";
        }
    }
}
