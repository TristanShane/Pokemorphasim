namespace PokemonPolymorphism
{
    // Each concrete Pokemon focuses on only its unique behavior (Single Responsibility).
    internal sealed class Pikachu : Pokemon
    {
        public Pikachu()
            : base("Pikachu", "Electric", "Ground", 100, new[] { "Thunderbolt", "Quick Attack" })
        {
        }

        public override string GetBattleCry()
        {
            return "Pika pika!";
        }
    }
}
