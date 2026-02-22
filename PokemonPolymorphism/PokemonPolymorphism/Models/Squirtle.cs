namespace PokemonPolymorphism
{
    internal sealed class Squirtle : Pokemon
    {
        public Squirtle()
            : base("Squirtle", "Water", "Electric", 100, new[] { "Water Gun" })
        {
        }

        public override string GetBattleCry()
        {
            return "Squirtle!";
        }
    }
}
