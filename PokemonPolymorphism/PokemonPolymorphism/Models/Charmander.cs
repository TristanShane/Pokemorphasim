namespace PokemonPolymorphism
{
    internal sealed class Charmander : Pokemon
    {
        public Charmander()
            : base("Charmander", "Fire", "Water", 100, new[] { "Ember" })
        {
        }

        public override string GetBattleCry()
        {
            return "Char!";
        }
    }
}
