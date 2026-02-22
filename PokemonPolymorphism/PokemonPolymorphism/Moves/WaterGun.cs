namespace PokemonPolymorphism
{
    internal sealed class WaterGun : IMove
    {
        public string Name => "Water Gun";

        public string Execute(Pokemon pokemon)
        {
            return $"{pokemon.Name} uses {Name}!";
        }
    }
}
