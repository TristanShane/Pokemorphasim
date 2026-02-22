namespace PokemonPolymorphism
{
    internal sealed class Bulbasaur : Pokemon
    {
        public Bulbasaur()
            : base("Bulbasaur", "Grass", "Fire", 100, new[] { "Vine Whip" })
        {
        }

        public override string GetBattleCry()
        {
            return "Bulba!";
        }
    }
}
